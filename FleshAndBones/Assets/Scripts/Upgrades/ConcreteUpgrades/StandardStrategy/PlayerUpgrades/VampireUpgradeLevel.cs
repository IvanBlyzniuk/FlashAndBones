using UnityEngine;
using App.World.Entity.Player.PlayerComponents;


namespace App.Upgrades.ConcreteUpgrades.StandardStrategy.PlayerUpgrades
{
    [System.Serializable]
    public struct VampireUpgradeLevel
    {
        [Range(0f, 1f)] public float lifeStealPercentage; 
    }

    [CreateAssetMenu(fileName = "VampireUpgrade", menuName = "Scriptable Objects/Upgrades/VampireUpgrade")]
    public class VampireUpgrade : StandardUpgradeScriptableObject<Player, VampireUpgradeLevel>
    {
        public VampireUpgrade() : base(new PlayerVampireUpgradeStrategy()) { }
    }

    public class PlayerVampireUpgradeStrategy : IStrategy<Player, VampireUpgradeLevel>
    {
        private LifeStealInfo initialLifeStealAmount = null;

        public void Initialize(Player player)
        {
            initialLifeStealAmount = player.Weapon.LifeStealAmount;

        }

        public void Destroy()
        {
            initialLifeStealAmount = null;
        }

        public void Reset(Player player)
        {
            player.Weapon.LifeStealAmount = initialLifeStealAmount ?? throw new System.InvalidOperationException("Cannot reset via a non-initialized strategy");
        }

        public void SwitchToLevel(Player player, VampireUpgradeLevel level)
        {
            Reset(player);
            player.Weapon.LifeStealAmount.lifeStealAmount = level.lifeStealPercentage;
        }


    }

    public class LifeStealInfo
    {
        public float lifeStealAmount;
        public Player player;


    }


}
