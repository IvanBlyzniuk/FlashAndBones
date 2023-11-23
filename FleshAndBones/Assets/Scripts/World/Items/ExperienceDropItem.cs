using App.World.Entity.Player.PlayerComponents;
using UnityEngine;

namespace App.World.Items
{
    public class ExperienceDropItem : BaseDropItem
    {
        [SerializeField]
        private int experience;
        [SerializeField]
        private float flightSpeed;

        private bool shouldBeCollected;

        private GameObject player;


        public override string PoolObjectType => "Experience";

        private void Update()
        {
            if (shouldBeCollected && player != null)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, flightSpeed * Time.deltaTime);

                if ((transform.position - player.transform.position).magnitude < 0.5f)
                {
                    shouldBeCollected = false;
                    player.GetComponent<Level>().IncreaseExperience(experience);
                    Destroy(this.gameObject); // TODO change for object pool
                }
            }
        }

        public override void Init(Vector3 position)
        {
            shouldBeCollected = false;
            base.Init(position);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log(collision.name);
            shouldBeCollected = true;
            player = collision.transform.parent.gameObject;
          
        }
    }
}

