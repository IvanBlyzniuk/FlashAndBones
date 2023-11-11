using System;

namespace App.Upgrades
{
    public interface IUpgradeAbstractVisitor
    {
        sealed void Enable(IUpgradable upgradable)
        {
            var guide = @$"
                AVDP Guide:
                Do not call {nameof(IUpgradeAbstractVisitor)}::Enable({nameof(IUpgradable)} upgradable).
                Instead use a dynamic_cast to concrete type implementing {nameof(IUpgradeAbstractVisitor)}, like 
                {typeof(IUpgradeVisitor<>).Name}, and call its Enable method instead to perform a double-dispatch 
                over upgrades.
            ";

            throw new InvalidOperationException(guide);
        }

        void Disable();
        void LevelUp();
        bool IsComplete { get; }
    }
}
