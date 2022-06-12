///<author>ThomasKrahl</author>

using System;
using System.Collections.Generic;
using UnityEngine;
using IdleGame.Unit.Stats;

namespace IdleGame.Stats
{
    public enum StatType
    {
        Invalid = -1,       
        HealthMax,
        ManaMax,
        HealthRegen,
        ManaRegen,
        SkillPoints,
        SkillPointsRegen,
        STR,
        DEX,
        AGI,
        INT,
        VIT,
        LUK,
        AttackSpeed,
        DamageMin,
        DamageMax,
        Defense,
        MovementSpeed,
    }

    [System.Serializable]
    public class Stat
    {
        #region SerializedFields

        [SerializeField] private StatType type;
        [SerializeField] private float baseValue = 1f;
        [SerializeField] private float maxValue = 999f;
        [SerializeField] private StatInfluence[] statImpacts;

        #endregion

        #region PrivateFields

        private float finalValue = 0f;
        private List<StatModifier> modifiers = new List<StatModifier>();
        private bool modified = true;
        private string id;

        #endregion

        #region PublicFields

        public StatType StatType => type;

        #endregion

        public void SetId(string id)
        {
            this.id = id;
        }

        public float GetValue()
        {
            CalculateFinalValue();
            return finalValue;
        }

        private void CalculateFinalValue()
        {
            var value = baseValue;

            foreach (var modifier in modifiers)
            {
                switch (modifier.modifierType)
                {
                    case ModifierType.Flat:
                        value += modifier.value;
                        break;

                    case ModifierType.Percent:
                        value *= 1 + modifier.value;
                        break;

                    default:
                        break;
                }
            }

            finalValue = (float)Math.Round(value, 4);
        }

        #region Modifier

        public void AddModifier(StatModifier modifier)
        {
            modifiers.Add(modifier);
            modified = true;
            CalculateFinalValue();

            //Debug.Log($"<color=green>Add Modifier for Type : {type}  with value : {modifier.value}</color>");

            if (statImpacts.Length != 0)
            {
                foreach (var item in statImpacts)
                {
                    StatModifier modNew = null;
                    switch (item.StatType)
                    {
                        case StatType.HealthMax:
                            modNew = new StatModifier(item.ModifierType, finalValue * 10f * 0.5f);
                            UnitStats.AddModifier(item.StatType, modNew, id);
                            break;
                        case StatType.ManaMax:
                            //TODO: fix error
                            //modNew = new StatModifier(item.ModifierType, finalValue * 10f * 0.5f);
                            //UnitStats.AddModifier(item.StatType, modNew);
                            break;
                        case StatType.HealthRegen:
                            modNew = new StatModifier(item.ModifierType, finalValue * 0.1f);
                            UnitStats.AddModifier(item.StatType, modNew, id);
                            break;
                        case StatType.ManaRegen:
                            modNew = new StatModifier(item.ModifierType, finalValue * 0.1f);
                            UnitStats.AddModifier(item.StatType, modNew, id);
                            break;
                        case StatType.STR:
                            break;
                        case StatType.DEX:
                            break;
                        case StatType.AGI:
                            break;
                        case StatType.INT:
                            break;
                        case StatType.VIT:
                            break;
                        case StatType.LUK:
                            break;
                        case StatType.AttackSpeed:
                            modNew = new StatModifier(item.ModifierType, 0.05f);
                            UnitStats.AddModifier(item.StatType, modNew, id);
                            break;
                        case StatType.Defense:
                            modNew = new StatModifier(item.ModifierType, 0.2f);
                            UnitStats.AddModifier(item.StatType, modNew, id);
                            break;
                        case StatType.MovementSpeed:
                            modNew = new StatModifier(item.ModifierType, - 0.025f);
                            UnitStats.AddModifier(item.StatType, modNew, id);
                            break;

                        case StatType.DamageMin:
                            modNew = new StatModifier(item.ModifierType, finalValue * 0.25f);
                            UnitStats.AddModifier(item.StatType, modNew, id);
                            break;

                        case StatType.DamageMax:
                            modNew = new StatModifier(item.ModifierType, finalValue * 0.25f);
                            UnitStats.AddModifier(item.StatType, modNew, id);
                            break;

                        case StatType.Invalid:
                        default:
                            break;
                    }
                }
            }           
        }

        public void RemoveModifier(StatModifier modifier)
        {
            bool removed = modifiers.Remove(modifier);       
        }

        #endregion

        #region Reset

        public void ResetValue()
        {
            modifiers.Clear();
            modified = true;
            CalculateFinalValue();
        }

        #endregion
    }
}

