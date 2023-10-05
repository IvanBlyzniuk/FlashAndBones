using App.World.Entity;
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
        Health collisionHealth = collision.GetComponent<Health>();
        if (collisionHealth != null)
        {
            collisionHealth.TakeDamage(damage);
            int index = Random.Range(0, hitSounds.Count);
            audioSource.PlayOneShot(hitSounds[index]);
        }  
        else
            Debug.Log("Error: Trying to damage target without Health component");
    }
}
