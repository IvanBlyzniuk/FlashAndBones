using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Rendering.Universal.Internal;

namespace App.Upgrades.ConcreteUpgrades.StandardStrategy
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
