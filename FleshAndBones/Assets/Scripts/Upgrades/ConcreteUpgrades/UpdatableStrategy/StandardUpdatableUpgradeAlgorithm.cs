using App.Upgrades.ConcreteUpgrades.StandardStrategy;
using System.Collections.Generic;

namespace App.Upgrades.ConcreteUpgrades.UpdatableStrategy
{
    public class StandardUpdatableUpgradeAlgorithm<UpgradableEntity, LevelType>
        : IUpgradeVisitor<UpgradableEntity>
        , IUpdatableUpgradeVisitor

        where UpgradableEntity : class, IUpgradable
    {
        private readonly StandardUpgradeAlgorithm<UpgradableEntity, LevelType> nonUpdatableVisitor;
        private readonly IUpdatableStrategy<UpgradableEntity, LevelType> updatableStrategy;

        public bool IsComplete => nonUpdatableVisitor.IsComplete;

        public bool IsEnabled => nonUpdatableVisitor.IsEnabled;

        public StandardUpdatableUpgradeAlgorithm(IUpdatableStrategy<UpgradableEntity, LevelType> strategy, List<LevelType> levelSet)
        {
            nonUpdatableVisitor = new(strategy, levelSet);
            updatableStrategy = strategy;
        }

        public void Disable()
        {
            nonUpdatableVisitor.Disable();
        }

        public void Enable(UpgradableEntity upgradable)
        {
            nonUpdatableVisitor.Enable(upgradable);
        }

        public void LevelUp()
        {
            nonUpdatableVisitor.LevelUp();
        }

        public void Update()
        {
            updatableStrategy.Update(nonUpdatableVisitor.CurrentUpgradableEntity);
        }
    }
}
