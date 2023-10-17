using App.Systems.Wave;
using App.World.Entity.Enemy;
using UnityEngine;

namespace App.Systems.EnemySpawning
{
    public class EnemySpawningSystem : MonoBehaviour
    {
        private IWaveSystem waveSystem;
        private ObjectPool objectPool;
        private Transform enemyTarget;

        [SerializeField]
        private Transform bottomLeftBound;
        [SerializeField]
        private Transform topRightBound;
        public void Init(IWaveSystem waveSystem,ObjectPool objectPool,Transform enemyTarget)
        {
            this.waveSystem = waveSystem;
            this.objectPool = objectPool;
            this.enemyTarget = enemyTarget;
        }

        public void SpawnEnemy(GameObject enemy, float enemyHpMultiplier)
        {
            BaseEnemy baseEnemy = enemy.GetComponent<BaseEnemy>();
            if (baseEnemy == null)
            {
                Debug.Log("Error, trying to create enemy, but gameobject doesn't contain BaseEnemy script");
                return;
            }
            
            
            float angle = Random.Range(0f, 360f);
            Quaternion rotation = Quaternion.Euler(0, 0, angle);
            float aspect = (float)Screen.width / Screen.height;
            float worldHeight = Camera.main.orthographicSize * 2;
            float worldWidth = worldHeight * aspect;
            float distance = Mathf.Sqrt(worldHeight * worldHeight + worldWidth * worldWidth) / 2;
            Vector3 position = rotation * Vector3.up * distance + enemyTarget.position;

            baseEnemy = objectPool.GetObjectFromPool(baseEnemy.PoolObjectType, enemy, position).GetGameObject().GetComponent<BaseEnemy>();
            if(baseEnemy == null)
            {
                Debug.Log("Error, took enemy out of object pool, but didn't find BaseEnemy script on it");
                return;
            }
            baseEnemy.Init(position, enemyTarget, waveSystem, enemyHpMultiplier);
        }
    }
}

