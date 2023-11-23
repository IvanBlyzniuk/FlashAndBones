using App.Systems.EnemySpawning;
using App.Systems.Wave;
using App.World.Entity.Enemy;
using App.World.Entity.Enemy.States;
using UnityEngine;

namespace World.Entity.Enemy
{
    public class RangedEnemy : BaseEnemy
    {
        [SerializeField]
        private EnemyProjectile projectile;

        public EnemyProjectile Projectile => projectile;

        public override void Awake()
        {
            base.Awake();
            attackState = new RangedAttackState(this, stateMachine, FindObjectOfType<ObjectPool>());
        }

        public override void Init(Vector3 position, Transform target, IWaveSystem waveSystem, float hpMultiplier)
        {
            base.Init(position, target, waveSystem, hpMultiplier);
        }
    }
}

