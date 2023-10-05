using UnityEngine;
using App.World.Entity.Player.Events;

namespace App.Utilities
{
    public class DisappearOnDeathEvent : MonoBehaviour
    {
        [SerializeField] private DieEvent dieEvent;

        private void OnEnable()
        {
            dieEvent.OnDied += Disappear;
        }

        private void OnDisable()
        {
            dieEvent.OnDied -= Disappear;
        }

        private void Disappear(DieEvent ev) => gameObject.SetActive(false);
    }
}

