using App.World.Entity.Player.PlayerComponents;
using System;
using UnityEngine;

namespace App.Upgrades.ConcreteUpgrades.StandardStrategy.PlayerUpgrades
{
    [System.Serializable]
    public struct NewUpgradeLevel
    {
        [Range(1f, 10f)] public float vampirizm;
    }

    [CreateAssetMenu(fileName = "NewUpgrade", menuName = "Scriptable Objects/Upgrades/NewUpgrade")]
    public class NewUpgrade : StandardUpgradeScriptableObject<Player, NewUpgradeLevel>
    {
        public NewUpgrade() : base(new NewUpgradeStrategy()) { }
    }

    public class NewUpgradeStrategy : IStrategy<Player, NewUpgradeLevel>
    {
        public void Initialize(Player player)
        {
        }

        public void Reset(Player player)
        {
        }

        public void SwitchToLevel(Player player, NewUpgradeLevel level)
        {
        }
    }
}
