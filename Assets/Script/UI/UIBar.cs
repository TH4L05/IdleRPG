///<author>ThomasKrahl</author>

using UnityEngine;
using UnityEngine.UI;
using TMPro;

using IdleGame.Unit.Stats;
using IdleGame.Stats;

namespace IdleGame.UI
{
    public class UIBar : MonoBehaviour
    {
        #region Fields

        [SerializeField] private string barName;
        [SerializeField] private TextMeshProUGUI barNameField;
        [SerializeField] private Image barFill;
        [SerializeField] private TextMeshProUGUI BarTextField;
        [SerializeField] private StatType statType = StatType.Invalid;

        #endregion

        #region UnityFunctions

        private void Awake()
        {
            barNameField.text = barName;

            //UnitStats.UpdateModifiedStatMinMaxType += UpdateInfoSlot;
            UnitStats.UpdateModifiedStatMinMaxByName += UpdateInfoSlot;
        }

        private void OnDestroy()
        {
            //UnitStats.UpdateModifiedStatMinMaxType -= UpdateInfoSlot;
            UnitStats.UpdateModifiedStatMinMaxByName -= UpdateInfoSlot;
        }

        #endregion
       
        /*private void UpdateInfoSlot(StatType type, float min, float max)
        {
            if (type == StatType.Invalid) return;

        }*/

        private void UpdateInfoSlot(string name, float min, float max)
        {
            if (string.IsNullOrEmpty(name)) return;
            if (barName != name) return;

            float barFillAmount = min / max;

            barFill.fillAmount = barFillAmount;
            BarTextField.text = min.ToString("0") + " / " + max.ToString("0");
        }
    }
}

