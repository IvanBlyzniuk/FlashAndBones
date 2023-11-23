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

        [SerializeField]
        private float enemyHpIncrease;
        [SerializeField]
        private List<BaseEnemy> enemies;

        public void StartWave()
        {
            //allowedEnemies = enemies.FindAll(e => e.EnemyData.firstSpawningWave <= waveNum);
            //currentTotalEnemyWeight = 0;
            //foreach (BaseEnemy enemy in allowedEnemies)
            //{
            //    currentTotalEnemyWeight += enemyWeights[enemy];
            //}
            //StartCoroutine(Wave());
        }

        public void Init(EnemySpawningSystem enemySpawningSystem)
        {
            this.enemySpawningSystem = enemySpawningSystem;
            foreach (BaseEnemy enemy in enemies)
                StartCoroutine(AddEnemyWithDelay(enemy, enemy.EnemyData.spawnStartTime));
            CalculateEnemyWeights();
            StartCoroutine(Wave());
        }

        public IEnumerator AddEnemyWithDelay(BaseEnemy enemy, float delay)
        {
            yield return new WaitForSeconds(delay);
            enemiesToAdd.Add(enemy);
        }

        private void CalculateEnemyWeights()
        {
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
                BaseEnemy randomEnemy = getRandomEnemy();
                enemySpawningSystem.SpawnEnemy(randomEnemy.gameObject, 1 /* + hp multiplier */);
                yield return new WaitForSeconds(1);
            }
        }

        //private void SpawnSubWave()
        //{
        //    int dangerDiff = (int)(totalDangerLevel * subWaveDangerPercentage);
        //    int startingDangerLevel = dangerLevelLeft;
        //    while(dangerLevelLeft > startingDangerLevel - dangerDiff)
        //    {
        //        BaseEnemy randomEnemy = getRandomEnemy();
        //        enemySpawningSystem.SpawnEnemy(randomEnemy.gameObject, 1 + waveNum * enemyHpIncrease);
        //        dangerLevelLeft -= randomEnemy.EnemyData.dangerLevel;
        //        enemiesAlive++;
        //    }
        //}

        private BaseEnemy getRandomEnemy()
        {
            float randomWeight = Random.value * currentTotalEnemyWeight;
            //foreach(BaseEnemy enemy in allowedEnemies)
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

