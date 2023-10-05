using App.World.Entity.Player.PlayerComponents;
using UnityEngine;

namespace App.World.Shop
{
    public abstract class BaseShopItem : MonoBehaviour
    {
        [SerializeField]
        protected ObjectsContainer container;

        [SerializeField]
        protected Shop shop;

        protected Player player;

        protected float minTimeFromBuy = 0.1f;

        protected float timeFromBuy = 0f;

        [SerializeField]
        protected GameObject merchantInfoField;

        [SerializeField]
        protected AudioClip buySound;

        protected AudioSource audioSource;


        protected void Update()
        {
            timeFromBuy += Time.deltaTime;
        }

        protected virtual void Awake()
        {
            player = container.Player.GetComponent<Player>();
            audioSource = GetComponent<AudioSource>();
        }
        
        
        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            merchantInfoField.SetActive(true);
            
            shop.SellEvent.OnSell += this.TryBuy;
        }
        protected virtual void OnTriggerExit2D(Collider2D collision)
        {
            merchantInfoField.SetActive(false);
            shop.SellEvent.OnSell -= this.TryBuy;
        }

        public abstract void TryBuy(SellEvent ev);

        public virtual void Buy()
        {
            audioSource.PlayOneShot(buySound);
        }
        
    }
}
