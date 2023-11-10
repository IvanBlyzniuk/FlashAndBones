namespace App.Upgrades
{
    public interface Old_IUpgradable
    {
        void EnableUpgrade(Old_BaseUpgrade upgrade);
        void UpdateUpgrade(Old_BaseUpgrade upgrade);
        void DisableUpgrade(Old_BaseUpgrade upgrade);
    }
}
