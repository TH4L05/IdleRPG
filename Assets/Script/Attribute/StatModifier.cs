///<author>ThomasKrahl</author>

namespace IdleGame.Stats
{
    public enum ModifierType
    {
        Flat,
        Percent
    }

    [System.Serializable]
    public class StatModifier
    {
        #region Fields

        public readonly ModifierType modifierType;
        public readonly float value;

        #endregion

        public StatModifier()
        {
        }

        public StatModifier(ModifierType type, float amount)
        {
            modifierType = type;
            value = amount;
        }
    }
}