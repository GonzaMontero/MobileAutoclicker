using System.Collections;
using System.Collections.Generic;
using TowerDefense.Scripts.Frontend.Level;
using TowerDefense.Scripts.Utils.Managers;
using UnityEngine;

namespace TowerDefense.Scripts.Frontend.Enemies
{
    public class EnemyMovement : MonoBehaviour
    {
        [Header("References")]
        public Rigidbody2D ThisRigidBody;

        [Header("Attributes")]
        public float MoveSpeed = 2f;
        public int Health = 1;

        private Transform target;
        private int pathIndex = 0;

        private void Start()
        {
            target = MapManager.Get().PathNodes[pathIndex];
        }

        private void Update()
        {
            if (Vector2.Distance(target.position, ThisRigidBody.position) <= 0.1f)
            {
                pathIndex++;

                if (pathIndex >= MapManager.Get().PathNodes.Length)
                {
                    ObjectPooler.Get().DisableItem(0, this.gameObject);
                    EnemyManager.OnEnemyDestroy.Invoke();
                    return;
                }
                else
                {
                    target = MapManager.Get().PathNodes[pathIndex];
                }
            }

        }

        private void FixedUpdate()
        {

            Vector2 direction = (target.position - transform.position);
            float distanceToTarget = direction.magnitude;

            if (distanceToTarget < 0.1f)
            {
                transform.position = target.position;
                ThisRigidBody.velocity = Vector2.zero;
            }
            else
            {
                ThisRigidBody.velocity = direction.normalized * MoveSpeed;
            }
        }
    }
}