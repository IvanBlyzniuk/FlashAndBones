using App.World.Entity.Enemy;
using UnityEngine;

namespace World.Entity.Enemy
{
    public class EnemyAnimationsController : MonoBehaviour
    {
        private BaseEnemy baseEnemy;

        public void SetMoveAnimationParams(float vx)
        {
            if(vx < 0)
            {
                baseEnemy.Animator.SetBool("MovingRight", false);
                baseEnemy.Animator.SetBool("MovingLeft", true);
            }
            else
            {
                baseEnemy.Animator.SetBool("MovingRight", true);
                baseEnemy.Animator.SetBool("MovingLeft", false);
            }
        }
    }
}

