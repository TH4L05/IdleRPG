///<author>ThomasKrahl</author>

using UnityEngine;
using TMPro;

using IdleGame.Stats;

namespace IdleGame.UI.Ingame
{
    public class InfoSlot : MonoBehaviour
    {
        #region Fields

        [SerializeField] protected string slotName;
        [SerializeField] protected bool slotNameEqualsType = false;
        [SerializeField] protected bool useSlotNameInField = true;
        [SerializeField] protected StatType type = StatType.Invalid;
        [SerializeField] protected TextMeshProUGUI slotNameField;
        [SerializeField] protected TextMeshProUGUI slotValueField;
        [SerializeField] protected string slotValueFormat = "0.0";
        [SerializeField] protected string slotValueSuffix = string.Empty;

        public string SlotName => slotName;
        public StatType Type => type;

        #endregion

        #region UnityFunctions

        private void Awake()
        {
            Initialize();
        }

        private void OnDestroy()
        {
        }

        #endregion

        protected virtual void Initialize()
        {          
            if (slotNameField == null) return;

            if (slotNameEqualsType)
            {
                slotNameField.text = type.ToString();            
            }
            else if(useSlotNameInField)
            {
                slotNameField.text = slotName;
            }

            if (slotValueField != null) slotValueField.text = "-";
        }

        public virtual void UpdateSlotValue(float amount)
        {
            if (slotValueField == null) return;
            slotValueField.text = amount.ToString(slotValueFormat) + slotValueSuffix;
        }

        public virtual void UpdateSlotValue(string value)
        {
            if (slotValueField == null) return;
            slotValueField.text = value + slotValueSuffix;
        }

        public virtual void UpdateSlotValue(float min, float max)
        {
            if (slotValueField == null) return;
            slotValueField.text = min.ToString(slotValueFormat) + " / " + max.ToString(slotValueFormat) + slotValueSuffix;
        }
    }
}

