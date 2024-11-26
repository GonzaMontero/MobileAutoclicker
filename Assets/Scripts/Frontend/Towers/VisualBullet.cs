using System.Collections;
using System.Collections.Generic;
using TowerDefense.Scripts.Frontend.Enemies;
using TowerDefense.Scripts.Frontend.Level;
using TowerDefense.Scripts.Utils.Managers;
using UnityEngine;

namespace TowerDefense.Scripts.Frontend.Towers
{
    public class VisualBullet : MonoBehaviour
    {
        [Header("Attributes")]
        public float BulletSpeed = 5f;
        public int BulletDamage = 0;


        private Transform target;
        private Rigidbody2D rb;

        public void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        public void SetupBullet(Transform tg, int damage)
        {
            target = tg;
            BulletDamage = damage;
        }

        private void FixedUpdate()
        {
            if (MapManager.Get().IsPaused)
                return;

            if (!target)
                return;
            else if (!target.gameObject.activeSelf)
                ObjectPooler.Get().DisableItem(1, this.gameObject);

            Vector2 direction = (target.position - transform.position).normalized;

            rb.velocity = direction * BulletSpeed;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            other.gameObject.GetComponent<EnemyMovement>().TakeDamage(BulletDamage);

            target = null;

            ObjectPooler.Get().DisableItem(1, this.gameObject);
        }
    }
}