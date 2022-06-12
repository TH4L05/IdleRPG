///<author>ThomasKrahl</author>

using UnityEngine;
using IdleGame.Stats;

namespace IdleGame.UI.Ingame
{
    public class InfoSlotWithButton : InfoSlot
    {
        #region Fields

        [Header("OnButtonClickUpgrade")]
        [SerializeField] private ModifierType modifierType;
        [SerializeField] private float upgradeAmount;

        #endregion

        public void ConsumeAttributePoints(int amount)
        {
            Game.Instance.PlayerStats.DecreaseAP(amount);
        }

        public void OnButtonClick()
        {
            if (Game.Instance.PlayerStats.AttributePoints < 1)
            {
                Debug.Log("Upgrade can not be apllied - no attributePoints left");
                return;
            }

            StatModifier mod = new StatModifier(modifierType, upgradeAmount);
            Game.Instance.PlayerStats.AddStatModifier(type, mod, Game.Instance.PlayerStats.ID);
        }
    }
}

