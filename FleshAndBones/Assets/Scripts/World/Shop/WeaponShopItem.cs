using App.World.Entity.Player.Weapons;
using App.World.Shop;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace App.World.Shop
{
    public class WeaponShopItem : BaseShopItem
    {
        [SerializeField]
        private List<WeaponSO> weapons;

        [SerializeField]
        private SpriteRenderer spriteRenderer;

        private WeaponSO currentWeapon;

        public void SetRandomWeapon()
        {
            do
            {
                int randomIndex = Random.Range(0, weapons.Count);
                currentWeapon = weapons[randomIndex];
            } while (currentWeapon == player.Weapon.Data);
            spriteRenderer.sprite = currentWeapon.weaponSpriteForShop;

        }
        private void Start()
        {
            SetRandomWeapon();
        }
        protected override void OnTriggerEnter2D(Collider2D collision)
        {
            base.OnTriggerEnter2D(collision);
            merchantInfoField.GetComponentInChildren<TextMeshPro>().text = $"Press E to buy for {currentWeapon.cost} bones :\n{currentWeapon.description}";
        }
        protected override void OnTriggerExit2D(Collider2D collision)
        {
            base.OnTriggerExit2D(collision);
        }
        public override void TryBuy(SellEvent ev) // on click && overlap
        {
            if (timeFromBuy >= minTimeFromBuy)
            {
                if (player.Money >= currentWeapon.cost)
                {
                    Buy();
                    timeFromBuy = 0;
                    merchantInfoField.GetComponentInChildren<TextMeshPro>().text = $"Press E to buy for {currentWeapon.cost} bones : {currentWeapon.description}";
                }
                else
                {
                    Debug.Log("Not enough money!");
                }
            }
        }

        public override void Buy()
        {
            base.Buy();
            player.Money -= currentWeapon.cost;
            GameObject.Destroy(player.CurWeaponObj);
            player.CurWeaponObj = Instantiate(currentWeapon.weaponPrefab, player.WeaponPoint.position, Quaternion.identity, player.WeaponPoint);
            player.CurWeaponObj.transform.rotation = player.WeaponPoint.rotation;
            player.Weapon = player.CurWeaponObj.GetComponent<Weapon>();
            player.ShootPosition = player.Weapon.ShootPosition;
            SetRandomWeapon();
        }

    }

}
