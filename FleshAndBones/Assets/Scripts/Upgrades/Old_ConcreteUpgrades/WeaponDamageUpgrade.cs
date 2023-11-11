using App.World.Entity.Player.PlayerComponents;
using UnityEngine;

namespace App.Upgrades.Old_ConcreteUpgrades
{
    [CreateAssetMenu(fileName = "DamageUp", menuName = "Scriptable Objects/Upgrades/WeaponDamageUpgrade")]
    public class WeaponDamageUpgrade : Old_BaseUpgrade
    {
        #region Serialized Fields
        [Min(0f)]
        [SerializeField] private float damageAddent;
        #endregion

        #region Overriden Methods
        protected override void Upgrade(Player upgradable)
        {
            upgradable.Weapon.Damage *= damageAddent;
        }

        protected override void Degrade(Player upgradable)
        {
            upgradable.Weapon.Damage /= damageAddent;
        }

        protected override void UpdateIfEnabled(Player upgradable) { }

        #endregion
    }
}
