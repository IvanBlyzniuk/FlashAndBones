namespace App.Upgrades
{
    public interface IUpdatableUpgradeVisitor : IUpgradeAbstractVisitor
    {
        void Update();
    }
}
