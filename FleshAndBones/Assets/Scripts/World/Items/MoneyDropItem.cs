using App.World.Entity.Player.PlayerComponents;
using UnityEngine;

namespace App.World.Items
{
    public class MoneyDropItem : BaseDropItem
    {
        [SerializeField]
        private int price;

        public override string PoolObjectType => "SmallMoney";

        public override void Init(Vector3 position)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
            base.Init(position);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            collision.gameObject.GetComponent<Player>().Money += price;
            objectPool.ReturnToPool(this);
        }
    }
}

