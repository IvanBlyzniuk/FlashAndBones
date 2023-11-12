using System;
using System.Collections.Generic;
using UnityEngine;

namespace App.Upgrades
{
    [RequireComponent(typeof(IUpgradable))]
    public class UpgradeManager : MonoBehaviour
    {
        #region Fields
        private Dictionary<Type, IUpgradeAbstractVisitor> upgrades; 
        private Dictionary<Type, IUpdatableUpgradeVisitor> updatableUpdates; 
        private IUpgradable upgradableEntity;
        #endregion

        #region MonoBehaviour Methods
        private void Awake()
        {
            upgrades = new();
            updatableUpdates = new(); 
            upgradableEntity = GetComponent<IUpgradable>();
        }

        private void Update()
        {
            UpdateAll();
        }
        #endregion

        #region Custom Methods

        public void EnableAll()
        {
            foreach (var upgrade in upgrades.Values) 
            {
                upgradableEntity.EnableUpgrade(upgrade);
            }

            foreach (var upgrade in updatableUpdates.Values)
            {
                upgradableEntity.EnableUpgrade(upgrade);
            }
        }

        public void UpdateAll()
        {
            foreach (var upgrade in updatableUpdates.Values)
            {
                upgrade.Update();
            }
        }

        public void DisableAll()
        {
            foreach (var upgrade in upgrades.Values)
            {
                upgrade.Disable();
            }

            foreach (var upgrade in updatableUpdates.Values)
            {
                upgrade.Disable();
            }
        }

        public void AddUpgrade(IUpgradeAbstractVisitor upgrade)
        {
            if (upgrade is IUpdatableUpgradeVisitor)
            {
                updatableUpdates.Add(upgrade.GetType(), upgrade as IUpdatableUpgradeVisitor);
            }
            else
            {
                upgrades.Add(upgrade.GetType(), upgrade);
            }

            upgradableEntity.EnableUpgrade(upgrade);
        }

        public void LevelUpUpgrade(IUpgradeAbstractVisitor upgrade)
        {
            IUpgradeAbstractVisitor upgradeToLevelUp = FindUpgrade(upgrade);

            if (upgradeToLevelUp == null)
            {
                throw new ArgumentException("Trying to level-up an upgrade absent in UpgradeManager Collection.");
            }

            if (upgradeToLevelUp.IsComplete)
            {
                throw new InvalidOperationException("Impossible to level-up a complete upgrade.");
            }

            upgradeToLevelUp.LevelUp();
        }

        private IUpgradeAbstractVisitor FindUpgrade(IUpgradeAbstractVisitor upgrade)
        {

            if (upgrades.TryGetValue(upgrade.GetType(), out IUpgradeAbstractVisitor upgradeToLevelUp))
            {
                return upgradeToLevelUp;
            }
            else if (upgrades.TryGetValue(upgrade.GetType(), out IUpgradeAbstractVisitor updatableUpgradeToLevelUp))
            {
                return updatableUpgradeToLevelUp;
            }

            return null;
        }

        #endregion
    }
}
