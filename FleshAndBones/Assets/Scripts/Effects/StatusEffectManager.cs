using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace App.Effects
{
    public class StatusEffectManager : MonoBehaviour
    {
        private Dictionary<BaseStatusEffect, ICollection<IEffectHolder>> holdersLists;

        private void FixedUpdate() => UpdateAllEffects();

        public void AddHolderToEffect(IEffectHolder holder, BaseStatusEffect effect)
        {
            if(holdersLists.ContainsKey(effect))
            {
                var holders = holdersLists[effect];
                if (!effect.IsStackable && holders.Contains(holder))
                    throw new ArgumentException($"Cannot stack an unstackable effect of type: {effect.GetType().Name}");
                holders.Add(holder);
                holder.EnableEffect(effect);
            }
            else
            {
                holdersLists.Add(effect, new List<IEffectHolder>());
            }
        }

        public void UpdateAllEffects()
        {
            foreach(var effect in holdersLists.Keys)
            {
                foreach(var holder in holdersLists[effect])
                {
                    holder.UpdateEffect(effect);
                }
            }
        }

        public void RemoveHolderFromEffect(IEffectHolder holder, BaseStatusEffect effect)
        {
            if (!holdersLists.ContainsKey(effect))
                throw new ArgumentException($"Cannot remove holder from a non-existent effect : {effect.GetType().Name}");
            var holders = holdersLists[effect];
            if (!holders.Contains(holder))
                throw new ArgumentException($"The holder hasn't been added to the effect");
            holders.Remove(holder);
            holder.DisableEffect(effect);
        }
    }
}
