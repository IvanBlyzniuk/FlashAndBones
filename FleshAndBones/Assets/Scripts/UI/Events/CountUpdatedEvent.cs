using System;
using UnityEngine;

namespace App.World.UI.Events
{
    public class CountUpdatedEvent : MonoBehaviour
    {
        public event Action<CountUpdatedEvent, CountUpdatedEventArgs> OnCountUpdated;

        public void CallCountUpdatedEvent(int newCount)
        {
            CountUpdatedEventArgs args = new() { newCount = newCount };
            OnCountUpdated?.Invoke(this, args);
        }
    }
}
