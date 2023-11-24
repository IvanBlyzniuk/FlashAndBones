using UnityEngine;
using App.World.Entity.Player.PlayerComponents;
using System;

namespace App.Upgrades.ConcreteUpgrades.StandardStrategy.PlayerUpgrades
{
    [System.Serializable]
    public struct DamageUpUpgradeLevel
    {
        [Min(0)] public float damageIncrease;
    }

    [CreateAssetMenu(fileName = "DamageUp", menuName = "Scriptable Objects/Upgrades/DamageUpUpgrade")]
    public class DamageUpUpgrade : StandardUpgradeScriptableObject<Player, DamageUpUpgradeLevel>
    {
        public DamageUpUpgrade() : base(new PlayerDamageUpUpgradeStrategy()) {}
    }

    public class PlayerDamageUpUpgradeStrategy : IStrategy<Player, DamageUpUpgradeLevel>
    {
        private float? initialDamage = null;

        public void Initialize(Player player)
        {
            initialDamage = player.Weapon.Damage;
        }

        public void Reset(Player player)
        {
            player.Weapon.Damage = initialDamage
                ?? throw new InvalidOperationException("Cannot reset via a non-initialized strategy");
        }

        public void SwitchToLevel(Player entity, DamageUpUpgradeLevel level)
        {
            Reset(entity);
            entity.Weapon.Damage += level.damageIncrease;
        }
    }
}
