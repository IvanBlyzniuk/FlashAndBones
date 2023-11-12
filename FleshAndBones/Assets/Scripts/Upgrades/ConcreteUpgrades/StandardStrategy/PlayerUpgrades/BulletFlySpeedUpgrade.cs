using UnityEngine;
using App.World.Entity.Player.PlayerComponents;
using System;

namespace App.Upgrades.ConcreteUpgrades.StandardStrategy.PlayerUpgrades
{
    [System.Serializable] 
    public struct BulletFlySpeedUpgradeLevel
    {
        [Range(1f, 10f)] public float bulletFlySpeedMultiplier;
    }
    
    [CreateAssetMenu(fileName = "BulletSpeedUp", menuName = "Scriptable Objects/Upgrades/BulletFlySpeedUpgrade")]
    public class BulletFlySpeedUpgrade : StandardUpgradeScriptableObject<Player, BulletFlySpeedUpgradeLevel>
    {
        public BulletFlySpeedUpgrade() : base(new PlayerBulletFlySpeedUpgradeStrategy()) {}
    }

    public class PlayerBulletFlySpeedUpgradeStrategy : IStrategy<Player, BulletFlySpeedUpgradeLevel>
    {
        private float? initialBulletFlySpeed = null;

        public void Initialize(Player player)
        {
            initialBulletFlySpeed = player.Weapon.BulletFlySpeed;
        }

        public void Destroy()
        {
            initialBulletFlySpeed = null;
        }

        public void Reset(Player player)
        {
            player.Weapon.BulletFlySpeed = initialBulletFlySpeed 
                ?? throw new InvalidOperationException("Cannot reset via a non-initialized strategy");
        }

        public void SwitchToLevel(Player player, BulletFlySpeedUpgradeLevel level)
        {
            Reset(player);
            player.Weapon.BulletFlySpeed *= level.bulletFlySpeedMultiplier;
        }
    }
}
