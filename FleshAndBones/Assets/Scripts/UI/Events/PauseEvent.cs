using System;
using UnityEngine;

namespace App.World.UI.Events
{
    public class PauseEvent : MonoBehaviour
    {
        public event Action<PauseEvent, PauseEventArgs> OnPause;

        public void CallPauseEvent(bool isPaused)
        {
            PauseEventArgs args = new () { isPaused = isPaused };
            OnPause?.Invoke(this, args);
        }
    }
}
