namespace App.Upgrades
{
    public interface IUpgradable
    {
        void EnableUpgrade(BaseUpgrade upgrade);
        void UpdateUpgrade(BaseUpgrade upgrade);
        void DisableUpgrade(BaseUpgrade upgrade);
    }
}
