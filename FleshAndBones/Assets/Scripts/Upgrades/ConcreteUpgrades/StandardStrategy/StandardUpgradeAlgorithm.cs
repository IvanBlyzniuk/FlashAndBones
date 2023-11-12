
using System;
using System.Collections.Generic;

namespace App.Upgrades.ConcreteUpgrades.StandardStrategy
{
    public class StandardUpgradeAlgorithm<UpgradableEntity, LevelType> : IUpgradeVisitor<UpgradableEntity>

        where UpgradableEntity : class, IUpgradable
    {
        private readonly UpgradeLevelManager<LevelType> levelManager;
        private readonly IStrategy<UpgradableEntity, LevelType> strategy;
        private UpgradableEntity upgradableEntity;

        public bool IsComplete => levelManager.IsComplete;

        public bool IsEnabled => (upgradableEntity != null);

        public StandardUpgradeAlgorithm(IStrategy<UpgradableEntity, LevelType> strategy, List<LevelType> levelSet)
        {
            this.strategy = strategy;
            levelManager = new (levelSet);
        }

        public void Disable()
        {
            strategy.Reset(upgradableEntity);
            strategy.Destroy();
            upgradableEntity = null;
        }

        public void Enable(UpgradableEntity upgradable)
        {
            upgradableEntity = upgradable;
            strategy.Initialize(upgradableEntity);
            strategy.SwitchToLevel(upgradableEntity, levelManager.CurrentLevel);
        }

        public void LevelUp()
        {
            levelManager.LevelUp();

            if (IsEnabled)
            {
                strategy.SwitchToLevel(upgradableEntity, levelManager.CurrentLevel);
            }
        }
    }
}
