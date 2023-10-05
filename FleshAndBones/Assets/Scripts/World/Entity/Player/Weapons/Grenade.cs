using App.VFX;
using UnityEngine;


namespace App.World.Entity.Player.Weapons
{
    public class Grenade : BaseBullet
    {
        [SerializeField]
        private Explosion explosion;

        public override void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.layer != LayerMask.NameToLayer("Enemy"))
            {
                objectPool.ReturnToPool(this);
                return;
            }
            base.OnTriggerEnter2D(collision);
            Explode();
        }

        private void Explode()
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            var newExplosion = objectPool.GetObjectFromPool(explosion.PoolObjectType,explosion.gameObject,transform.position).GetGameObject();
            newExplosion.GetComponent<Explosion>().Init(transform.position, 0.3f, damage);
            objectPool.ReturnToPool(this);
        }
        
        public override void Init(float damage,int pearcingCount)
        {
            base.Init(damage,pearcingCount);
            
        }
    }
}
