using UnityEngine;

namespace App.World.Entity.Player.Weapons
{
    public class BulletGun : Weapon
    {
        
        public override void Shoot()
        {
            if (timeFromCoolDown > coolDown)
            {
                BaseBullet bulletScript = bulletPrefab.GetComponent<BaseBullet>();
                if (bulletScript == null)
                {
                    Debug.Log("Trying to shoot bullet that doesn't contain Bullet script");
                    return;
                }
                for (int i = 0; i < bulletCount; i++)
                {
                    float spread = Random.Range(-bulletSpread, bulletSpread);
                    Quaternion rotation = Quaternion.Euler(ShootPosition.eulerAngles.x, ShootPosition.eulerAngles.y, ShootPosition.eulerAngles.z + spread);
                    GameObject bullet = objectPool.GetObjectFromPool(bulletScript.PoolObjectType, bulletPrefab, ShootPosition.position).GetGameObject();
                    bullet.transform.rotation = rotation;
                    bullet.transform.position = ShootPosition.position;
                    bullet.GetComponent<BaseBullet>().Init(damage,PearcingCount, accuracy, lifeStealAmount, SlowEffect);
                    bullet.GetComponent<Rigidbody2D>().velocity = bullet.transform.right * bulletFlySpeed;
                    //Debug.Log("Bullet");
                }
                audioSource.PlayOneShot(shootSound);
                timeFromCoolDown = 0.0f;
            }
        }
    }
}

