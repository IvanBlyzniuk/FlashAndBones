namespace App.Upgrades.ConcreteUpgrades.StandardStrategy
{
    public interface IUpdatableStrategy<UpgradableEntity, LevelType> : IStrategy<UpgradableEntity, LevelType>
        where UpgradableEntity : class
    {
        void Update(UpgradableEntity upgradableEntity);
    }
}
