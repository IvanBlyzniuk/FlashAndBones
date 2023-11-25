using UnityEngine;
using App.World.Entity.Player.PlayerComponents;
using System;
using App.World.Entity.Player.Weapons;
using App.World.Entity.Enemy;
using System.Collections;
using App.World.Entity;

namespace App.Upgrades.ConcreteUpgrades.StandardStrategy.PlayerUpgrades
{
    [System.Serializable]
    public struct BurningBulletsUpgradeLevel
    {
        public int burningHitCount;
        public float burningDamage;
        public float hitPeriod;
    }

    [CreateAssetMenu(fileName = "BurningBullets", menuName = "Scriptable Objects/Upgrades/BurningBulletsUpgrade")]
    public class BurningBulletsUpgrade : StandardUpgradeScriptableObject<Player, BurningBulletsUpgradeLevel>
    {
        public BurningBulletsUpgrade() : base(new PlayerBurningBulletsUpgradeStrategy()) { }
    }

    public class PlayerBurningBulletsUpgradeStrategy : IStrategy<Player, BurningBulletsUpgradeLevel>
    {
        private BurningBulletsUpgradeLevel currentLevel;

        public void Initialize(Player player)
        {
            Reset(player);
            GetBulletGun(player).OnBulletSpawned += MakeBurning;
        }

        public void Reset(Player player)
        {
            GetBulletGun(player).OnBulletSpawned -= MakeBurning;
        }

        public void SwitchToLevel(Player player, BurningBulletsUpgradeLevel level)
        {
            currentLevel = level;
        }

        private BulletGun GetBulletGun(Player player)
        {
            if (player.Weapon is not BulletGun)
            {
                throw new InvalidOperationException("Cannot apply Burning Bullets upgrade to a non-bullet gun.");
            }

            return player.Weapon as BulletGun;
        }

        private void MakeBurning(BaseBullet bullet)
        {
            bullet.OnBulletHit += OnEnemyHit;
        }

        private void OnEnemyHit(BaseBullet bullet, BaseEnemy enemy)
        {
            Health enemyHealth = enemy.GetComponent<Health>();
            enemy.StartCoroutine(ApplyBurningEffect(enemyHealth));

            if (bullet.PearcingCount <= 0)
            {
                bullet.OnBulletHit -= OnEnemyHit;
            }
        }

        private IEnumerator ApplyBurningEffect(Health health)
        {
            int burningHitCount = currentLevel.burningHitCount;
            float damage = currentLevel.burningDamage;
            float hitPeriod = currentLevel.hitPeriod;
            
            for (int i = 0; i < burningHitCount; ++i)
            {
                if (health == null)
                {
                    yield break;
                }

                health.TakeDamage(damage);
                yield return new WaitForSeconds(hitPeriod);
            }
        }
    }
}
