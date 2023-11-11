namespace App.Upgrades
{
    public interface IUpgradeVisitor<UpgradableEntity> : IUpgradeAbstractVisitor
        where UpgradableEntity : IUpgradable
    {
        void Enable(UpgradableEntity upgradable);
    }
}
