using App.World.Entity.Player.Events;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace App.World.Entity.Player.PlayerComponents
{
    public class Level : MonoBehaviour
    {
        [SerializeField] private List<int> levels;
        [SerializeField] private ValueUpdateEvent onExperienceGained;
        [SerializeField] private ValueUpdateEvent onLevelUp;

        private int currentExperience;
        private int nextLevelExperience;
        private int currentLevel;

        private const int EXPERIENCE_WHEN_FULL_LEVEL = 1;

        public ValueUpdateEvent OnExperienceGained => onExperienceGained;
        public ValueUpdateEvent OnLevelUp => onLevelUp;

        public int MaxLevel => levels.Count;

        public int CurrentLevel
        {
            get 
            { 
                return currentLevel; 
            }
            set 
            {
                if (value > MaxLevel)
                    throw new InvalidOperationException("Too big level.");

                int prevLevel = currentLevel;
                currentLevel = value;
                nextLevelExperience = value >= MaxLevel ? EXPERIENCE_WHEN_FULL_LEVEL : levels[currentLevel];
                OnLevelUp?.CallValueUpdateEvent(prevLevel, currentLevel, MaxLevel);
            }
        }

        private void Awake()
        {
            if (levels.Count == 0)
            {
                throw new ArgumentException("There must be more than 0 levels set.");
            }

            currentExperience = 0;
        }

        private void Start()
        {
            CurrentLevel = 0;
            OnExperienceGained?.CallValueUpdateEvent(0, currentExperience, nextLevelExperience);
        }

        public void IncreaseExperience(int increase)
        {
            int totalExperience = increase + currentExperience;

            while (totalExperience >= nextLevelExperience)
            {
                if (CurrentLevel == MaxLevel)
                {
                    totalExperience = EXPERIENCE_WHEN_FULL_LEVEL;
                    break;
                }

                totalExperience -= nextLevelExperience;
                ++CurrentLevel;
            }

            OnExperienceGained?.CallValueUpdateEvent(currentExperience, totalExperience, nextLevelExperience);
            currentExperience = totalExperience;
        }
    }
}
