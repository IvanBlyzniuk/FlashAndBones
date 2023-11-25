using App.Systems.EnemySpawning;
using UnityEngine;
using App.Upgrades.ConcreteUpgrades;
using App.Upgrades.ConcreteUpgrades.StandardStrategy.PlayerUpgrades;
namespace App.World.Entity.Player.Weapons
{

    public abstract class Weapon : MonoBehaviour
    {
        protected ObjectPool objectPool;
        #region Serialized Fields
        [SerializeField]
        private ShootEvent shootEvent;
        [SerializeField]
        private Transform shootPosition;
        [SerializeField]
        private WeaponSO data;
        #endregion

        #region Sounds
        protected AudioSource audioSource;
        [SerializeField]
        protected AudioClip shootSound;
        #endregion

        #region Parameters
        protected float timeFromCoolDown;
        protected float damage;
        protected float bulletFlySpeed;
        protected float bulletSpread;
        protected int bulletCount;
        protected float coolDown;
        private int pearcingCount;
        protected GameObject bulletPrefab;

        protected float accuracy = 0f;//for accuracy upgrade
        protected LifeStealInfo lifeStealAmount = null;// for vamp upgrade
        #endregion

        protected virtual void Awake()
        {
            damage = Data.damage;
            coolDown = Data.coolDown;
            bulletFlySpeed = Data.bulletFlySpeed;
            bulletPrefab = Data.bullet;
            bulletSpread = Data.bulletSpread;
            objectPool = FindObjectOfType<ObjectPool>();
            audioSource = GetComponent<AudioSource>();
            shootSound = Data.shootSound;
            bulletCount = Data.bulletCount;
            pearcingCount = Data.pearcingCount;
        }

        public SlowEffectInfo SlowEffect { get; set; }
        public ShootEvent ShootEvent { get => shootEvent; }
        public float Cooldown { get => coolDown; set => coolDown = value; }
        public float Damage { get => damage; set => damage = value; }
        public float BulletFlySpeed { get => bulletFlySpeed; set => bulletFlySpeed = value; }
        public Transform ShootPosition { get => shootPosition; set => shootPosition = value; }
        public WeaponSO Data { get => data; set => data = value; }
        public int PearcingCount { get => pearcingCount; set => pearcingCount = value; }

        public LifeStealInfo LifeStealAmount
        {
            get => lifeStealAmount;
            set
            {
                if (value == null)
                {
                    lifeStealAmount = null;
                    return;
                }

                lifeStealAmount = new LifeStealInfo
                {
                    lifeStealAmount = Mathf.Clamp01(value.lifeStealAmount),
                    player = value.player
                };
            }
        }

        private void OnEnable()
        {
            ShootEvent.OnShoot += Shoot;
        }

        public float Accuracy
        {
            get => accuracy;
            set => accuracy = Mathf.Clamp01(value);

        }

        private void OnDisable()
        {
            ShootEvent.OnShoot -= Shoot;
        }
        void Start()
        {
            timeFromCoolDown = coolDown;

        }
        protected void Update()
        {
            timeFromCoolDown += Time.deltaTime;
        }
        public void Shoot(ShootEvent ev)
        {
            Shoot();
        }
        public abstract void Shoot();


    }
}