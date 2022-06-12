///<author>ThomasKrahl</author>

using UnityEngine;

namespace IdleGame.Stats
{
    [System.Serializable]
    public class StatInfluence
    {
        #region SerializedFields

        [SerializeField] private StatType statType;
        [SerializeField] private ModifierType modType;

        #endregion

        #region PublicFields

        public StatType StatType => statType;
        public ModifierType ModifierType => modType;

        #endregion
    }
}
