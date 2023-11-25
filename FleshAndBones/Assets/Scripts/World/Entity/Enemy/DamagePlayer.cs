using App.World.Entity;
using App.World.Entity.Player.PlayerComponents;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    private float damage;
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private List<AudioClip> hitSounds;

    public void Init(float damage)
    {
        this.damage = damage;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        Health collisionHealth = collision.GetComponent<Health>();
        if (collisionHealth != null && player != null)
        {
            // Check if the player dodges the attack
            if (Random.value > player.DodgeChance) {
                collisionHealth.TakeDamage(damage);
                PlayHitSound();
            }
            else
            {

                PlayDodgeEffect();
            }


        }
        
    }

    private void PlayHitSound()
    {
        if (hitSounds.Count > 0)
        {
            int index = Random.Range(0, hitSounds.Count);
            audioSource.PlayOneShot(hitSounds[index]);
        }
    }

    private void PlayDodgeEffect()
    {
        Debug.Log("Player dodged the attack!");
    }
}
