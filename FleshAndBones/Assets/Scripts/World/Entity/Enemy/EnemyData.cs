using App.World.Items;
using System.Collections.Generic;
using UnityEngine;

namespace App.World.Entity.Enemy
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/Enemies/ Enemy Data")]
    public class EnemyData : ScriptableObject
    {
        public string type;
        public int maxHealth;
        public int dangerLevel;
        public int spawnStartTime;
        public float speed;
        public float damage;
        public float timeBetweenAttacks;
        public float healingDropChance;
        public float attackRange;
        public float spawnAnimationDuration;
        public int minExpDrop;
        public int maxExpDrop;
        public ExperienceDropItem experiencePrefab;
        public HealingDropItem healingPrefab;
        public float minTimeBetweenGrunts = 10f;
        public float maxTimeBetweenGrunts = 15f;
        public List<AudioClip> gruntSounds;
        public List<AudioClip> attackSounds;
        public int experienceForKill;
    }
}

