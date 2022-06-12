///<author>ThomasKrahl</author>

using System;
using UnityEngine;

using IdleGame.UI.Ingame;
using IdleGame.Unit.Stats;

namespace IdleGame.Unit
{
    public class Enemy : Unit
    {
        #region Events

        public static Action<GameObject> EnemyIsDead;

        #endregion

        #region SerializedFields

        [SerializeField] private string enemyName;
        [SerializeField] private EnemyInfoBar infoBar;
        [SerializeField] [Range(1.0f, 20.0f)] private float expOnDeathFactor = 1f;

        #endregion

        #region PrivateFields

        private EnemyStats enemyStats => unitStats as EnemyStats;
        private Player player;
        private int expOnDeath = 1;

        #endregion

        #region PublicFields

        public int ExpOnDeath => expOnDeath;

        #endregion

        #region Initialize and Destroy

        public void Setup()
        {
            var playerLevel = Game.Instance.PlayerStats.Level;
            enemyStats.SetStats(playerLevel);
        }

        protected override void AdditionalSetup()
        {
            base.AdditionalSetup();
            player = Game.Instance.Player;
            expOnDeath = (int)(unitStats.Level * expOnDeathFactor + 1);

            if (infoBar != null)
            {
                infoBar.BarVisibility(false);
                infoBar.UpdateBar(currentHealth, unitStats.MaxHealth, enemyName, unitStats.Level);
            }
        }

        protected override void DeathSetup()
        {
            EnemyIsDead?.Invoke(gameObject);
        }

        #endregion

        #region Health

        protected override void IncreaseHealth(float amount)
        {
            base.IncreaseHealth(amount);
            if (infoBar != null) infoBar.UpdateBar(currentHealth, unitStats.MaxHealth, enemyName, unitStats.Level);

            if (currentHealth >= unitStats.MaxHealth)
            {
                if(infoBar !=null) infoBar.BarVisibility(false);
            }
        }

        protected override void DecreaseHealth(float amount)
        {
            base.DecreaseHealth(amount);
            if (infoBar != null) infoBar.UpdateBar(currentHealth, unitStats.MaxHealth, enemyName, unitStats.Level);

            if (currentHealth < unitStats.MaxHealth)
            {
                if (infoBar != null) infoBar.BarVisibility(true);
            }
        }

        #endregion

        #region State

        public void SetState(UnitState state)
        {
            this.state = state;
        }

        protected override void OnStateMove()
        {
            base.OnStateMove();
            transform.position += Vector3.left * unitStats.MovementSpeed * Time.deltaTime;
        }

        protected override void OnStateDead()
        {
            base.OnStateDead();          
            Destroy(gameObject, 1f);
        }

        #endregion

        #region Damage and Attack

        protected override void OnCollision(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {              
                if (animator != null) animator.SetInteger("Speed", 0);
                state = UnitState.Attack;          
            }
        }

        protected override void Attack()
        {
            attackTimer += Time.deltaTime;

            if (attackTimer >= unitStats.AttackSpeed)
            {
                attackTimer = 0f;

                if (animator != null) animator.SetTrigger("Attack");
                ProvideDamage();
            }
        }

        protected override void ProvideDamage()
        {
            var attackDamage = UnityEngine.Random.Range(unitStats.DamageMin, unitStats.DamageMax + 0.1f);
            Debug.Log(attackDamage);
            player.TakeDamage(attackDamage);
        }

        #endregion
    }
}

