using System;
using UnityEngine;
namespace App.World.Entity.Player.Events
{
    public class AimEvent : MonoBehaviour
    {
        public event Action<AimEvent, AimEventArgs> OnAim;

        public void CallAimEvent(float angle, float playerPos, float mousePos)
        {
            AimEventArgs args = new AimEventArgs() { angle = angle, playerPos = playerPos, mousePos = mousePos };
            OnAim?.Invoke(this, args);
        }
    }
}