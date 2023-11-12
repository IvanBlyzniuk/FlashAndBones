using App.World.Entity.Player.PlayerComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace App.Upgrades
{
    public abstract class BaseUpgradeScriptableObject<UpgradableEntity> 
        : ScriptableObject
        , IDisplayableUpgrade
        , IUpgradeVisitor<UpgradableEntity>

        where UpgradableEntity : IUpgradable
    {
        public abstract string Description { get; }

        public abstract bool IsComplete { get; }

        public abstract bool IsEnabled { get; }

        public abstract void Disable();

        public abstract void Enable(UpgradableEntity upgradable);

        public abstract void LevelUp();
    }
}
