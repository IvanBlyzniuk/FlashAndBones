using System;
using UnityEngine;

namespace App.World.Entity.Player.Events
{
    public class ValueUpdateEvent : MonoBehaviour
    {
        public event Action<ValueUpdateEvent, ValueUpdateEventArgs> OnValueUpdate;

        public void CallValueUpdateEvent(float prevValue, float newHP, float maxHP)
        {
            ValueUpdateEventArgs args = new() { prevValue = prevValue, newValue = newHP, maxValue = maxHP };
            OnValueUpdate?.Invoke(this, args);
        }
    }
}