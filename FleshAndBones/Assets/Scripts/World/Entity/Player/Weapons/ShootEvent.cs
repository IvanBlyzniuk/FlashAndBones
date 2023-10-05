using System;
using UnityEngine;
namespace App.World.Entity.Player.Weapons
{
    public class ShootEvent : MonoBehaviour
    {
        public event Action<ShootEvent> OnShoot;

        public void CallShootEvent()
        {
            OnShoot?.Invoke(this);
        }
    }
}