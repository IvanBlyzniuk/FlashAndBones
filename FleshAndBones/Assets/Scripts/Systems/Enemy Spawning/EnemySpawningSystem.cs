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
            Vector3 position = new Vector3();
            position.x = Random.Range(bottomLeftBound.position.x, topRightBound.position.x);
            position.y = Random.Range(bottomLeftBound.position.y, topRightBound.position.y);
            position.z = 0;
            baseEnemy = objectPool.GetObjectFromPool(baseEnemy.PoolObjectType, enemy, position).GetGameObject().GetComponent<BaseEnemy>();
            if(baseEnemy == null)
            {
                Debug.Log("Error, took enemy out of object pool, but didn't find BaseEnemy script on it");
                return;
            }
            baseEnemy.Init(position, enemyTarget,waveSystem,enemyHpMultiplier);
        }
    }
}

