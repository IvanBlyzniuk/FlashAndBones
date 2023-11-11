using App.World.Entity.Player.PlayerComponents;
using System.Collections.Generic;
using UnityEngine;


namespace App.Upgrades.ConcreteUpgrades
{
    [CreateAssetMenu(fileName = "BulletSpeedUp", menuName = "Scriptable Objects/Upgrades/BulletFlySpeedUpgrade")]
    public class BulletFlySpeedUpgrade : ScriptableObject, IDisplayableUpgrade, IUpgradeAbstractVisitor, IUpgradeVisitor<Player>
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
        public string Description => description;
        public bool IsComplete => levelManager.IsComplete;
        #endregion

        #region Upgrading Itself
        public void Enable(Player upgradable)
        {
            upgradableEntity = upgradable;
            initialBulletFlySpeed = upgradableEntity.Weapon.BulletFlySpeed;
        }

        public void Disable()
        {
            upgradableEntity = null;
            upgradableEntity.Weapon.BulletFlySpeed = initialBulletFlySpeed;
            initialBulletFlySpeed = 0.0f;
        }

        public void LevelUp()
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
