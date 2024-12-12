using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TowerDefense.Scripts.Frontend.Level;
using TowerDefense.Scripts.Frontend.Enemies;

namespace TowerDefense.Scripts.Frontend.Towers
{
    public class Tower : MonoBehaviour
    {
        [Header("References")]
        public LayerMask EnemyMask;
        public Transform FiringPoint;
        public Transform Target;

        [Header("Attributes")]
        public float TargetingRange = 4f;
        public float BulletsPerSecond = 1;
        public int Damage = 3;

        private float timeUntilFire;

        private void Update()
        {
            if (MapManager.Get().IsPaused)
                return;

            if(Target == null || !Target.gameObject.activeSelf)
            {
                FindTarget();
                return;
            }

            if(!CheckTargetIsInRange())
            {
                Target = null;
            }
            else
            {
                timeUntilFire += Time.deltaTime;
                
                if(timeUntilFire >= 1f / BulletsPerSecond)
                {
                    Shoot();
                    timeUntilFire = 0f;
                }
            }
        }

        private void Shoot()
        {
            BulletsFactory.GetBullet(1).Process(this);
        }

        private void FindTarget()
        {
            RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, TargetingRange,
                (Vector2)transform.position, 0f, EnemyMask);

            int currentPathIndex = -1;
            int largestPathItem = 0;

            if (hits.Length > 0)
            {
                for (short i = 0; i < hits.Length; i++)
                {
                    if (hits[i].collider.gameObject.GetComponent<EnemyMovement>().GetPathIndex() > currentPathIndex)
                    {
                        currentPathIndex = hits[i].collider.gameObject.GetComponent<EnemyMovement>().GetPathIndex();
                        largestPathItem = i;
                    }
                }

                Target = hits[largestPathItem].transform;
            }
        }

        private bool CheckTargetIsInRange()
        {
            return Vector2.Distance(Target.position, transform.position) <= TargetingRange;
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Handles.color = Color.cyan;
            Handles.DrawWireDisc(transform.position, transform.forward, TargetingRange);
        }
#endif
    }
}