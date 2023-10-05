using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace App.World.Entity.Player.Events
{
    public class StandEvent : MonoBehaviour
    {
        public event Action<StandEvent> OnStand;

        public void CallStandEvent()
        {
            OnStand?.Invoke(this);
        }
    }
}
