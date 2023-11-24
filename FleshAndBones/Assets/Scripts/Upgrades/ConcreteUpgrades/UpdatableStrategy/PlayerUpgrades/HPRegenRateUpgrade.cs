using App.World.Entity.Player.PlayerComponents;
using UnityEngine;

namespace App.Upgrades.ConcreteUpgrades.UpdatableStrategy.PlayerUpgrades
{
    [System.Serializable]
    public struct HpRegenRateUpgradeLevel
    {
        [Min(0.0f)] public float hpRegenRateAddent;
        [Min(0.0f)] public float period;
    }

    [CreateAssetMenu(fileName = "HPRegenRate", menuName = "Scriptable Objects/Upgrades/HPRegenRateUpgrade")]
    public class HPRegenRateUpgrade : StandardUpdatableUpgradeScriptableObject<Player, HpRegenRateUpgradeLevel>
    {
        public HPRegenRateUpgrade() : base(new HPRegenRateUpgradeUpgradeStrategy()) { }
    }

    public class HPRegenRateUpgradeUpgradeStrategy : IUpdatableStrategy<Player, HpRegenRateUpgradeLevel>
    {
        private float hpRegenRateAddent { get; set; }
        private float period { get; set; }
        private float timeCounter { get; set; } = 0.0f;

        public void Initialize(Player player)
        {
            Debug.Log("Enabled");
        }

        public void Reset(Player player) {}

        public void SwitchToLevel(Player player, HpRegenRateUpgradeLevel level)
        {
            hpRegenRateAddent = level.hpRegenRateAddent;
            period = level.period;  
        }

        public void Update(Player upgradableEntity)
        {
            var health = upgradableEntity.Health;
            if (timeCounter > period)
            {
                health.Heal(hpRegenRateAddent);
                timeCounter = 0f;
                Debug.Log("Healed");
            }
            else
            {
                timeCounter += Time.deltaTime;
            }
        }
    }
}
