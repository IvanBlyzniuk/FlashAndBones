using UnityEngine;
using App.World.Entity.Player.PlayerComponents;


namespace App.Upgrades.ConcreteUpgrades.StandardStrategy.PlayerUpgrades
{
    [System.Serializable]
    public struct WeaponCooldownUpgradeLevel
    {
        [Range(0f, 1f)] public float cooldownReductionMultiplier; 
    }
}


namespace App.Upgrades.ConcreteUpgrades.StandardStrategy.PlayerUpgrades
{
    [CreateAssetMenu(fileName = "WeaponCooldownUpgrade", menuName = "Scriptable Objects/Upgrades/WeaponCooldownUpgrade")]
    public class WeaponCooldownUpgrade : StandardUpgradeScriptableObject<Player, WeaponCooldownUpgradeLevel>
    {
        public WeaponCooldownUpgrade() : base(new PlayerWeaponCooldownUpgradeStrategy()) { }
    }

    public class PlayerWeaponCooldownUpgradeStrategy : IStrategy<Player, WeaponCooldownUpgradeLevel>
    {
        private float? initialCooldown = null;

        public void Initialize(Player player)
        {
            if (player.Weapon != null)
                initialCooldown = player.Weapon.Cooldown;
        }

        public void Destroy()
        {
            initialCooldown = null;
        }

        public void Reset(Player player)
        {
            if (player.Weapon != null && initialCooldown.HasValue)
                player.Weapon.Cooldown = initialCooldown.Value;
            else
                throw new System.InvalidOperationException("Cannot reset via a non-initialized strategy");
        }

        public void SwitchToLevel(Player player, WeaponCooldownUpgradeLevel level)
        {
            Reset(player);
            if (player.Weapon != null)
            {
                player.Weapon.Cooldown = initialCooldown.Value * (1f - level.cooldownReductionMultiplier);
            }
        }
    }
}
