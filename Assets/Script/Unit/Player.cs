///<author>ThomasKrahl</author>

using System.Collections.Generic;
using System;
using UnityEngine;

using IdleGame.Unit.Stats;

namespace IdleGame.Unit
{
    public class Player : Unit
    {
        #region Events

        public static Action<UnitState> PlayerStateChanged;
        public static Action PlayerDied;

        #endregion

        #region SerializedFields

        [SerializeField] [Range(0.1f, 2f)]private float step_Distance = 1f;

        #endregion

        #region PrivateFields

        private PlayerStats playerStats => unitStats as PlayerStats;
        private Enemy enemy;
        private float currentSP;
        private bool spRegenActive;
        private int distance;
        private float accumulated_Distance = 1f;
        private int attackCount;

        #endregion

        #region PublicFields

        public float CurrentSP => currentSP;
        public int Distance => distance;

        #endregion

        #region Initialize and Destroy

        protected override void Initialize()
        {
            Enemy.EnemyIsDead += ResetState;
            base.Initialize();
            currentSP = unitStats.MaxSP;         
        }

        protected override void AdditionalSetup()
        {
            base.AdditionalSetup();

            UnitStats.UpdateModifiedStatMinMaxByName?.Invoke("HP", currentHealth, unitStats.MaxHealth);
            UnitStats.UpdateModifiedStatMinMaxByName?.Invoke("SP", currentSP, unitStats.MaxSP);
            UnitStats.UpdateModifiedStatMinMaxByName?.Invoke("MP", currentMana, unitStats.MaxMana);
            UnitStats.UpdateModifiedStatByName?.Invoke("Distance", distance);
            UnitStats.UpdateModifiedStringStatByName("Status", state.ToString());
        }

        #endregion
     
        #region SkillPoints

        private void DecreaseSP(float amount)
        {
            currentSP -= amount;

            if (currentSP <= 0)
            {
                currentSP = 0;
            }
        }

        private void IncreaseSP(float amount)
        {
            currentSP += amount;

            if (currentSP >= unitStats.MaxSP)
            {
                currentSP = unitStats.MaxSP;
            }
        }

        private void RegenerateSP()
        {
            if (currentSP < unitStats.MaxSP && !spRegenActive)
            {
                InvokeRepeating("SPRegen", 1f, 1f);
                spRegenActive = true;
            }
        }

        private void SPRegen()
        {
            IncreaseSP(unitStats.ManaRegen);

            if (currentSP >= unitStats.MaxSP)
            {
                spRegenActive = false;
                CancelInvoke("SPRegen");
            }
        }

        #endregion*/

        #region State

        private void ChangeMovementState()
        {
            if (state == UnitState.Attack || state == UnitState.Dead) return;

            if (Input.GetKeyDown(KeyCode.Space))
            {               
                if (state == UnitState.Resting)
                {
                    state = UnitState.Running;                  
                }
                else
                {
                    state = UnitState.Resting;                 
                }

                PlayerStateChanged?.Invoke(state);
            }
        }

        private void ResetState(GameObject obj)
        {
            state = UnitState.Running;
        }

        protected override void OnStateIdle()
        {
            base.OnStateIdle();
            Game.Instance.GroundAnim(false, 0f);
        }


        protected override void OnStateMove()
        {
            base.OnStateMove();
            Game.Instance.GroundAnim(true, unitStats.MovementSpeed);
            PlayerStateChanged?.Invoke(state);
            UpdateDistanceTraveled();
        }

        protected override void OnStateAttack()
        {
            base.OnStateAttack();
            Game.Instance.GroundAnim(false, 0f);
        }

        protected override void OnStateDead()
        {           
            base.OnStateDead();
            Game.Instance.GroundAnim(false, 0f);
            PlayerDied?.Invoke();
        }

        #endregion;

        #region Damage and Attack

        protected override void OnCollision(Collider2D collision)
        {
            if (collision.CompareTag("Enemy"))
            {
                enemy = collision.GetComponent<Enemy>();
                if (animator != null) animator.SetInteger("Speed", 0);
                state = UnitState.Resting;
                PlayerStateChanged?.Invoke(state);
                enemy.SetState(UnitState.Attack);
                state = UnitState.Attack;
            }            
        }

        protected override void Attack()
        {
            attackTimer += Time.deltaTime;

            if (attackTimer >= unitStats.AttackSpeed)
            {
                attackTimer = 0f;

                switch (attackCount)
                {
                    case 0:
                        if (animator != null) animator.SetTrigger("Attack1");
                        attackCount++;
                        break;

                    case 1:
                        if (animator != null) animator.SetTrigger("Attack2");
                        attackCount++;
                        break;

                    case 2:
                        if (animator != null) animator.SetTrigger("Attack3");
                        attackCount = 0;
                        break;

                    default:
                        break;
                }

                ProvideDamage();
            }
        }

        protected override void ProvideDamage()
        {
            var attackDamage = UnityEngine.Random.Range(unitStats.DamageMin, unitStats.DamageMax + 0.1f);
            Debug.Log(attackDamage);
            enemy.TakeDamage(attackDamage);
            
        }

        #endregion

        protected override void OnUpdate()
        {
            RegenerateSP();
            ChangeMovementState();
            DevKeys();
        }
        protected override void OnLateUpdate()
        {
            UnitStats.UpdateModifiedStringStatByName("Status", state.ToString());
            UnitStats.UpdateModifiedStatByName?.Invoke("Distance", distance);
            UnitStats.UpdateModifiedStatMinMaxByName?.Invoke("MP", currentMana, unitStats.MaxMana);
            UnitStats.UpdateModifiedStatMinMaxByName?.Invoke("SP", currentSP, unitStats.MaxSP);
            UnitStats.UpdateModifiedStatMinMaxByName?.Invoke("HP", currentHealth, unitStats.MaxHealth);
            UnitStats.UpdateModifiedStringStatByName("Attack", unitStats.DamageMin.ToString("0") + " ~ " + unitStats.DamageMax.ToString("0"));
            playerStats.UpdateValues();
        }

        private void UpdateDistanceTraveled()
        {
            accumulated_Distance += Time.deltaTime;

            if (accumulated_Distance > unitStats.MovementSpeed)
            {
                accumulated_Distance = 0;
                distance++;               
            }

            if ((int)distance % 25 == 0)
            {
                playerStats.UpdateDistanceRPforNextRebirth(1);              
            }
        }

        public void Rebirth()
        {
            playerStats.Rebirth();

            currentHealth = unitStats.MaxHealth;
            currentMana = unitStats.MaxMana;
            currentSP = unitStats.MaxSP;
            distance = 0;
            enemy = null;

            if (animator != null) animator.Play("Idle");
            if (animator != null) animator.SetInteger("Speed", 0);
            state = UnitState.Resting;           
            
            AdditionalSetup();

            isDead = false;        
        }

        protected virtual void DevKeys()
        {
            if (Input.GetKeyDown(KeyCode.F5))
            {
                IncreaseHealth(10f);
            }

            if (Input.GetKeyDown(KeyCode.F6))
            {
                DecreaseHealth(10f);
            }

            if (Input.GetKeyDown(KeyCode.F7))
            {
                IncreaseMana(10f);
            }

            if (Input.GetKeyDown(KeyCode.F8))
            {
                DecreaseMana(10f);
            }
        }

        public void SetData(Dictionary<string, object> profileData)
        {
            currentHealth = (float)profileData["currentHealth"];
            currentMana = (float)profileData["currentMana"];
            currentSP = (float)profileData["currentSP"];
            distance = (int)profileData["distance"];
        }
    }
}
