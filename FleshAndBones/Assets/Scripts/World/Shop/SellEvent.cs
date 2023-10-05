using System;
using UnityEngine;

namespace App.World.Shop
{
    public class SellEvent : MonoBehaviour
    {
        public event Action<SellEvent> OnSell;

        public void CallSellEvent()
        {
            OnSell?.Invoke(this);
        }
    }
}

