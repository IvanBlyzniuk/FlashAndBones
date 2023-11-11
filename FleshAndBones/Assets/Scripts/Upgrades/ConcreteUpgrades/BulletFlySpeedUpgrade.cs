using App.World.Entity.Player.PlayerComponents;
using System.Collections.Generic;
using UnityEngine;


namespace App.Upgrades.ConcreteUpgrades
{
    [CreateAssetMenu(fileName = "BulletSpeedUp", menuName = "Scriptable Objects/Upgrades/BulletFlySpeedUpgrade")]
    public class BulletFlySpeedUpgrade : BaseUpgradeScriptableObject<Player>
    {
        [System.Serializable] struct UpgradeLevel
        {
            [Range(1f, 10f)] public float bulletFlySpeedMultiplier;
        }

        #region Serialized Fields
        [SerializeField] private string description;
        [SerializeField] private List<UpgradeLevel> levels;
        #endregion

        #region Fields
        private Player upgradableEntity;
        private UpgradeLevelManager<UpgradeLevel> levelManager;
        private float initialBulletFlySpeed;
        #endregion

        #region Properties
        public override string Description => description;
        public override bool IsComplete => levelManager.IsComplete;
        #endregion

        #region Upgrading Itself
        public override void Enable(Player upgradable)
        {
            upgradableEntity = upgradable;
            initialBulletFlySpeed = upgradableEntity.Weapon.BulletFlySpeed;
        }

        public override void Disable()
        {
            upgradableEntity = null;
            upgradableEntity.Weapon.BulletFlySpeed = initialBulletFlySpeed;
            initialBulletFlySpeed = 0.0f;
        }

        public override void LevelUp()
        {
            levelManager.LevelUp();
            UpgradeLevel nextLevel = levelManager.CurrentLevel;
            upgradableEntity.Weapon.BulletFlySpeed *= nextLevel.bulletFlySpeedMultiplier;
        }
        #endregion

        public BulletFlySpeedUpgrade()
        {
            levels = new();
            upgradableEntity = null;
            initialBulletFlySpeed = 0.0f;
            levelManager = new(levels);
        }
    }
}
