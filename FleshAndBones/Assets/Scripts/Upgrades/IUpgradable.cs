using System;

namespace App.Upgrades
{
    public interface IUpgradable
    {
        void EnableUpgrade(IUpgradeAbstractVisitor upgrade);

        public static void EnableUpgradeViaVisitorOf<UpgradableEntity>(UpgradableEntity self, IUpgradeAbstractVisitor visitor)
            where UpgradableEntity : class, IUpgradable
        {
            if (visitor is IUpgradeVisitor<UpgradableEntity>)
            {
                IUpgradeVisitor<UpgradableEntity> castedVisitor = visitor as IUpgradeVisitor<UpgradableEntity>;
                castedVisitor.Enable(self); // do visit
            }
            else
            {
                throw new InvalidOperationException($"Input abstract visitor does not represent a visitor to upgrade {self.GetType()}.");
            }
        }
    }
}
