using App.Systems.EnemySpawning;
using System.Collections;
using UnityEngine;
using World.Entity.Enemy;

namespace App.World.Entity.Enemy.States
{
    public class RangedAttackState : BaseEnemyState
    {
        private ObjectPool objectPool;
        public RangedAttackState(RangedEnemy baseEnemy, StateMachine stateMachine, ObjectPool objectPool) : base(baseEnemy, stateMachine)
        {
            this.objectPool = objectPool;
        }

        public override void Enter()
        {
            baseEnemy.Animator.SetBool("IsAttacking", true);
            baseEnemy.MyRigidbody.mass *= 1000;
            if(baseEnemy.EnemyData.attackSounds.Count > 0)
            {
                int index = Random.Range(0, baseEnemy.EnemyData.attackSounds.Count);
                baseEnemy.AudioSource.PlayOneShot(baseEnemy.EnemyData.attackSounds[index]);
            }
            baseEnemy.StartCoroutine(Attack());
        }

        public override void Update()
        {

        }

        public override void Exit()
        {
            baseEnemy.Animator.SetBool("IsAttacking", false);
            baseEnemy.MyRigidbody.mass /= 1000;
        }

        private IEnumerator Attack()
        {
            yield return new WaitForSeconds(baseEnemy.EnemyData.timeBetweenAttacks);

            RangedEnemy rangedEnemy = (RangedEnemy)baseEnemy;
            Vector3 lookDirection = (baseEnemy.Target.position - rangedEnemy.ShootPosition.position).normalized;
            Vector3 direction = Quaternion.Euler(0, 0, (rangedEnemy.ProjectileCount - 1) / 2.0f * -rangedEnemy.AngleBetweenProjectiles) * lookDirection;

            Quaternion rotation = Quaternion.Euler(0, 0, rangedEnemy.AngleBetweenProjectiles);
            for (int i = 0; i < rangedEnemy.ProjectileCount; i++, direction = rotation * direction)
            {
                GameObject projectileObject = objectPool.GetObjectFromPool(rangedEnemy.Projectile.PoolObjectType, rangedEnemy.Projectile.gameObject, rangedEnemy.ShootPosition.position).GetGameObject();
                
                EnemyProjectile projectile = projectileObject.GetComponent<EnemyProjectile>();
                projectile.Init(rangedEnemy.ShootPosition.position, direction);

                DamagePlayer projectileDamage = projectileObject.GetComponent<DamagePlayer>();
                projectileDamage.Init(baseEnemy.EnemyData.damage);
            }
            
            yield return new WaitForSeconds(0.1f);

            stateMachine.ChangeState(baseEnemy.FollowState);
        }
    }
}

