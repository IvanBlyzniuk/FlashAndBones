using UnityEngine;
using App.World.Entity.Player.PlayerComponents;

namespace App.Upgrades.ConcreteUpgrades.StandardStrategy.PlayerUpgrades
{
    [System.Serializable]
    public struct DexterityUpgradeLevel
    {
        [Range(0f, 1f)] public float dodgeChanceIncrease;
    }

    [CreateAssetMenu(fileName = "DexterityUpgrade", menuName = "Scriptable Objects/Upgrades/DexterityUpgrade")]
    public class DexterityUpgrade : StandardUpgradeScriptableObject<Player, DexterityUpgradeLevel>
    {
        public DexterityUpgrade() : base(new PlayerDexterityUpgradeStrategy()) { }
    }

    public class PlayerDexterityUpgradeStrategy : IStrategy<Player, DexterityUpgradeLevel>
    {
        private float? initialDodgeChance = null;

        public void Initialize(Player player)
        {
            initialDodgeChance = player.DodgeChance;
        }

        public void Destroy()
        {
            initialDodgeChance = null;
        }

        public void Reset(Player player)
        {
            player.DodgeChance = initialDodgeChance ?? throw new System.InvalidOperationException("Cannot reset via a non-initialized strategy");
        }

        public void SwitchToLevel(Player player, DexterityUpgradeLevel level)
        {
            Reset(player);
            player.DodgeChance += initialDodgeChance.Value * level.dodgeChanceIncrease;
        }
    }
}
