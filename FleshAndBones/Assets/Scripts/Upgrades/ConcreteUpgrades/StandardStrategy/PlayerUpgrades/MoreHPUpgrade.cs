using UnityEngine;
using App.World.Entity.Player.PlayerComponents;
using System;

namespace App.Upgrades.ConcreteUpgrades.StandardStrategy.PlayerUpgrades
{
    [System.Serializable]
    public struct MoreHPUpgradeLevel
    {
        [Min(0)] public float maxHP;
    }

    [CreateAssetMenu(fileName = "MoreHP", menuName = "Scriptable Objects/Upgrades/MoreHPUpgrade")]
    public class MoreHPUpgrade : StandardUpgradeScriptableObject<Player, MoreHPUpgradeLevel>
    {
        public MoreHPUpgrade() : base(new PlayerMoreHPUpgradeStrategy()) {}
    }

    public class PlayerMoreHPUpgradeStrategy : IStrategy<Player, MoreHPUpgradeLevel>
    {
        private float? initialMaxHP = null;

        public void Initialize(Player player)
        {
            initialMaxHP = player.Health.MaxHealth;
        }

        public void Reset(Player player)
        {
            player.Health.MaxHealth = initialMaxHP
                ?? throw new InvalidOperationException("Cannot reset via a non-initialized strategy");
        }

        public void SwitchToLevel(Player entity, MoreHPUpgradeLevel level)
        {
            Reset(entity);
            entity.Health.MaxHealth = level.maxHP;
        }
    }
}
