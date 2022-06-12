///<author>ThomasKrahl</author>

using System;
using System.Collections.Generic;
using UnityEngine;

using IdleGame.Stats;

namespace IdleGame.Unit.Stats
{
    public class PlayerStats : UnitStats
    {
        #region Events

        public static Action<string, float> UpdateUIBar;

        #endregion

        #region SerializedFields

        [SerializeField] private int expToNextLevel = 100;
        [SerializeField][Range(0.1f, 5.0f)] protected float expToNextLevelOffset = 1.2f;
        [SerializeField] private List<PassiveAbility> passiveAbilities = new List<PassiveAbility>();
        [SerializeField] private List<Ability> abilities = new List<Ability>();
        
        #endregion

        #region PrivateFields

        private int currentExp = 0;
        private int attributePoints = 0;
        private int rebirthPointsTotal = 0;
        private int rebirthPointsNextRebirthDistance = 0;
        private int rebirthPointsNextRebirthLevel = 0;
        private int ulimatePoints = 0;

        #endregion

        #region PublicFields

        public string ID => id;
        public int AttributePoints => attributePoints;
        public int RebirthPointsTotal => rebirthPointsTotal;
        public int RebirthPointsNextRebirthDistance => rebirthPointsNextRebirthDistance;
        public int RebirthPointsNextRebirthLevel => rebirthPointsNextRebirthLevel;
        public int UlimatePoints => ulimatePoints;
        public int CurrentExp => currentExp;
        public int ExpToNextLevel => expToNextLevel;

        #endregion

        #region Initialize and Destroy

        protected override void Initialize()
        {         
            base.Initialize();
            Enemy.EnemyIsDead += EnemyDied;

        }

        protected override void StartSetup()
        {
            base.StartSetup();

            UpdateModifiedStatByName?.Invoke("EXP", currentExp);
            UpdateModifiedStatByName?.Invoke("TNL", expToNextLevel);
            UpdateModifiedStatByName?.Invoke("Level", level);
            UpdateModifiedStatByName?.Invoke("AP", attributePoints);
            UpdateModifiedStatMinMaxByName?.Invoke("EXP", currentExp, expToNextLevel);
        }

        protected override void DestroySetup()
        {
            base.DestroySetup();
            Enemy.EnemyIsDead -= EnemyDied;
        }


        #endregion

        #region EXP and Level

        private void EnemyDied(GameObject enemyObj)
        {
            var enemy = enemyObj.GetComponent<Enemy>();
            int exp = enemy.ExpOnDeath;
            AddExperience(exp);
        }

        public void AddExperience(int amount)
        {
            currentExp += amount;
            CheckLevelUp();
        }

        private void CheckLevelUp()
        {
            if (level == levelMax) return;
            if (currentExp >= expToNextLevel)
            {
                LevelUp();
                currentExp = currentExp - expToNextLevel;
                expToNextLevel = (int)(expToNextLevel * expToNextLevelOffset);
                UpdateModifiedStatByName?.Invoke("TNL", expToNextLevel);
            }
            UpdateModifiedStatByName?.Invoke("EXP", currentExp);
            UpdateModifiedStatMinMaxByName?.Invoke("EXP", currentExp, expToNextLevel);
        }

        private void LevelUp()
        {
            level++;
            IncreaseAP(5);
            Debug.Log($"<color=green>PLAYER - increased Level to Level {level}</color>");
            UpdateModifiedStatByName?.Invoke("Level", level);
            UpdateLevelRPforNextRebirth(1);
        }

        #endregion

        #region AttributePoints

        public void DecreaseAP(int amount)
        {
            attributePoints -= amount;
            UpdateModifiedStatByName?.Invoke("AP", attributePoints);
        }

        private void IncreaseAP(int amount)
        {
            attributePoints += amount;
            UpdateModifiedStatByName?.Invoke("AP", attributePoints);
        }

        #endregion

        #region RebirthPoints

        public void UpdateDistanceRPforNextRebirth(int amount)
        {
            rebirthPointsNextRebirthDistance += amount;
        }

        public void UpdateLevelRPforNextRebirth(int amount)
        {
            rebirthPointsNextRebirthLevel += amount;
        }


        public void IncreaseRebirthPoints(int amount)
        {
            rebirthPointsTotal += amount;
        }

        public void DecreaseRebirthPoints(int amount)
        {
            rebirthPointsTotal -= amount;
        }

        #endregion

        public void Rebirth()
        {
            level = 1;
            currentExp = 0;
            expToNextLevel = 25;
            attributePoints = 0;
            rebirthPointsTotal = rebirthPointsNextRebirthDistance + rebirthPointsNextRebirthLevel;

            StartSetup();
        }

        public void UpdateValues()
        {
            float value = 0f;

            foreach (var stat in stats)
            {

                switch (stat.StatType)
                {
                    case IdleGame.Stats.StatType.Invalid:
                        continue;

                    case IdleGame.Stats.StatType.HealthMax:
                        value = stat.GetValue();
                        break;
                    case IdleGame.Stats.StatType.ManaMax:
                        value = stat.GetValue();
                        break;
                    case IdleGame.Stats.StatType.HealthRegen:
                        value = stat.GetValue();
                        break;
                    case IdleGame.Stats.StatType.ManaRegen:
                        value = stat.GetValue();
                        break;
                    case IdleGame.Stats.StatType.SkillPoints:
                        value = stat.GetValue();
                        break;
                    case IdleGame.Stats.StatType.SkillPointsRegen:
                        value = stat.GetValue();
                        break;
                    case IdleGame.Stats.StatType.STR:
                        value = stat.GetValue();
                        break;
                    case IdleGame.Stats.StatType.DEX:
                        value = stat.GetValue();
                        break;
                    case IdleGame.Stats.StatType.AGI:
                        value = stat.GetValue();
                        break;
                    case IdleGame.Stats.StatType.INT:
                        value = stat.GetValue();
                        break;
                    case IdleGame.Stats.StatType.VIT:
                        value = stat.GetValue();
                        break;
                    case IdleGame.Stats.StatType.LUK:
                        value = stat.GetValue();
                        break;
                    case IdleGame.Stats.StatType.AttackSpeed:
                        value =  1 / stat.GetValue();
                        break;
                    case IdleGame.Stats.StatType.DamageMin:
                        value = stat.GetValue();
                        break;
                    case IdleGame.Stats.StatType.DamageMax:
                        value = stat.GetValue();
                        break;
                    case IdleGame.Stats.StatType.Defense:
                        value = stat.GetValue();
                        break;
                    case IdleGame.Stats.StatType.MovementSpeed:
                        break;
                    default:
                        value = 1 / stat.GetValue();
                        break;
                }

                
                UpdateModifiedStatByType?.Invoke(stat.StatType, value);
            }

            UpdateModifiedStatByName?.Invoke("RP", rebirthPointsTotal);
            UpdateModifiedStatByName?.Invoke("UP", ulimatePoints);
        }

        public void SetData(Dictionary<string, object> profileData)
        {
            stats = (List<Stat>)profileData["stats"];
            level = (int)profileData["level"];
            expToNextLevel = (int)profileData["expToNextLevel"];
            attributePoints = (int)profileData["attributePoints"];
            rebirthPointsTotal = (int)profileData["attributePoints"];
            rebirthPointsNextRebirthDistance = (int)profileData["rebirthPointsNextRebirthDistance"];
            rebirthPointsNextRebirthLevel = (int)profileData["rebirthPointsNextRebirthLevel"];
            ulimatePoints = (int)profileData["ulimatePoints"];
        }
    }
}
