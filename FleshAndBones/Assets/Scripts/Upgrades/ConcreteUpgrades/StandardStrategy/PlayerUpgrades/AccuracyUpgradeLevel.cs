using UnityEngine;
using App.World.Entity.Player.PlayerComponents;
using System;

namespace App.Upgrades.ConcreteUpgrades.StandardStrategy.PlayerUpgrades
{
    [System.Serializable]
    public struct AccuracyUpgradeLevel
    {
        [Range(0f, 1f)] public float accuracyIncrease;
    }

    [CreateAssetMenu(fileName = "AccuracyUpgrade", menuName = "Scriptable Objects/Upgrades/AccuracyUpgrade")]
    public class AccuracyUpgrade : StandardUpgradeScriptableObject<Player, AccuracyUpgradeLevel>
    {
        public AccuracyUpgrade() : base(new PlayerAccuracyUpgradeStrategy()) { }
    }

    public class PlayerAccuracyUpgradeStrategy : IStrategy<Player, AccuracyUpgradeLevel>
    {
        private float? initialAccuracy = null;

        public void Initialize(Player player)
        {
            if (player.Weapon != null)
                initialAccuracy = player.Weapon.Accuracy;
        }

        public void Destroy()
        {
            initialAccuracy = null;
        }

        public void Reset(Player player)
        {
            if (player.Weapon != null && initialAccuracy.HasValue)
                player.Weapon.Accuracy = initialAccuracy.Value;
            else
                throw new InvalidOperationException("Cannot reset via a non-initialized strategy");
        }

        public void SwitchToLevel(Player player, AccuracyUpgradeLevel level)
        {
            if (player.Weapon != null)
            {
                Reset(player);
                player.Weapon.Accuracy = initialAccuracy.Value * level.accuracyIncrease;
            }
        }
    }
}
