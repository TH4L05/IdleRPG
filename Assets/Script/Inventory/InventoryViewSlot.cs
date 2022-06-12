///<author>ThomasKrahl</author>

using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace IdleGame
{
    public class InventoryViewSlot : MonoBehaviour
    {
        #region Fields

        [SerializeField] private Image icon;
        [SerializeField] private Sprite defaultSprite;
        [SerializeField] private TextMeshProUGUI slotAmountTextField;
        private ItemData itemData;

        #endregion

        public void SetItem(ItemData data, int stackSize)
        {
            itemData = data;
            icon.sprite = data.ItemIcon;
            slotAmountTextField.text = stackSize.ToString();
        }

        public void ClearSlot()
        {
            itemData = null;
            icon.sprite = defaultSprite;
            slotAmountTextField.text = "0";
        }

        public void UseItem()
        {
            if (itemData != null)
            {
                itemData.Use();
                Inventory.Instance.RemoveItem(itemData);
            }
        }
    }
}

