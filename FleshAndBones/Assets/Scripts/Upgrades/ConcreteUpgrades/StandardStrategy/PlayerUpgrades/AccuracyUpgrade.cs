using UnityEngine;
using App.World.Entity.Player.PlayerComponents;
using System;

namespace App.Upgrades.ConcreteUpgrades.StandardStrategy.PlayerUpgrades
{
    [System.Serializable]
    public struct AccuracyUpgradeLevel
    {
        [Range(0f, 1f)] public float accuracy;
    }

    [CreateAssetMenu(fileName = "Accuracy", menuName = "Scriptable Objects/Upgrades/AccuracyUpgrade")]
    public class AccuracyUpgrade : StandardUpgradeScriptableObject<Player, AccuracyUpgradeLevel>
    {
        public AccuracyUpgrade() : base(new PlayerAccuracyUpgradeStrategy()) {}
    }

    public class PlayerAccuracyUpgradeStrategy : IStrategy<Player, AccuracyUpgradeLevel>
    {
        private float? initialBulletSpread = null;

        public void Initialize(Player player)
        {
            initialBulletSpread = player.Weapon.BullerSpread;
        }

        public void Destroy()
        {
            initialBulletSpread = null;
        }

        public void Reset(Player player)
        {
            player.Weapon.BullerSpread = initialBulletSpread
                ?? throw new InvalidOperationException("Cannot reset via a non-initialized strategy");
        }

        public void SwitchToLevel(Player player, AccuracyUpgradeLevel level)
        {
            Reset(player);
            player.Weapon.BullerSpread *= (1 - level.accuracy);
        }
    }
}
