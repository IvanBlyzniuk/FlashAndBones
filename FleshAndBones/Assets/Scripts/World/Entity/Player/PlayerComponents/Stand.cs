using App.World.Entity.Player.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace App.World.Entity.Player.PlayerComponents
{
    public class Stand : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody2D rb;
        [SerializeField]
        private StandEvent standEvent;

        private void OnEnable()
        {
            standEvent.OnStand += StopMoving;
        }
        private void StopMoving(StandEvent standEvent)
        {
            rb.velocity = Vector3.zero;
        }
    }
}