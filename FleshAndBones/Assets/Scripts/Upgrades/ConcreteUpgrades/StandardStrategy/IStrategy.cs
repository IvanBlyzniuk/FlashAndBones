using System.Collections.Generic;

namespace App.Upgrades.ConcreteUpgrades.StandardStrategy
{
    public interface IStrategy<UpgradableEntity, LevelType> where UpgradableEntity : class
    {
        void Initialize(UpgradableEntity upgradableEntity);
        void Reset(UpgradableEntity entity);
        void SwitchToLevel(UpgradableEntity entity, LevelType level);
    }
}
