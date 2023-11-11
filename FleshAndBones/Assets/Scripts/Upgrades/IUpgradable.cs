namespace App.Upgrades
{
    public interface IUpgradable
    {
        void EnableUpgrade(IUpgradeAbstractVisitor upgrade);
        void DisableUpgrade(IUpgradeAbstractVisitor upgrade);
    }
}
