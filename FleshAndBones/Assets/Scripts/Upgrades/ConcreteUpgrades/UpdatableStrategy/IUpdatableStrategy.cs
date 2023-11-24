using App.Upgrades.ConcreteUpgrades.StandardStrategy;

namespace App.Upgrades.ConcreteUpgrades.UpdatableStrategy
{
    public interface IUpdatableStrategy<UpgradableEntity, LevelType> : IStrategy<UpgradableEntity, LevelType>
        where UpgradableEntity : class
    {
        void Update(UpgradableEntity upgradableEntity);
    }
}
