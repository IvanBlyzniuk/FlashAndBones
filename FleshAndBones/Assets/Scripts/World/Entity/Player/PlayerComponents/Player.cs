using App.World.Entity.Player.Events;
using App.World.Entity.Player.Weapons;
using UnityEngine;
using App.Upgrades;
using App.World.UI.Events;
using Unity.VisualScripting;

namespace App.World.Entity.Player.PlayerComponents
{
    #region Required
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Transform))]
    [RequireComponent(typeof(Health))]
    [RequireComponent(typeof(PlayerAnimationsController))]
    [RequireComponent(typeof(Movement))]
    [RequireComponent(typeof(Aim))]
    [RequireComponent(typeof(Stand))]
    [RequireComponent(typeof(Old_UpgradeManager))]
    #endregion
    public class Player : MonoBehaviour, IKillable, Old_IUpgradable, IUpgradable
    {
        #region Components
        private Transform playerTransform;
        private Animator pAnimator;
        private Health health;
        private Old_UpgradeManager upgradeManager;
        [SerializeField]
        private PlayerDataSO playerData;
        #endregion

        #region Weapon
        [SerializeField]
        private Transform shootPosition;
        [SerializeField]
        private Transform weaponAnchor;
        [SerializeField]
        private GameObject curWeaponObj;
        private Weapon weapon;
        [SerializeField]
        private Transform weaponPoint;
        #endregion

        #region Events
        [SerializeField]
        private AimEvent aimEvent;
        [SerializeField]
        private StandEvent standEvent;
        [SerializeField]
        private MovementEvent movementEvent;
        [SerializeField]
        private ValueUpdateEvent hpUpdateEvent;
        [SerializeField]
        private DieEvent dieEvent;
        [SerializeField]
        private CountUpdatedEvent countUpdatedEvent;
        #endregion

        #region Sounds
        [SerializeField]
        private AudioClip[] stepSounds;
        private AudioSource audioSource;
        #endregion

        #region Parameters
        private float movementSpeed;
        private int money;
        private bool isDead; //TODO replace with more global "game stop"
        #endregion

        #region Properties
        public Transform ShootPosition { get => shootPosition; set => shootPosition = value; }
        public Animator PAnimator { get => pAnimator; }
        public Transform PlayerTransform { get => playerTransform;}
        public Transform WeaponAnchor { get => weaponAnchor;}
        public AimEvent AimEvent { get => aimEvent;}
        public StandEvent StandEvent { get => standEvent;}
        public MovementEvent MovementEvent { get => movementEvent;}
        public ValueUpdateEvent HPUpdateEvent { get => hpUpdateEvent; }
        public float MovementSpeed { get => movementSpeed; set => movementSpeed = value; }
        public int Money { get => money; set { money = value; countUpdatedEvent.CallCountUpdatedEvent(value); } }
        public Health Health { get => health; set => health = value; }
        public GameObject CurWeaponObj { get => curWeaponObj; set => curWeaponObj = value; }
        public Transform WeaponPoint { get => weaponPoint; set => weaponPoint = value; }
        public Weapon Weapon 
        { 
            get => weapon; 
            set
            {
                upgradeManager.DisableAll();
                weapon = value;
                upgradeManager.EnableAll();
            }
        }
        #endregion

        private void Awake()
        {
            Init();
        }
        private void Init()
        {
            playerTransform = GetComponent<Transform>();
            pAnimator = GetComponent<Animator>();
            health = GetComponent<Health>();
            upgradeManager = GetComponent<Old_UpgradeManager>();
            weapon = CurWeaponObj.GetComponent<Weapon>();
            audioSource = GetComponent<AudioSource>();
            movementSpeed = playerData.speed;
            health.MaxHealth = playerData.maxHealth;
            isDead = false;
            money = 1000;
        }
        public void Die()
        {
            Weapon.enabled = false;
            GetComponent<Movement>().enabled = false;
            GetComponent<Aim>().enabled = false;
            GetComponent<PlayerAnimationsController>().enabled = false;
            if (isDead) return;
            dieEvent.CallDieEvent();
        }

        public void EnableUpgrade(Old_BaseUpgrade upgrade)
        {
            upgrade.Enable(this);
        }

        public void UpdateUpgrade(Old_BaseUpgrade upgrade)
        {
            upgrade.UpdateUpgrade(this);
        }

        public void DisableUpgrade(Old_BaseUpgrade upgrade)
        {
            upgrade.Disable(this);
        }

        public void EnableUpgrade(IUpgradeAbstractVisitor upgrade)
        {
            IUpgradable.EnableUpgradeViaVisitorOf(this, upgrade);
        }

        public void DisableUpgrade(IUpgradeAbstractVisitor upgrade)
        {
            throw new System.NotImplementedException();
        }

        public void MakeStepSound()
        {
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(stepSounds[Random.Range(0, stepSounds.Length)]);
            }
        }
    }
}

