using UnityEngine;
using World.Entity;

namespace App.World.Entity.Player.Weapons
{
    public class ParticleDamageZone : MonoBehaviour
    {
        private ParticleGun particleGun;

        public void Init(ParticleGun particleGun)
        {
            this.particleGun = particleGun;
        }

        public void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.layer != LayerMask.NameToLayer("Enemy"))
            {
                return;
            }
            Health health = collision.GetComponent<Health>();
            if(health == null)
            {
                Debug.LogWarning("No health component on entity affected by particle gun");
                return;
            }
            particleGun.AddAffectedEntity(health);
        }

        public void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.layer != LayerMask.NameToLayer("Enemy"))
            {
                return;
            }
            Health health = collision.GetComponent<Health>();
            if (health == null)
            {
                Debug.LogWarning("No health component on entity affected by particle gun");
                return;
            }
            particleGun.RemoveAffectedEntity(health);
        }
    }
}

