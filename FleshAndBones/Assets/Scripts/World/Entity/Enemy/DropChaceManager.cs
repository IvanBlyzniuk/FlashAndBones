using UnityEngine;

public class DropChanceManager
{
    public static float AdditionalHealingDropChance { get; set; } = 0f;

    public static bool ShouldDropHealing(float baseChance)
    {
        return Random.value <= (baseChance + AdditionalHealingDropChance);
    }
}
