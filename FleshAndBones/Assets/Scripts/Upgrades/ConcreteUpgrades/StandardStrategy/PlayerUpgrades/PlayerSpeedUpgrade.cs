using UnityEngine;
using App.World.Entity.Player.PlayerComponents;

namespace App.Upgrades.ConcreteUpgrades.StandardStrategy.PlayerUpgrades
{
    [System.Serializable]
    public struct PlayerSpeedUpgradeLevel
    {
        [Range(1f, 5f)] 
        public float speedMultiplier;
    }

    [CreateAssetMenu(fileName = "PlayerSpeedUpgrade", menuName = "Scriptable Objects/Upgrades/PlayerSpeedUpgrade")]
    public class PlayerSpeedUpgrade : StandardUpgradeScriptableObject<Player, PlayerSpeedUpgradeLevel>
    {
        public PlayerSpeedUpgrade() : base(new PlayerSpeedUpgradeStrategy()) { }
    }

    public class PlayerSpeedUpgradeStrategy : IStrategy<Player, PlayerSpeedUpgradeLevel>
    {
        private float? initialSpeed = null;

        public void Initialize(Player player)
        {
            initialSpeed = player.MovementSpeed;
        }

        public void Destroy()
        {
            initialSpeed = null;
        }

        public void Reset(Player player)
        {
            if (player != null && initialSpeed.HasValue)
                player.MovementSpeed = initialSpeed.Value;
            else
                throw new System.InvalidOperationException("Cannot reset via a non-initialized strategy");
        }

        public void SwitchToLevel(Player player, PlayerSpeedUpgradeLevel level)
        {
            Reset(player);
            if (player != null)
            {
                player.MovementSpeed *= level.speedMultiplier;
            }
        }
    }
}
