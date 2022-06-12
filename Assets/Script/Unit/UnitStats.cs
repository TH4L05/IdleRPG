///<author>ThomasKrahl</author>

using System;
using System.Collections.Generic;
using UnityEngine;

using IdleGame.Stats;

namespace IdleGame.Unit.Stats
{
    public class UnitStats : MonoBehaviour
    {
        #region Events

        public static Action<StatType, StatModifier, string> AddModifier;
        public static Action<StatType, StatModifier, string> RemoveModifier;

        public static Action<StatType, float> UpdateModifiedStatByType;
        public static Action<StatType, string> UpdateModifiedStringStatByType;
        public static Action<string, float> UpdateModifiedStatByName;
        public static Action<string, string> UpdateModifiedStringStatByName;

        public static Action<StatType, float, float> UpdateModifiedStatMinMaxByType;
        public static Action<string, float, float> UpdateModifiedStatMinMaxByName;

        #endregion

        #region SerializedFields

        [SerializeField] protected int levelMax = 999;
        [SerializeField] protected List<Stat> stats = new List<Stat>();

        #endregion

        #region PrivateFields

        protected int level = 1;
        protected string id;

        #endregion

        #region PublicFields

        public int Level => level;
        public List<Stat> Stats => stats;

        public float MaxHealth => GetStatValue(StatType.HealthMax);
        public float MaxMana => GetStatValue(StatType.ManaMax);
        public float MaxSP => GetStatValue(StatType.SkillPoints);
        public float HealthRegen => GetStatValue(StatType.HealthRegen);
        public float ManaRegen => GetStatValue(StatType.ManaRegen);
        public float SPRegen => GetStatValue(StatType.SkillPointsRegen);
        public float MovementSpeed => GetStatValue(StatType.MovementSpeed);
        public float AttackSpeed => GetStatValue(StatType.AttackSpeed);
        public float DamageMin => GetStatValue(StatType.DamageMin);
        public float DamageMax => GetStatValue(StatType.DamageMax);
        public int Defense => (int)GetStatValue(StatType.Defense);

        #endregion

        #region UnityFunctions

        private void Awake()
        {
            Initialize();          
        }

        private void Start()
        {
            StartSetup();
        }

        private void OnDestroy()
        {
            DestroySetup();
        }

        #endregion

        #region Initialize and Destroy

        protected virtual void Initialize()
        {
            AddModifier += AddStatModifier;
            RemoveModifier += RemoveStatModifier;
            id = gameObject.name + Time.frameCount;
        }

        protected virtual void StartSetup()
        {
            foreach (var stat in stats)
            {
                stat.ResetValue();
                stat.SetId(id);
            }
        }

        protected virtual void DestroySetup()
        {
            AddModifier -= AddStatModifier;
            RemoveModifier -= RemoveStatModifier;
        }

        #endregion

        public float GetStatValue(StatType type)
        {
            foreach (var stat in stats)
            {
                if (stat.StatType == type)
                {
                    return stat.GetValue();
                }
            }
            return 0;
        }

        public void AddStatModifier(StatType type, StatModifier mod, string id)
        {
            if (id != this.id) return;

            foreach (var stat in stats)
            {
                if (stat.StatType == type)
                {
                    stat.AddModifier(mod);
                    return;
                }
            }
        }

        public void RemoveStatModifier(StatType type, StatModifier mod, string id)
        {
            if (id != this.id) return;

            foreach (var stat in stats)
            {
                if (stat.StatType == type)
                {
                    stat.RemoveModifier(mod);
                    return;
                }
            }
        }
    }
}

