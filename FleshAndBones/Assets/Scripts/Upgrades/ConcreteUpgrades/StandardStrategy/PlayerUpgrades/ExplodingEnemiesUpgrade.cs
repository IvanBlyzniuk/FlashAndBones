using UnityEngine;
using App.World.Entity.Player.PlayerComponents;
using System;
using App.World.Entity.Player.Weapons;
using App.World.Entity.Enemy;
using System.Collections;
using App.World.Entity;
using App.VFX;

namespace App.Upgrades.ConcreteUpgrades.StandardStrategy.PlayerUpgrades
{
    [System.Serializable]
    public struct ExplodingEnemiesUpgradeLevel
    {
        [Range(0.001f, 1f)] public float explosionChance;
        [Range(0.001f, 1f)] public float timeToLive;
        [Min(0f)] public float damage;
        public GameObject explosionPrefab;
    }

    [CreateAssetMenu(fileName = "ExplodingEnemies", menuName = "Scriptable Objects/Upgrades/ExplodingEnemiesUpgrade")]
    public class ExplodingEnemiesUpgrade : StandardUpgradeScriptableObject<Player, ExplodingEnemiesUpgradeLevel>
    {
        public ExplodingEnemiesUpgrade() : base(new PlayerExplodingEnemiesUpgradeStrategy()) { }
    }

    public class PlayerExplodingEnemiesUpgradeStrategy : IStrategy<Player, ExplodingEnemiesUpgradeLevel>
    {
        private ExplodingEnemiesUpgradeLevel currentLevel;

        public void Initialize(Player player)
        {
            Reset(player);
            GetBulletGun(player).OnBulletSpawned += MakeExplosive;
        }

        public void Reset(Player player)
        {
            GetBulletGun(player).OnBulletSpawned -= MakeExplosive;
        }

        public void SwitchToLevel(Player player, ExplodingEnemiesUpgradeLevel level)
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

        private void MakeExplosive(BaseBullet bullet)
        {
            bullet.OnBulletHit += OnEnemyHit;
        }

        private void OnEnemyHit(BaseBullet bullet, BaseEnemy enemy)
        {
            if (UnityEngine.Random.Range(0, 1) <= currentLevel.explosionChance)
            {
                var prefab = currentLevel.explosionPrefab;
                var position = enemy.transform.position;
                GameObject instantiated = GameObject.Instantiate(prefab, position, Quaternion.identity);
                Explosion explosion = instantiated.GetComponent<Explosion>();
                explosion.Init(position, currentLevel.timeToLive, currentLevel.damage);
            }
        }
    }
}
