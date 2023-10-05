using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using App.Systems.EnemySpawning;
using World.Entity;
using App.World.Entity;

namespace App.VFX
{
    public class Explosion : MonoBehaviour, IObjectPoolItem
    {
        public string PoolObjectType => "Explosion";
        private ObjectPool pool;
        private float damage;
        private PolygonCollider2D damageZone;
        [SerializeField]
        private ParticleSystem explosionParticles;
        private AudioSource explosionSound;
        public void GetFromPool(ObjectPool pool)
        {
            this.pool = pool;
            gameObject.SetActive(true);
        }
        public void ReturnToPool()
        {
            gameObject.SetActive(false);
        }
        public GameObject GetGameObject()
        {
            return gameObject;
        }

        public void Init(Vector3 pos, float timeToLive, float damage)
        {
            transform.position = pos;
            this.damage = damage;
            explosionSound = GetComponent<AudioSource>();
            StartCoroutine(Explode(timeToLive));

        }
        public virtual void OnTriggerEnter2D(Collider2D collision)
        {
            if (!gameObject.activeSelf)
                return;
            Health targetHealt = collision.GetComponent<Health>();
            if (targetHealt == null)
            {
                Debug.Log("No Health component on explosion target");
                return;
            }
            targetHealt.TakeDamage(damage);
        }

        public IEnumerator Explode(float time)
        {
            explosionParticles.Play();
            explosionSound.Play();
            yield return new WaitForSeconds(time);
            pool.ReturnToPool(this);
        }
    }

}

