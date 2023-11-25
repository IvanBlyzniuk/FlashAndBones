using UnityEngine;
using App.World.Entity.Player.PlayerComponents;
using App.World.Entity.Player.Weapons;

namespace App.Upgrades.ConcreteUpgrades.StandardStrategy.PlayerUpgrades
{
    [System.Serializable]
    public struct SlowEffectInfo
    {
        [Range(0f, 1f)] public float slowMultiplier;
        [Range(0f, 10f)] public float slowDuration;
    }

    [CreateAssetMenu(fileName = "SlowEffectUpgrade", menuName = "Scriptable Objects/Upgrades/SlowEffectUpgrade")]
    public class SlowEffectUpgrade : StandardUpgradeScriptableObject<Player, SlowEffectInfo>
    {
        public SlowEffectUpgrade() : base(new PlayerSlowEffectUpgradeStrategy()) { }
    }

    public class PlayerSlowEffectUpgradeStrategy : IStrategy<Player, SlowEffectInfo>
    {
        private SlowEffectInfo initialSlowEffect = new SlowEffectInfo();

        public void Initialize(Player player)
        {
            if (player.Weapon != null)
                initialSlowEffect = player.Weapon.SlowEffect;
        }

        public void Destroy()
        {
            initialSlowEffect.slowMultiplier = 0;
            initialSlowEffect.slowDuration = 0;
        }

        public void Reset(Player player)
        {
            if (player.Weapon != null)
                player.Weapon.SlowEffect = initialSlowEffect;
            else
                throw new System.InvalidOperationException("Cannot reset via a non-initialized strategy");
        }

        public void SwitchToLevel(Player player, SlowEffectInfo level)
        {
            Reset(player);
            if (player.Weapon != null)
            {
                player.Weapon.SlowEffect = new SlowEffectInfo
                {
                    slowMultiplier = level.slowMultiplier,
                    slowDuration = level.slowDuration
                };
            }
        }
    }

 
    
}
