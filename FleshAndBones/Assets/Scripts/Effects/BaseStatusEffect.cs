using App.World.Entity.Enemy;
using UnityEngine;

namespace App.Effects
{
    public abstract class BaseStatusEffect : ScriptableObject
    {
        public void EnableEffect(IEffectHolder holder)
        {
            Debug.LogWarning($"There's no method-visitor taking: { holder.GetType().Name } param. " +
                $"Add one or check the correctness of {GetType().Name}.");
        }

        public void DisableEffect(IEffectHolder holder)
        {
            Debug.LogWarning($"There's no method-visitor taking: { holder.GetType().Name } param. " +
                $"Add one or check the correctness of {GetType().Name}.");
        }

        public void UpdateEffect(IEffectHolder holder)
        {
            Debug.LogWarning($"There's no method-visitor taking: { holder.GetType().Name } param. " +
                $"Add one or check the correctness of {GetType().Name}.");
        }

        public abstract void EnableEffect(BaseEnemy holder);

        public abstract void UpdateEffect(BaseEnemy holder);

        public abstract void DisableEffect(BaseEnemy holder);

        public abstract bool IsStackable { get; }
    }
}
