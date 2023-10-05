using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using World.Entity;

namespace App.World.Entity.Player.Weapons
{
    public class ParticleGun : Weapon
    {
        private HashSet<Health> affectedEntities;
        private Queue<Health> entitiesToRemove;
        private Queue<Health> entitiesToAdd;
        private Coroutine damageCoroutione;

        [SerializeField]
        private ParticleDamageZone damageZone;

        protected override void Awake()
        {
            base.Awake();
            damageZone.Init(this);
            affectedEntities = new HashSet<Health>();
            entitiesToAdd = new Queue<Health>();
            entitiesToRemove = new Queue<Health>();
            var emission = GetComponentInChildren<ParticleSystem>().emission;
            emission.rateOverTime = 0;
        }

        bool isShooting = false;
        new void Update()
        {
            base.Update();
            if (isShooting)
            {
                if (!audioSource.isPlaying)
                {
                    audioSource.clip = shootSound;
                    audioSource.Play();
                }
                var emission = GetComponentInChildren<ParticleSystem>().emission;
                emission.rateOverTime = 6000;
                isShooting = false;
            }
            else
            {
                audioSource.Stop();
                var emission = GetComponentInChildren<ParticleSystem>().emission;
                damageZone.gameObject.SetActive(false);
                affectedEntities.Clear();
                emission.rateOverTime = 0;
            }
        }
        public override void Shoot()
        {
            isShooting = true;
            damageZone.gameObject.SetActive(true);
            if(damageCoroutione == null)
            {
                damageCoroutione = StartCoroutine(DealDamage());
            }
        }

        private IEnumerator DealDamage()
        {
            Queue<Health> deadEntities = new Queue<Health>();
            foreach(var entity in affectedEntities)
            {
                entity.TakeDamage(damage);
                if(entity.CurrentHealth <= 0)
                    entitiesToRemove.Enqueue(entity);
            }
            foreach (var entity in entitiesToRemove)
                affectedEntities.Remove(entity);
            entitiesToRemove.Clear();
            foreach (var entity in entitiesToAdd)
                affectedEntities.Add(entity);
            entitiesToAdd.Clear();
            yield return new WaitForSeconds(coolDown);
            damageCoroutione = null;
        }

        public void AddAffectedEntity(Health entity)
        {
            entitiesToAdd.Enqueue(entity);
        }

        public void RemoveAffectedEntity(Health entity)
        {
            entitiesToRemove.Enqueue(entity);
        }
    }
}
