using App.World.Entity.Player.PlayerComponents;
using UnityEngine;

namespace App.Upgrades.Old_ConcreteUpgrades
{
    [CreateAssetMenu(fileName = "WpnCooldownUp", menuName = "Scriptable Objects/Old_Upgrades/WeaponCooldownUpgrade")]
    public class WeaponCooldownUpgrade : Old_BaseUpgrade
    {
        #region Serialized Fields
        [Range(0f, 1f)]
        [SerializeField] private float cooldownMultiplier;
        #endregion

        #region Overriden Methods
        protected override void Upgrade(Player upgradable)
        {
            upgradable.Weapon.Cooldown *= cooldownMultiplier;
        }

        protected override void Degrade(Player upgradable)
        {
            upgradable.Weapon.Cooldown /= cooldownMultiplier;
        }

        protected override void UpdateIfEnabled(Player upgradable) {}

        #endregion
    }
}
