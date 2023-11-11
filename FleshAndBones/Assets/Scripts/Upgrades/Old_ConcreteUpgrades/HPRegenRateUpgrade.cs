using App.World.Entity.Player.PlayerComponents;
using UnityEngine;

namespace App.Upgrades.Old_ConcreteUpgrades
{
    [CreateAssetMenu(fileName = "HPRegenRateUp", menuName = "Scriptable Objects/Upgrades/HPRegenRateUpgrade")]
    public class HPRegenRateUpgrade : Old_BaseUpgrade
    {
        #region Serialized Fields
        [Min(0.0f)]
        [SerializeField] private float hpRegenRateAddent;
        #endregion

        #region Non-serialized Fields
        private float timeCounter  = 0.0f;
        private const float period = 1.0f;
        #endregion

        #region Overriden methods
        protected override void Upgrade(Player upgradable) {}

        protected override void UpdateIfEnabled(Player upgradable) 
        {
            var health = upgradable.Health;
            if (timeCounter > period)
            {
                health.Heal(hpRegenRateAddent);
                timeCounter = 0f;
            }
            else
            {
                timeCounter += Time.deltaTime;
            }
        }

        protected override void Degrade(Player upgradable) {}

        #endregion
    }
}
