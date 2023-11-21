using App.World.Entity.Player.Events;
using App.World.Entity.Player.PlayerComponents;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceMapper : MonoBehaviour
{
    [SerializeField] private Level valueContainer;
    [SerializeField] private Image fill;

    private void MapValue(ValueUpdateEvent ev, ValueUpdateEventArgs args)
    {
        Debug.Log($"Experience args: {args.prevValue}, {args.newValue}, {args.maxValue}");
        var percentage = args.newValue / args.maxValue;
        var prevScale = fill.transform.localScale;
        fill.transform.localScale = new Vector3(percentage, prevScale.y, prevScale.z);
    }

    private void OnEnable()
    {
        valueContainer.OnExperienceGained.OnValueUpdate += MapValue;
    }

    private void OnDisable()
    {
        valueContainer.OnExperienceGained.OnValueUpdate -= MapValue;
    }
}