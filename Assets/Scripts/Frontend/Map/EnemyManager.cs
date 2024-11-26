using System;
using System.Collections;
using System.Collections.Generic;
using TowerDefense.Scripts.Frontend.UIElements;
using TowerDefense.Scripts.Utils;
using TowerDefense.Scripts.Utils.Managers;

using UnityEngine;
using UnityEngine.Events;

namespace TowerDefense.Scripts.Frontend.Level
{
    public class EnemyManager : MonoBehaviourSingletonInScene<EnemyManager>
    {
        [Header("References")]
        public int[] EnemyIDs;

        [Header("Attributes")]
        public int BaseEnemies = 8;
        public float EnemiesPerSeconds = 0.5f;
        public float TimeBetweenWaves = 5f;
        public float MaxEnemiesPerSecond = 10f;

        [Header("Events")]
        public static UnityEvent OnEnemyDestroy;

        private int currentWave = 1;
        private int enemiesAlive;
        private int enemiesLeftToSpawn;
        private float timeSinceLastSpawn;
        private float epsScalar;
        private float DifficultyScalar = 0.75f;
        private bool isSpawning = false;

        public override void Awake()
        {
            base.Awake();

            OnEnemyDestroy = new UnityEvent();

            OnEnemyDestroy.AddListener(EnemyDestroyed);
        }

        private void Start()
        {
            StartCoroutine(StartWave());
        }

        private void Update()
        {
            if (!isSpawning || MapManager.Get().IsPaused)
                return;

            timeSinceLastSpawn += Time.deltaTime;

            if(timeSinceLastSpawn >= 1 / EnemiesPerSeconds && enemiesLeftToSpawn > 0)
            {
                SpawnEnemy();

                enemiesLeftToSpawn--;
                enemiesAlive++;

                timeSinceLastSpawn = 0;
            }

            if(enemiesAlive ==0 && enemiesLeftToSpawn ==0)
            {
                EndWave();
            }
        }

        private void SpawnEnemy()
        {
            int index = UnityEngine.Random.Range(0, EnemyIDs.Length);

            ObjectPooler.Get().EnableItem(EnemyIDs[index]);
        }

        public void GameEnded()
        {
            isSpawning = false;

            for (short i = 0; i < EnemyIDs.Length; i++) 
            {
                ObjectPooler.Get().DisableAllItems(EnemyIDs[i]);
            }
        }

        private IEnumerator StartWave()
        {
            yield return new WaitForSeconds(TimeBetweenWaves);

            isSpawning = true;
            enemiesLeftToSpawn = EnemiesPerWave();
            EnemiesPerSeconds = EnemiesPerSecond();
        }

        private void EndWave()
        {
            isSpawning = false;
            timeSinceLastSpawn = 0;
            currentWave++;

            DataHolder.Get().UpdateWave(currentWave);

            StartCoroutine(StartWave());
        }

        private int EnemiesPerWave()
        {
            return Mathf.RoundToInt(BaseEnemies * Mathf.Pow(currentWave, DifficultyScalar));
        }

        private float EnemiesPerSecond()
        {
            return Mathf.Clamp(Mathf.RoundToInt(EnemiesPerSeconds * Mathf.Pow(currentWave, epsScalar)), 0f, MaxEnemiesPerSecond);
        }

        private void EnemyDestroyed()
        {
            enemiesAlive--;
        }

        public int GetCurrentWave()
        {
            return currentWave;
        }
    }
}