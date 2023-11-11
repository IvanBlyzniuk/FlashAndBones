using Unity.VisualScripting;

namespace App.Upgrades
{
    public interface IUpgrade
    {
        void Enable(IUpgradable upgradable);
        void Disable(IUpgradable upgradable);
        void LevelUp();
        bool IsComplete { get; }
    }
}
