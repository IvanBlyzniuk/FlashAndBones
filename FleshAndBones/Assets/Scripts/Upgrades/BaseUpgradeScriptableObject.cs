using UnityEngine;

namespace App.Upgrades
{
    public abstract class BaseUpgradeScriptableObject<UpgradableEntity> 
        : ScriptableObject
        , IDisplayableUpgrade
        , IUpgradeVisitor<UpgradableEntity>

        where UpgradableEntity : IUpgradable
    {
        [SerializeField] private string upgradeName;
        [SerializeField] private string description;
        [SerializeField] private Sprite image;

        public Sprite Image => image;

        public string Name => upgradeName;

        public string Description => description;

        public abstract bool IsComplete { get; }

        public abstract bool IsEnabled { get; }

        public abstract void Disable();

        public abstract void Enable(UpgradableEntity upgradable);

        public abstract void LevelUp();
    }
}
