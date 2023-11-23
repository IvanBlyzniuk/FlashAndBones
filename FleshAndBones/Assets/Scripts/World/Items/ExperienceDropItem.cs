using App.World.Entity.Player.PlayerComponents;
using UnityEngine;

namespace App.World.Items
{
    public class ExperienceDropItem : BaseDropItem
    {
        [SerializeField]
        private int experience;

        public override string PoolObjectType => "Experience";

        public override void Init(Vector3 position)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
            base.Init(position);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log(collision.name);
            collision.gameObject.GetComponentInParent<Level>().IncreaseExperience(experience);
           // objectPool.ReturnToPool(this);
        }
    }
}

