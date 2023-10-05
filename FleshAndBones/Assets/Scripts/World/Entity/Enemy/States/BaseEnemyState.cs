using World.Entity.Enemy;
namespace App.World.Entity.Enemy.States
{
    public class BaseEnemyState : IState
    {
        protected BaseEnemy baseEnemy;
        protected StateMachine stateMachine;
        public BaseEnemyState(BaseEnemy baseEnemy, StateMachine stateMachine)
        {
            this.baseEnemy = baseEnemy;
            this.stateMachine = stateMachine;
        }
        public virtual void Enter() { }

        public virtual void Update() { }

        public virtual void Exit() { }
    }
}

