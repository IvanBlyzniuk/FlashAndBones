using App.World.Entity.Player.PlayerComponents;
using UnityEngine;

namespace App.Upgrades.ConcreteUpgrades.UpdatableStrategy.PlayerUpgrades
{
    [System.Serializable]
    public struct PoisonPoolUpgradeLevel
    {
        [Min(0.1f)] public float damage;
        [Min(1)] public int hitNumber;
        [Range(0.05f, 5f)] public float hitPeriod;
        [Range(1f, 30f)] public float appearancePeriod;
        [Range(1f, 30f)] public float existanceDuration;
        public GameObject poisonPoolPrefab;
    }

    [CreateAssetMenu(fileName = "PoisonPool", menuName = "Scriptable Objects/Upgrades/PoisonPoolUpgrade")]
    public class PoisonPoolUpgrade : StandardUpdatableUpgradeScriptableObject<Player, PoisonPoolUpgradeLevel>
    {
        public PoisonPoolUpgrade() : base(new PoisonPoolUpgradeUpgradeStrategy()) {}
    }

    public class PoisonPoolUpgradeUpgradeStrategy : IUpdatableStrategy<Player, PoisonPoolUpgradeLevel>
    {
        PoisonPoolUpgradeLevel currentLevel;
        private float timeCounter { get; set; } = 0.0f;

        public void Initialize(Player player) { }

        public void Reset(Player player) { }

        public void SwitchToLevel(Player player, PoisonPoolUpgradeLevel level)
        {
            currentLevel = level;
        }

        public void Update(Player upgradableEntity)
        {
            if (timeCounter > currentLevel.appearancePeriod)
            {
                SpawnPoisonPool(upgradableEntity.transform.position);
                timeCounter = 0f;
            }
            else
            {
                timeCounter += Time.deltaTime;
            }
        }

        private void SpawnPoisonPool(Vector3 position)
        {
            GameObject poisonPoolPrefab = currentLevel.poisonPoolPrefab;
            GameObject instantiated = GameObject.Instantiate(poisonPoolPrefab, position, Quaternion.identity);
            PoisonPool poisonPoolComponent = instantiated.GetComponent<PoisonPool>();
            poisonPoolComponent.Init(currentLevel.hitPeriod, 
                                     currentLevel.damage, 
                                     currentLevel.hitNumber, 
                                     currentLevel.existanceDuration);
        }
    }
}
