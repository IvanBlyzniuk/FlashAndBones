using App.Systems.EnemySpawning;
using App.Systems.Wave;
using App.World.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using App.Effects;
using App.World.Entity.Enemy.States;
using World.Entity;
using World.Entity.Enemy;

namespace App.World.Entity.Enemy
{
    public abstract class BaseEnemy : MonoBehaviour, IKillable, IEffectHolder, IObjectPoolItem
    {
        private bool initialised;
        private Transform target;
        private FollowState followState;
        private SpawningState spawningState;
        private DieState dieState;
        private IWaveSystem waveSystem;
        private Animator animator;
        private SpriteRenderer spriteRenderer;
        private float originalSpeed;
        private float slowEffectDuration;
        private float slowEffectMultiplier;


        [SerializeField]
        private Rigidbody2D myRigidbody;
        [SerializeField]
        private Health health;
        [SerializeField]
        private AudioSource audioSource;
        [SerializeField]
        protected EnemyData enemyData;
        [SerializeField]
        protected List<Collider2D> myColliders;

        protected StateMachine stateMachine;
        protected BaseEnemyState attackState;
        protected ObjectPool objectPool;

        public Transform Target => target;
        public Rigidbody2D MyRigidbody => myRigidbody;
        public EnemyData EnemyData => enemyData;
        public FollowState FollowState => followState;
        public BaseEnemyState AttackState => attackState;
        public Animator Animator => animator;
        public List<Collider2D> MyColliders => myColliders;
        public SpriteRenderer SpriteRenderer => spriteRenderer;
        public AudioSource AudioSource => audioSource;

        public virtual string PoolObjectType => enemyData.type;

        public virtual void Awake()
        {
            initialised = false;
            animator = GetComponent<Animator>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            stateMachine = new StateMachine();
            followState = new FollowState(this, stateMachine);
            spawningState = new SpawningState(this, stateMachine);
            dieState = new DieState(this,stateMachine);
        }

        void Update()
        {
            if(initialised)
                stateMachine.CurrentState.Update();
        }

        public virtual void Init(Vector3 position,Transform target, IWaveSystem waveSystem, float hpMultiplier)
        {
            this.target = target;
            this.waveSystem = waveSystem;
            transform.position = position;
            health.MaxHealth = enemyData.maxHealth * hpMultiplier;
            health.HealToMax();
            initialised = true;
            if(stateMachine.CurrentState == null)
                stateMachine.Initialize(spawningState);
            else
                stateMachine.ChangeState(spawningState);
            if(enemyData.gruntSounds.Count > 0)
                StartCoroutine(Grunt());
        }

        private IEnumerator Grunt()
        {
            float time = Random.Range(0, enemyData.maxTimeBetweenGrunts);
            yield return new WaitForSeconds(time);
            int index = Random.Range(0, enemyData.gruntSounds.Count);
            audioSource.PlayOneShot(enemyData.gruntSounds[index]);
            while (true)
            {
                time = Random.Range(enemyData.minTimeBetweenGrunts,enemyData.maxTimeBetweenGrunts);
                yield return new WaitForSeconds(time);
                index = Random.Range(0,enemyData.gruntSounds.Count);
                audioSource.PlayOneShot(enemyData.gruntSounds[index]);
            }
            
        }

        public void Die()
        {
            if(stateMachine.CurrentState != dieState)
            {
                StopAllCoroutines();
                stateMachine.ChangeState(dieState);
                DropMoney();
                DropHealing();
            }
        }

        public void DyingSequence()
        {
            waveSystem.ReportKilled(EnemyData.type);
            objectPool.ReturnToPool(this);
        }

        private void DropMoney()
        {
            //if(Random.value <= enemyData.moneyDropChance)
            //{
            //    int count = Random.Range(enemyData.minMoneyDrop,enemyData.maxMoneyDrop + 1);
            //    for(int i=0;i<count;i++)
            //    {
            //        GameObject money = objectPool.GetObjectFromPool(enemyData.moneyPrefab.PoolObjectType, enemyData.moneyPrefab.gameObject, transform.position).GetGameObject();
            //        money.GetComponent<MoneyDropItem>().Init(transform.position);
            //    }
            //}
        }
        private void DropHealing()
        {
            //if (Random.value <= enemyData.healingDropChance)
            //{
            //    GameObject healing = objectPool.GetObjectFromPool(enemyData.healingPrefab.PoolObjectType, enemyData.healingPrefab.gameObject, transform.position).GetGameObject();
            //    healing.GetComponent<HealingDropItem>().Init(transform.position);
               
            //}
        }

        public void GetFromPool(ObjectPool pool)
        {
            objectPool = pool;
            gameObject.SetActive(true);
        }

        public void ReturnToPool()
        {
            gameObject.SetActive(false);
        }

        public GameObject GetGameObject()
        {
            return(gameObject);
        }

        public void EnableEffect(BaseStatusEffect effect)
        {
            effect.EnableEffect(this);
        }

        public void UpdateEffect(BaseStatusEffect effect)
        {
            effect.UpdateEffect(this);
        }

        public void DisableEffect(BaseStatusEffect effect)
        {
            effect.DisableEffect(this);
        }


        public void ApplySlow(float multiplier, float duration)
        {
            StartCoroutine(SlowDownCoroutine(multiplier, duration));
        }

        private IEnumerator SlowDownCoroutine(float multiplier, float duration)
        {
            float originalSpeed = MyRigidbody.velocity.magnitude;
            MyRigidbody.velocity *= multiplier;
            Debug.Log($"Original speed {originalSpeed}");
            yield return new WaitForSeconds(duration);
            Debug.Log($"New speed {MyRigidbody.velocity.magnitude}");
            MyRigidbody.velocity = MyRigidbody.velocity.normalized * originalSpeed;
           
        }

    }
}

