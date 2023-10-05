using App.World.Entity.Player.PlayerComponents;
using UnityEngine;

namespace App.Upgrades.ConcreteUpgrades
{
    [CreateAssetMenu(fileName = "MaxHPUpgrade", menuName = "Scriptable Objects/Upgrades/MaxHPUpgrade")]
    public class MaxHPUpgrade : BaseUpgrade
    {
        #region Serialized Fields
        [Min(0f)]
        [SerializeField] private float maxHPAddent;
        #endregion

        #region Overriden Methods
        protected override void Upgrade(Player upgradable)
        {
            upgradable.Health.MaxHealth += maxHPAddent;
        }

        protected override void Degrade(Player upgradable)
        {
            upgradable.Health.MaxHealth -= maxHPAddent;
        }

        protected override void UpdateIfEnabled(Player upgradable) { }

        #endregion
    }
}
