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
        private int waveNum = 1;
        private EnemySpawningSystem enemySpawningSystem;
        private GameStatesSystem gameStatesSystem;
        private int enemiesAlive = 0;
        private int dangerLevelLeft;
        [SerializeField]
        private int totalDangerLevel = 300;
        private List<BaseEnemy> allowedEnemies = new List<BaseEnemy>();
        private Dictionary<BaseEnemy, float> enemyWeights;
        private float currentTotalEnemyWeight;

        [SerializeField]
        private float enemyHpIncrease;
        [SerializeField]
        private float minTimeBetweenSubwaves;
        [SerializeField]
        private float maxTimeBetweenSubwaves;
        [SerializeField]
        private float subWaveDangerPercentage;
        [SerializeField]
        private float nextWaveDangerLevelMultiplier;
        [SerializeField]
        private List<BaseEnemy> enemies;

        public void StartWave()
        {
            allowedEnemies = enemies.FindAll(e => e.EnemyData.firstSpawningWave <= waveNum);
            currentTotalEnemyWeight = 0;
            foreach(BaseEnemy enemy in allowedEnemies)
            {
                currentTotalEnemyWeight += enemyWeights[enemy];
            }
            StartCoroutine(Wave());
        }

        public void Init(EnemySpawningSystem enemySpawningSystem, GameStatesSystem gameStatesSystem)
        {
            this.enemySpawningSystem = enemySpawningSystem;
            this.gameStatesSystem = gameStatesSystem;
            CalculateEnemyWeights();
        }

        private void CalculateEnemyWeights()
        {
            enemyWeights = new Dictionary<BaseEnemy, float>();
            foreach(BaseEnemy enemy in enemies)
            {
                enemyWeights.Add(enemy, 1.0f / enemy.EnemyData.dangerLevel);
            }
        }

        public IEnumerator Wave()
        {
            dangerLevelLeft = totalDangerLevel;
            while(dangerLevelLeft > 0)
            {
                SpawnSubWave();
                yield return new WaitForSeconds(Random.Range(minTimeBetweenSubwaves, maxTimeBetweenSubwaves));
            }
        }

        private void SpawnSubWave()
        {
            int dangerDiff = (int)(totalDangerLevel * subWaveDangerPercentage);
            int startingDangerLevel = dangerLevelLeft;
            while(dangerLevelLeft > startingDangerLevel - dangerDiff)
            {
                BaseEnemy randomEnemy = getRandomEnemy();
                enemySpawningSystem.SpawnEnemy(randomEnemy.gameObject, 1 + waveNum * enemyHpIncrease);
                dangerLevelLeft -= randomEnemy.EnemyData.dangerLevel;
                enemiesAlive++;
            }
        }

        private BaseEnemy getRandomEnemy()
        {
            float randomWeight = Random.value * currentTotalEnemyWeight;
            foreach(BaseEnemy enemy in allowedEnemies)
            {
                if (enemyWeights[enemy] >= randomWeight)
                    return enemy;
                randomWeight -= enemyWeights[enemy];
            }
            return allowedEnemies[allowedEnemies.Count - 1];
        }

        private void CalculateNextDangerLevel()
        {
            totalDangerLevel = (int)(totalDangerLevel * nextWaveDangerLevelMultiplier);
        }

        public void ReportKilled(string enemyType)
        {
            enemiesAlive--;
            if (dangerLevelLeft <= 0 && enemiesAlive <= 0)
                EndWave();
        }

        private void EndWave()
        {
            Debug.Log("Wave ended");
            waveNum++;
            CalculateNextDangerLevel();
            gameStatesSystem.RestingState();
        }
    }
}

