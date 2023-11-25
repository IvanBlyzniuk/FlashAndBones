using System;
using UnityEngine;

namespace App.World.Entity.Player.Weapons
{
    public class BulletGun : Weapon
    {
        public event Action<BaseBullet> OnBulletSpawned;
        
        public override void Shoot()
        {
            float spread = UnityEngine.Random.Range(-bulletSpread, bulletSpread);
            Quaternion rotation = Quaternion.Euler(ShootPosition.eulerAngles.x, ShootPosition.eulerAngles.y, ShootPosition.eulerAngles.z + spread);
            ShootInDirection(rotation);
        }

        public void ShootInDirection(Quaternion rotation)
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
                    GameObject bullet = objectPool.GetObjectFromPool(bulletScript.PoolObjectType, bulletPrefab, ShootPosition.position).GetGameObject();
                    bullet.transform.rotation = rotation;
                    bullet.transform.position = ShootPosition.position;
                    bullet.GetComponent<BaseBullet>().Init(damage, PearcingCount, accuracy, lifeStealAmount, SlowEffect);
                    bullet.GetComponent<Rigidbody2D>().velocity = bullet.transform.right * bulletFlySpeed;
                    OnBulletSpawned?.Invoke(bullet.GetComponent<BaseBullet>());
                }
                audioSource.PlayOneShot(shootSound);
                timeFromCoolDown = 0.0f;
            }
        }
    }
}

