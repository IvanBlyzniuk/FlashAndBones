using App.World.Entity;
using App.World.Entity.Enemy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonPool : MonoBehaviour
{
    private float? hitPeriod = null;
    private float? damage = null;
    private int? hitNumber = null;
    private float? existenceDuration = null;

    private List<Health> poisonedHealths = new();
    private float timeCounter = 0f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!gameObject.activeSelf)
        {
            return;
        }

        if (!IsInitialized())
        {
            return;
        }

        var enemyComponent = collision.gameObject.GetComponent<BaseEnemy>();

        if (enemyComponent == null)
        {
            return;
        }

        var health = enemyComponent.Health;

        if (gameObject.activeSelf && !IsPoisoned(health))
        {
            poisonedHealths.Add(health);
            StartCoroutine(ApplyPoisoningEffect(health));
        }
        else
        {
            poisonedHealths.Remove(health);
        }
    }

    private void Update()
    {
        if (!IsInitialized())
        {
            return;
        }

        if (timeCounter > existenceDuration)
        {
            Destroy(gameObject);
        }
        else
        {
            timeCounter += Time.deltaTime;
        }
    }

    public void Init(float hitPeriod, float damage, int hitNumber, float existenceDuration)
    {
        this.hitPeriod = hitPeriod;
        this.damage = damage;
        this.hitNumber = hitNumber;
        this.existenceDuration = existenceDuration;
    }

    private IEnumerator ApplyPoisoningEffect(Health health)
    {
        for (int i = 0; i < hitNumber; ++i)
        {
            Debug.Log("Hit");
            health.TakeDamage(damage.Value);
            yield return new WaitForSeconds(hitPeriod.Value);
        }

        poisonedHealths.Remove(health);
    }

    private bool IsPoisoned(Health health) => poisonedHealths.Find(h => h == health) != null;

    private bool IsInitialized() 
        => hitPeriod != null && damage != null && hitNumber != null && existenceDuration != null;
}
