using UnityEngine;

namespace App.World.Entity.Player.Weapons
{
    [CreateAssetMenu(fileName = "WeaponDataSO", menuName = "Scriptable Objects/Weapons/ Single Bullet Weapon Data")]
    public class WeaponSO : ScriptableObject
    {
        public float damage;
        public float coolDown;
        public float bulletFlySpeed;
        public float bulletSpread;
        public int pearcingCount;
        public GameObject bullet;
        public int bulletCount;
        public AudioClip shootSound;
    }
}

