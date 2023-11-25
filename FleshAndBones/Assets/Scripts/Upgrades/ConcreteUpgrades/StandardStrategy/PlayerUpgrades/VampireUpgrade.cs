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

        public void Initialize(Player player)
        {

        }


        public void Reset(Player player)
        {
            player.Weapon.LifeStealAmount = null;
        }

        public void SwitchToLevel(Player entity, VampireUpgradeLevel level)
        {
            var lifeStealInfo = new LifeStealInfo {
                lifeStealAmount = level.lifeStealPercentage, 
                player = entity };

            entity.Weapon.LifeStealAmount = lifeStealInfo;
        }


    }

    public class LifeStealInfo
    {
        public float lifeStealAmount;
        public Player player;


    }


}
