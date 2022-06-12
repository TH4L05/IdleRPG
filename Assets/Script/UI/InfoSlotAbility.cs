///<author>ThomasKrahl</author>

using UnityEngine;
using TMPro;

namespace IdleGame.UI.Ingame
{

    public class InfoSlotAbility : MonoBehaviour
    {
        [SerializeField] protected string slotName;
        [SerializeField] protected Ability abilityData;
        [SerializeField] protected TextMeshProUGUI slotNameField;
        [SerializeField] protected TextMeshProUGUI slotLevelField;

        private void Awake()
        {
            if (slotNameField != null && !string.IsNullOrEmpty(slotName)) slotNameField.text = slotName;
            if (slotLevelField != null && abilityData != null) slotLevelField.text = abilityData.Level.ToString();
        }
    }
}
