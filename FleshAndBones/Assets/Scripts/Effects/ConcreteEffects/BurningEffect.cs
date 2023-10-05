using App.World.Entity;
using App.World.Entity.Enemy;
using UnityEngine;


namespace App.Effects.ConcreteEffects
{
    public class BurningEffect : BaseStatusEffect
    {
        [SerializeField] private float burningPeriod;
        [SerializeField] private float burningDamage;
        private float timeCounter;

        public override bool IsStackable => false;

        public override void EnableEffect(BaseEnemy holder) => timeCounter = 0f;

        public override void DisableEffect(BaseEnemy holder) => timeCounter = 0f;

        public override void UpdateEffect(BaseEnemy holder)
        {
            if(timeCounter >= burningPeriod)
            {
                var health = holder.GetComponent<Health>();
                health.TakeDamage(burningDamage);
                timeCounter = 0f;
            }
            else
            {
                timeCounter += Time.fixedDeltaTime;
            }
        }
    }
}
