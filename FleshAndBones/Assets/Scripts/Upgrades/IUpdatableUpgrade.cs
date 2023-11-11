namespace App.Upgrades
{
    public interface IUpdatableUpgrade : IUpgrade
    {
        void Update();
    }
}
