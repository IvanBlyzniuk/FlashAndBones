using App.Systems.EnemySpawning;
using App.Upgrades.ConcreteUpgrades.StandardStrategy.PlayerUpgrades;
using App.World.Entity.Enemy;
using System;
using UnityEngine;
using World.Entity;

namespace App.World.Entity.Player.Weapons
{
    public class BaseBullet : MonoBehaviour, IObjectPoolItem
    {
        protected float damage;
        protected float accuracy;
        private const float DefaultAccuracy = 0.5f;
        protected LifeStealInfo lifeStealAmount;
        protected float pearcingCount;
        protected ObjectPool objectPool;
        [SerializeField]
        protected string poolObjectType;
        protected SlowEffectInfo slowEffect;
        public string PoolObjectType => poolObjectType;

        public virtual void OnTriggerEnter2D(Collider2D collision)
        {
            if (!gameObject.activeSelf)
                return;
            if (collision.gameObject.layer != LayerMask.NameToLayer("Enemy"))
            {
                objectPool.ReturnToPool(this);
                return;
            }

            BaseEnemy enemy = collision.GetComponent<BaseEnemy>();
            if (enemy != null)
            {
                ApplySlowEffect(enemy);
            }

            var a = (DefaultAccuracy + (1 - DefaultAccuracy) * accuracy) + Mathf.Epsilon;
            var b = UnityEngine.Random.Range(0f, 1f);

            if (a <= b)
            {
                //Bullet misses
                return;
            }

            Health targetHealt = collision.GetComponent<Health>();
            if (targetHealt == null)
            {
                return;
            }
            targetHealt.TakeDamage(damage);
            if (lifeStealAmount != null)
            {
                float lifeSteal = damage * lifeStealAmount.lifeStealAmount;
                lifeStealAmount.player.Health.Heal(lifeSteal);
            }

            if (pearcingCount > 0)
            {
                pearcingCount--;
            }
            else
            {
                objectPool.ReturnToPool(this);
            }



        }
        public virtual void Init(float damage, int pearcingCount, float accuracy, LifeStealInfo lifeStealAmount, SlowEffectInfo slowEffect)
        {
            this.damage = damage;
            this.pearcingCount = pearcingCount;
            this.accuracy = accuracy;
            this.lifeStealAmount = lifeStealAmount;
            this.slowEffect = slowEffect;
            GetComponent<TimeToLive>().Init();
        }

        public void GetFromPool(ObjectPool pool)
        {
            objectPool = pool;
            gameObject.SetActive(true);
        }

        public void ReturnToPool()
        {
            gameObject.SetActive(false);
        }

        public GameObject GetGameObject()
        {
            return (gameObject);
        }


        private void ApplySlowEffect(BaseEnemy enemy)
        {
            enemy.ApplySlow(slowEffect.slowMultiplier, slowEffect.slowDuration);

        }

    }
}
