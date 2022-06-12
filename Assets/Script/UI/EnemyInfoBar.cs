///<author>ThomasKrahl</author>

using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace IdleGame.UI.Ingame
{
    public class EnemyInfoBar : MonoBehaviour
    {
        #region Fields

        [SerializeField] private Image barFill;
        [SerializeField] private Image barBack;
        [SerializeField] private TextMeshProUGUI barTextField;
        [SerializeField] private TextMeshProUGUI nameField;

        #endregion

        public void BarVisibility(bool visible)
        {
            barFill.gameObject.SetActive(visible);
            barBack.gameObject.SetActive(visible);
        }

        public void UpdateBar(float value, float maxValue, string name, int level)
        {
            if (barFill != null)
            {
                barFill.fillAmount = value / maxValue;
            }
            if (barTextField != null)
            {
                barTextField.text = value.ToString();
            }

            if (nameField != null)
            {
                nameField.text = $"Lvl: {level} - {name}";
            }
        }
    }
}

