///<author>ThomasKrahl</author>

using UnityEngine;

namespace IdleGame
{
    [System.Serializable]
    public class InventorySlot
    {
        #region Fields

        [SerializeField] private ItemData itemData;
        [SerializeField] private int itemAmount;

        #endregion

        #region PublicFields

        public ItemData ItemData => itemData;
        public int ItemAmount => itemAmount;

        #endregion

        public void AddItem(ItemData data)
        {
            itemData = data;
            IncreaseAmount();
        }

        public void IncreaseAmount()
        {
            itemAmount++;
        }

        public void DecreaseAmount()
        {
            itemAmount--;

            if (itemAmount < 1)
            {
                ClearSlot();
            }
        }

        public void ClearSlot()
        {
            itemData = null;
            itemAmount = 0;
        }
    }
}

