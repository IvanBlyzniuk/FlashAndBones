using UnityEngine;
using App.World.Entity.Player.PlayerComponents;
using System;
using App.World.Entity.Player.Weapons;

namespace App.Upgrades.ConcreteUpgrades.StandardStrategy.PlayerUpgrades
{
    [System.Serializable]
    public struct BigBulletsUpgradeLevel
    {
        public Vector3 bulletScale;
    }

    [CreateAssetMenu(fileName = "BigBullets", menuName = "Scriptable Objects/Upgrades/BigBulletsUpgrade")]
    public class BigBulletsUpgrade : StandardUpgradeScriptableObject<Player, BigBulletsUpgradeLevel>
    {
        public BigBulletsUpgrade() : base(new PlayerBigBulletsUpgradeStrategy()) { }
    }

    public class PlayerBigBulletsUpgradeStrategy : IStrategy<Player, BigBulletsUpgradeLevel>
    {
        private Vector3 currentScaleMultiplier;

        public void Initialize(Player player) 
        {
            Reset(player);
            GetBulletGun(player).OnBulletSpawned += MakeBig;
        }

        public void Reset(Player player) 
        {
            currentScaleMultiplier = Vector3.one;
            GetBulletGun(player).OnBulletSpawned -= MakeBig;
        }

        public void SwitchToLevel(Player player, BigBulletsUpgradeLevel level)
        {
            currentScaleMultiplier = level.bulletScale;
        }
        
        private BulletGun GetBulletGun(Player player)
        {
            if (player.Weapon is not BulletGun)
            {
                throw new InvalidOperationException("Cannot apply Big Bullets upgrade to a non-bullet gun.");
            }

            return player.Weapon as BulletGun;
        }

        private void MakeBig(BaseBullet bullet)
        {
            bullet.gameObject.transform.localScale = currentScaleMultiplier;
            Debug.Log($"Next Scale = {currentScaleMultiplier}");
            Debug.Log($"Actual Scale = {bullet.gameObject.transform.localScale}");
        }
    }
}
