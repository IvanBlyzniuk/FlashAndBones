using App;
using App.World.Entity.Enemy.States;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.World.Entity.Enemy.States
{
    public class SpawningState : BaseEnemyState
    {
        public SpawningState(BaseEnemy baseEnemy, StateMachine stateMachine) : base(baseEnemy, stateMachine) { }
        public override void Enter()
        {
            resetAllAnimationParams();
            baseEnemy.StartCoroutine(Spawn());
        }

        public override void Exit()
        {
            baseEnemy.Animator.SetBool("IsSpawning", false);
        }

        private IEnumerator Spawn()
        {
            yield return new WaitForSeconds(baseEnemy.EnemyData.spawnAnimationDuration);
            stateMachine.ChangeState(baseEnemy.FollowState);
        }

        private void resetAllAnimationParams()
        {
            baseEnemy.Animator.SetBool("IsSpawning", true);
            baseEnemy.Animator.SetBool("MovingRight",false);
            baseEnemy.Animator.SetBool("MovingLeft", false);
            baseEnemy.Animator.SetBool("IsAttacking", false);
            baseEnemy.Animator.SetBool("IsDying", false);
        }
    }
}

