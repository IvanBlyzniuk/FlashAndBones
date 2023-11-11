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

        public UpgradeLevelManager(List<LevelType> levels, int initialLevelIndex = 0)
        {
            if (levels.Count == 0)
            {
                throw new ArgumentException("Cannot manage an empty level set.");
            }

            if (currentLevelIndex < 0 || currentLevelIndex >= levels.Count) 
            {
                throw new IndexOutOfRangeException($"Input level index is out of level-set's bounds: {initialLevelIndex}");
            }

            this.levels = levels;
            this.currentLevelIndex = initialLevelIndex;
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
