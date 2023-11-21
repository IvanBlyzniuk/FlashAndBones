using UnityEngine;

namespace App.Upgrades
{
    public interface IDisplayableUpgrade
    {
        Sprite Image { get; }
        string Description { get; }
    }
}
