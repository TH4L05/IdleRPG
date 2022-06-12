///<author>ThomasKrahl</author>

using UnityEngine;
using IdleGame.Unit.Stats;

namespace IdleGame.Unit
{
    public enum UnitState
    {
        Invalid = -1,
        Resting,
        Running,
        Attack,
        Dead,
    }

    public class Unit : MonoBehaviour ,IDamagable
    {
        #region SerializedFields

        [SerializeField] protected UnitStats unitStats;
        [SerializeField] protected Animator animator;

        #endregion

        #region private Fields

        protected UnitState state;
        protected float currentHealth;
        protected float currentMana;       
        protected bool healthRegenActive;
        protected bool manaRegenActive;
        protected bool isDead;
        protected float attackTimer;

        #endregion

        #region PublicFields

        public float CurrentHealth => currentHealth;
        public float CurrentMana => currentMana;

        #endregion

        #region UnityFunctions

        void Awake()
        {
            Initialize();
        }

        void Start()
        {
            AdditionalSetup();
        }

        void Update()
        {
            CheckStatus();
            OnUpdate();
            RegenerateHealth();
            RegenerateMana();            
        }

        private void LateUpdate()
        {
            OnLateUpdate();
        }

        void OnDestroy()
        {
            DeathSetup();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            OnCollision(collision);
        }

        #endregion

        #region Initialize and Destroy

        protected virtual void Initialize()
        {
            currentHealth = unitStats.MaxHealth;
            currentMana = unitStats.MaxMana;
        }

        protected virtual void AdditionalSetup()
        {
        }

        protected virtual void DeathSetup()
        {
        }

        #endregion

        #region Damage and Attack

        public virtual void TakeDamage(float damageAmount)
        {
            if(isDead) return;

            float t1 = 100 + unitStats.Defense;
            float t2 = 100 / t1;
            float damage = damageAmount * t2;

            DecreaseHealth(damage);
            Debug.Log($"<color=orange>{gameObject.name} takes damage - {damage.ToString("0")}</color>");
            Game.Instance.InstaniateFloatingText(new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z), damage.ToString("0"));
        }

        protected virtual void Attack()
        {
        }

        protected virtual void ProvideDamage()
        {
        }

        #endregion

        #region Health

        protected virtual void DecreaseHealth(float amount)
        {
            currentHealth -= amount;

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                state = UnitState.Dead;
            }            
        }

        protected virtual void IncreaseHealth(float amount)
        {
            currentHealth += amount;

            if (currentHealth >= unitStats.MaxHealth)
            {
                currentHealth = unitStats.MaxHealth;
            }
        }

        private void RegenerateHealth()
        {
            if (currentHealth < unitStats.MaxHealth && !healthRegenActive)
            {
                InvokeRepeating("HealthRegen", 1f, 1f);
                healthRegenActive = true;
            }
        }

        private void HealthRegen()
        {
            IncreaseHealth(unitStats.HealthRegen);

            if (currentHealth >= unitStats.MaxHealth)
            {
                CancelInvoke("HealthRegen");
                healthRegenActive = false;
            }
        }

        #endregion

        #region Mana

        protected virtual void DecreaseMana(float amount)
        {
            currentMana -= amount;

            if (currentMana <= 0)
            {
                currentMana = 0;
            }
        }

        protected virtual void IncreaseMana(float amount)
        {
            currentMana += amount;

            if (currentMana >= unitStats.MaxMana)
            {
                currentMana = unitStats.MaxMana;
            }
        }

        private void RegenerateMana()
        {
            if (currentMana < unitStats.MaxMana && !manaRegenActive)
            {
                InvokeRepeating("ManaRegen", 1f, 1f);
                manaRegenActive = true;
            }
        }

        private void ManaRegen()
        {
            IncreaseMana(unitStats.ManaRegen);

            if (currentMana >= unitStats.MaxMana)
            {
                manaRegenActive = false;
                CancelInvoke("ManaRegen");
            }
        }

        #endregion*/

        #region State

        protected void CheckStatus()
        {        
            if(isDead) return;

            switch (state)
            {
                case UnitState.Invalid:
                default:
                    Debug.Log("ERROR - Invalid State");
                    return;


                case UnitState.Resting:
                    OnStateIdle();
                    break;


                case UnitState.Running:
                    OnStateMove();
                    break;


                case UnitState.Attack:
                    OnStateAttack();
                    break;


                case UnitState.Dead:
                    OnStateDead();
                    break;

            }
        }

        protected virtual void OnStateIdle()
        {
            if (animator != null) animator.SetInteger("Speed", 0);
        }

        protected virtual void OnStateMove()
        {
            if(animator != null) animator.SetInteger("Speed", 1);
        }

        protected virtual void OnStateAttack()
        {
            Attack();
        }

        protected virtual void OnStateDead()
        {
            isDead = true;
            if (animator != null) animator.SetTrigger("Death");
        }

        #endregion

        protected virtual void OnUpdate()
        {
        }

        protected virtual void OnLateUpdate()
        {
        }

        protected virtual void OnCollision(Collider2D collision)
        {
        }

        
    }
}

