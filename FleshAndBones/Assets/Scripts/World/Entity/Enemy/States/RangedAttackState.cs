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
            int index = Random.Range(0, baseEnemy.EnemyData.attackSounds.Count);
            baseEnemy.AudioSource.PlayOneShot(baseEnemy.EnemyData.attackSounds[index]);
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
            GameObject projectileObject = objectPool.GetObjectFromPool(rangedEnemy.Projectile.PoolObjectType, rangedEnemy.Projectile.gameObject, rangedEnemy.transform.position).GetGameObject();

            Vector3 direction = (baseEnemy.Target.position - baseEnemy.transform.position).normalized;
            EnemyProjectile projectile = projectileObject.GetComponent<EnemyProjectile>();
            projectile.Init(baseEnemy.transform.position,direction);

            DamagePlayer projectileDamage = projectileObject.GetComponent<DamagePlayer>();
            projectileDamage.Init(baseEnemy.EnemyData.damage);
            yield return new WaitForSeconds(0.1f);

            stateMachine.ChangeState(baseEnemy.FollowState);
        }
    }
}

