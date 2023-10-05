using UnityEngine;
using World.Entity.Enemy;

namespace App.World.Entity.Enemy.States
{
    public class DieState : BaseEnemyState
    {
        public DieState(BaseEnemy baseEnemy, StateMachine stateMachine) : base(baseEnemy, stateMachine) { }

        public override void Enter()
        {
            foreach(Collider2D collider in baseEnemy.MyColliders)
                collider.enabled = false;
            baseEnemy.Animator.SetBool("IsSpawning", false);
            baseEnemy.Animator.SetBool("IsAttacking", false);
            baseEnemy.Animator.SetBool("IsSpawning", false);
            if (!baseEnemy.Animator.GetBool("MovingRight") && !baseEnemy.Animator.GetBool("MovingLeft"))
                baseEnemy.Animator.SetBool("MovingLeft", true);
            baseEnemy.Animator.SetBool("IsDying", true);
        }

        public override void Exit()
        {
            foreach (Collider2D collider in baseEnemy.MyColliders)
                collider.enabled = true;
            baseEnemy.Animator.SetBool("IsDying", false);
            baseEnemy.Animator.SetBool("MovingRight", false);
            baseEnemy.Animator.SetBool("MovingLeft", false);
        }
    }
}
