using App.World.Entity;
using App.World.Entity.Enemy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonPool : MonoBehaviour
{
    private float? period = 1f;
    private float? damage = 5;
    private int? hitNumber = 5;
    private float? duration = 20;

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

        if (timeCounter > duration)
        {
            Destroy(gameObject);
        }
        else
        {
            timeCounter += Time.deltaTime;
        }
    }

    public void Init(float period, float damage, int hitNumber, float duration)
    {
        this.period = period;
        this.damage = damage;
        this.hitNumber = hitNumber;
        this.duration = duration;
    }

    private IEnumerator ApplyPoisoningEffect(Health health)
    {
        for (int i = 0; i < hitNumber; ++i)
        {
            Debug.Log("Hit");
            health.TakeDamage(damage.Value);
            yield return new WaitForSeconds(period.Value);
        }

        poisonedHealths.Remove(health);
    }

    private bool IsPoisoned(Health health) => poisonedHealths.Find(h => h == health) != null;

    private bool IsInitialized() 
        => period != null && damage != null && hitNumber != null && duration != null;
}
