namespace App.Upgrades
{
    public interface IUpgradable
    {
        void EnableUpgrade(IUpgrade upgrade);
        void DisableUpgrade(IUpgrade upgrade);
    }
}
