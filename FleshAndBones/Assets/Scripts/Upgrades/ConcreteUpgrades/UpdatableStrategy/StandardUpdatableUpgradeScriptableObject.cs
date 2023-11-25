using System.Collections.Generic;
using UnityEngine;

namespace App.Upgrades.ConcreteUpgrades.UpdatableStrategy
{
    public class StandardUpdatableUpgradeScriptableObject<UpgradableEntity, LevelType> 
        : BaseUpgradeScriptableObject<UpgradableEntity>
        , IUpdatableUpgradeVisitor

        where UpgradableEntity : class, IUpgradable
    {
        #region Serialized Fields
        [SerializeField] private List<LevelType> levels;
        #endregion

        #region Fields
        private readonly StandardUpdatableUpgradeAlgorithm<UpgradableEntity, LevelType> upgradeAlgorithm;
        #endregion

        #region Properties
        public override bool IsComplete => upgradeAlgorithm.IsComplete;
        public override bool IsEnabled => upgradeAlgorithm.IsEnabled;
        #endregion

        #region Constructors
        public StandardUpdatableUpgradeScriptableObject(IUpdatableStrategy<UpgradableEntity, LevelType> strategy)
        {
            levels = new();
            upgradeAlgorithm = new(strategy, levels);
        }
        #endregion

        #region Methods
        public override void Disable()
        {
            upgradeAlgorithm.Disable();
        }

        public override void Enable(UpgradableEntity upgradable)
        {
            upgradeAlgorithm.Enable(upgradable);
        }

        public override void LevelUp()
        {
            upgradeAlgorithm.LevelUp();
        }

        public void Update()
        {
            upgradeAlgorithm.Update();
        }
        #endregion
    }
}
