using System;
using System.Collections.Generic;

namespace App.Upgrades
{
    public class UpgradeLevelManager<LevelType>
    {
        private readonly List<LevelType> levels;
        private int currentLevelIndex;

        public bool IsComplete { get => currentLevelIndex == levels.Count - 1; }
        public LevelType CurrentLevel => levels[currentLevelIndex];

        public UpgradeLevelManager(List<LevelType> levels)
        {
            this.levels = levels;
            currentLevelIndex = 0;
        }

        public void LevelUp()
        {
            if (IsComplete)
            {
                throw new InvalidOperationException("Cannot level up an upgrade in a complete state.");
            }

            ++currentLevelIndex;
        }
    }
}
