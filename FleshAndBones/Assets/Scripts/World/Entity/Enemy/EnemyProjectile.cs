using App.Systems.EnemySpawning;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.World.Entity.Enemy
{
    public class EnemyProjectile : MonoBehaviour, IObjectPoolItem
    {
        [SerializeField]
        private DamagePlayer damagePlayer;
        [SerializeField]
        private float velocity;
        private ObjectPool objectPool;
        public string PoolObjectType => "Enemy Projectile";

        public void Init(Vector3 position,Vector3 rotation)
        {
            transform.position = position;
            Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
            rigidbody.velocity = rotation * velocity;
        }

        public void GetFromPool(ObjectPool pool)
        {
            objectPool = pool;
            gameObject.SetActive(true);
        }

        public GameObject GetGameObject()
        {
            return gameObject;
        }

        public void ReturnToPool()
        {
            gameObject.SetActive(false);
        }

        public void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log("Return projectile to pool");
            objectPool.ReturnToPool(this);
        }
    }
}
