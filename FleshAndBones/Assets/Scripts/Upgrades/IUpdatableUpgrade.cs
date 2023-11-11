namespace App.Upgrades
{
    public interface IUpdatableUpgrade : IUpgradeAbstractVisitor
    {
        void Update();
    }
}
