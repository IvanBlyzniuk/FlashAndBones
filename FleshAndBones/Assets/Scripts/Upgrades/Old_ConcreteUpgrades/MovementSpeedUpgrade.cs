using App.World.Entity.Player.PlayerComponents;
using UnityEngine;

namespace App.Upgrades.ConcreteUpgrades
{
    [CreateAssetMenu(fileName = "MovementSpeedUp", menuName = "Scriptable Objects/Upgrades/MovementSpeedUpgrade")]
    public class MovementSpeedUpgrade : Old_BaseUpgrade
    {
        #region Serialized Fields
        [Range(1f, 10f)]
        [SerializeField] private float speedMultiplier;
        #endregion

        #region Overriden methods
        protected override void Upgrade(Player upgradable)
        {
            upgradable.MovementSpeed *= speedMultiplier;
        }

        protected override void Degrade(Player upgradable)
        {
            upgradable.MovementSpeed /= speedMultiplier;
        }

        protected override void UpdateIfEnabled(Player upgradable) { }

        #endregion
    }
}
