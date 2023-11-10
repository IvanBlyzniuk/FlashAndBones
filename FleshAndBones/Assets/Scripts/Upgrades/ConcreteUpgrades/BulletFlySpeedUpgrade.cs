using App.World.Entity.Player.PlayerComponents;
using System;
using UnityEngine;

namespace App.Upgrades.ConcreteUpgrades
{
    [CreateAssetMenu(fileName = "BulletSpeedUp", menuName = "Scriptable Objects/Upgrades/BulletFlySpeedUpgrade")]
    public class BulletFlySpeedUpgrade : Old_BaseUpgrade
    {
        #region Serialized Fields
        [Range(1f, 10f)]
        [SerializeField] private float bulletFlySpeedMultiplier;
        #endregion

        #region Overriden Methods
        protected override void Upgrade(Player upgradable)
        {
            upgradable.Weapon.BulletFlySpeed *= bulletFlySpeedMultiplier;
        }

        protected override void Degrade(Player upgradable)
        {
            upgradable.Weapon.BulletFlySpeed /= bulletFlySpeedMultiplier;
        }

        protected override void UpdateIfEnabled(Player upgradable) { }

        #endregion
    }
}
