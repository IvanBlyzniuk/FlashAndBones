using App.Systems.EnemySpawning;
using App.Systems.GameStates;
using App.World.Entity.Enemy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.Systems.Wave
{
    public class WaveSystem : MonoBehaviour, IWaveSystem
    {
        private EnemySpawningSystem enemySpawningSystem;
        private List<BaseEnemy> allowedEnemies = new List<BaseEnemy>();
        private List<BaseEnemy> enemiesToAdd = new List<BaseEnemy>();
        private Dictionary<BaseEnemy, float> enemyWeights;
        private float currentTotalEnemyWeight;
        private float enemyHpMultiplier = 1f;

        [SerializeField]
        private float minTimeBetweenSubwaves;
        [SerializeField]
        private float maxTimeBetweenSubwaves;
        [SerializeField]
        private float subWaveDangerLevel;
        [SerializeField]
        private float subWaveSizeIncrease;

        [SerializeField]
        private float enemyHpIncrease;
        [SerializeField]
        private List<BaseEnemy> enemies;

        public void StartWave()
        {
            
        }

        public void Init(EnemySpawningSystem enemySpawningSystem)
        {
            this.enemySpawningSystem = enemySpawningSystem;
            foreach (BaseEnemy enemy in enemies)
            {
                if(enemy.EnemyData.spawnStartTime > 0)
                    StartCoroutine(AddEnemyWithDelay(enemy, enemy.EnemyData.spawnStartTime));
                else
                    allowedEnemies.Add(enemy);
            }
            CalculateEnemyWeights();
            StartCoroutine(Wave());
        }

        public IEnumerator AddEnemyWithDelay(BaseEnemy enemy, float delay)
        {
            Debug.Log(enemy.PoolObjectType + " " + delay);
            yield return new WaitForSeconds(delay);
            enemiesToAdd.Add(enemy);
        }

        private void CalculateEnemyWeights()
        {
            currentTotalEnemyWeight = 0;
            enemyWeights = new Dictionary<BaseEnemy, float>();
            foreach(BaseEnemy enemy in enemies)
            {
                float weight = 1.0f / enemy.EnemyData.dangerLevel;
                enemyWeights.Add(enemy, weight);
                currentTotalEnemyWeight += weight;
            }
        }

        public IEnumerator Wave()
        {
            while(true) 
            {
                if(enemiesToAdd.Count > 0)
                {
                    allowedEnemies.AddRange(enemiesToAdd);
                    enemiesToAdd.Clear();
                    CalculateEnemyWeights();
                }
                //SpawnSubWave();
                float dangerLevelLeft = subWaveDangerLevel;
                while (dangerLevelLeft > 0)
                {
                    BaseEnemy randomEnemy = getRandomEnemy();
                    enemySpawningSystem.SpawnEnemy(randomEnemy.gameObject, 1 + enemyHpMultiplier);
                    dangerLevelLeft -= randomEnemy.EnemyData.dangerLevel;
                    yield return new WaitForSeconds(0.1f);
                }
                //
                subWaveDangerLevel *= 1 + subWaveSizeIncrease;
                enemyHpMultiplier *= 1 + enemyHpIncrease;
                yield return new WaitForSeconds(Random.Range(minTimeBetweenSubwaves, maxTimeBetweenSubwaves));
            }
        }

        //private void SpawnSubWave()
        //{
        //    float dangerLevelLeft = subWaveDangerLevel;
        //    while (dangerLevelLeft > 0)
        //    {
        //        BaseEnemy randomEnemy = getRandomEnemy();
        //        enemySpawningSystem.SpawnEnemy(randomEnemy.gameObject, 1 + enemyHpMultiplier);
        //        dangerLevelLeft -= randomEnemy.EnemyData.dangerLevel;
        //    }
        //}

        private BaseEnemy getRandomEnemy()
        {
            float randomWeight = Random.value * currentTotalEnemyWeight;
            foreach (BaseEnemy enemy in allowedEnemies)
            {
                if (enemyWeights[enemy] >= randomWeight)
                    return enemy;
                randomWeight -= enemyWeights[enemy];
            }
            return allowedEnemies[allowedEnemies.Count - 1];
        }

        public void ReportKilled(string enemyType)
        {
            
        }
    }
}

