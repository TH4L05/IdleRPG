///<author>ThomasKrahl</author>

namespace IdleGame.Stats
{
    [System.Serializable]
    public class AbilityStatModifier
    {       
        #region Fields

        public ModifierType modifierType;
        public float value;
        public StatType statType;

        #endregion

        public AbilityStatModifier()
        {
        }

        public AbilityStatModifier(StatType statType, ModifierType type, float amount)
        {
            modifierType = type;
            value = amount;
            this.statType = statType;
        }
    }
}

