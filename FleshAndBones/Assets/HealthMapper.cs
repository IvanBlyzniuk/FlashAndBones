using App.World.Entity;
using App.World.Entity.Player.Events;
using UnityEngine;
using UnityEngine.UI;

public class HealthMapper : MonoBehaviour
{
    [SerializeField] private Health valueContainer;
    [SerializeField] private Image fill;

    private void MapValue(ValueUpdateEvent ev, ValueUpdateEventArgs args)
    {
        var percentage = args.newValue / args.maxValue;
        var prevScale = fill.transform.localScale;
        fill.transform.localScale = new Vector3(percentage, prevScale.y, prevScale.z);
    }

    private void OnEnable()
    {
        valueContainer.healthUpdateEvent.OnValueUpdate += MapValue;
    }

    private void OnDisable()
    {
        valueContainer.healthUpdateEvent.OnValueUpdate -= MapValue;
    }
}
