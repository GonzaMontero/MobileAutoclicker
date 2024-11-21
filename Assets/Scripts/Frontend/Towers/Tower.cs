using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

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
            if(Target == null)
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

            if (hits.Length > 0)
            {
                Target = hits[0].transform;
            }
        }

        private bool CheckTargetIsInRange()
        {
            return Vector2.Distance(Target.position, transform.position) <= TargetingRange;
        }

        private void OnDrawGizmos()
        {
            Handles.color = Color.cyan;
            Handles.DrawWireDisc(transform.position, transform.forward, TargetingRange);
        }
    }
}