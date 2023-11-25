using UnityEngine;
using App.World.Entity.Player.PlayerComponents;
using System;

namespace App.Upgrades.ConcreteUpgrades.StandardStrategy.PlayerUpgrades
{
    [System.Serializable]
    public struct PiercingBulletsUpgradeLevel
    {
        [Min(0)] public int piercingCountIncrease;
    }

    [CreateAssetMenu(fileName = "PiercingBullets", menuName = "Scriptable Objects/Upgrades/PiercingBulletsUpgrade")]
    public class PiercingBulletsUpgrade : StandardUpgradeScriptableObject<Player, PiercingBulletsUpgradeLevel>
    {
        public PiercingBulletsUpgrade() : base(new PlayerPiercingBulletsUpgradeStrategy()) { }
    }

    public class PlayerPiercingBulletsUpgradeStrategy : IStrategy<Player, PiercingBulletsUpgradeLevel>
    {
        private int? initialPiercingCount = null;

        public void Initialize(Player player)
        {
            initialPiercingCount = player.Weapon.PearcingCount;
        }

        public void Reset(Player player)
        {
            player.Weapon.PearcingCount = initialPiercingCount
                ?? throw new InvalidOperationException("Cannot reset via a non-initialized strategy");
        }

        public void SwitchToLevel(Player player, PiercingBulletsUpgradeLevel level)
        {
            Reset(player);
            player.Weapon.PearcingCount += level.piercingCountIncrease;
        }
    }
}
