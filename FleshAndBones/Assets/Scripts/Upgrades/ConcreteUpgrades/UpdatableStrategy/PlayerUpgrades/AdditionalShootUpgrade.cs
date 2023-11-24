using App.World.Entity.Player.PlayerComponents;
using App.World.Entity.Player.Weapons;
using UnityEngine;

namespace App.Upgrades.ConcreteUpgrades.UpdatableStrategy.PlayerUpgrades
{
    [System.Serializable]
    public struct AdditionalShootUpgradeLevel
    {
        [Range(0.1f, 20.0f)] public float period;
    }

    [CreateAssetMenu(fileName = "AdditionalShoot", menuName = "Scriptable Objects/Upgrades/AdditionalShootUpgrade")]
    public class AdditionalShootUpgrade : StandardUpdatableUpgradeScriptableObject<Player, AdditionalShootUpgradeLevel>
    {
        public AdditionalShootUpgrade() : base(new PlayerAdditionalShootUpgradeUpgradeStrategy()) {}
    }

    public class PlayerAdditionalShootUpgradeUpgradeStrategy : IUpdatableStrategy<Player, AdditionalShootUpgradeLevel>
    {
        private float period { get; set; }
        private float timeCounter { get; set; } = 0.0f;

        public void Initialize(Player player) {}

        public void Reset(Player player) { }

        public void SwitchToLevel(Player player, AdditionalShootUpgradeLevel level)
        {
            period = level.period;
        }

        public void Update(Player upgradableEntity)
        {
            var shootPosition = upgradableEntity.Weapon.ShootPosition;
            if (timeCounter > period)
            {
                float randomAngle = UnityEngine.Random.Range(0f, 360f);
                Quaternion rotation = Quaternion.Euler(shootPosition.eulerAngles.x, 
                                                       shootPosition.eulerAngles.y, 
                                                       shootPosition.eulerAngles.z + randomAngle);
                BulletGun gun = upgradableEntity.Weapon as BulletGun;
                gun.ShootInDirection(rotation);
                timeCounter = 0f;
            }
            else
            {
                timeCounter += Time.deltaTime;
            }
        }
    }
}
