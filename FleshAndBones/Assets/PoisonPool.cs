using App.World.Entity;
using App.World.Entity.Enemy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonPool : MonoBehaviour
{
    private float? period = 1;
    private float? damage = 10;
    private int? hitNumber = 5;

    private List<Health> poisonedHealths = new();

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

        if (!IsPoisoned(health) && gameObject.activeSelf)
        {
            StartCoroutine(ApplyPoisoningEffect(health));
        }
    }

    public void Init(float period, float damage, int hitNumber)
    {
        this.period = period;
        this.damage = damage;
        this.hitNumber = hitNumber;
    }

    private IEnumerator ApplyPoisoningEffect(Health health)
    {
        poisonedHealths.Add(health);

        for (int i = 0; i < hitNumber; ++i)
        {
            Debug.Log("Hit");
            health.TakeDamage(damage.Value);
            yield return new WaitForSeconds(period.Value);
        }

        poisonedHealths.Remove(health);
    }

    private bool IsPoisoned(Health health) => poisonedHealths.Find(h => h == health) != null;

    private bool IsInitialized() => period != null && damage != null && hitNumber != null;
}
