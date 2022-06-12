///<author>ThomasKrahl</author>

using System.Collections.Generic;
using UnityEngine;

namespace IdleGame
{
    public class InventoryView : MonoBehaviour
    {
        #region Fields

        [SerializeField] private List<InventoryViewSlot> slots = new List<InventoryViewSlot>();

        #endregion

        #region UnityFunctions

        private void OnEnable()
        {
            Inventory.ItemChanged += UpdateView;
            UpdateView();
        }

        private void OnDisable()
        {
            Inventory.ItemChanged -= UpdateView;
            //UpdateView();
        }

        #endregion

        private void UpdateView()
        {
            List<InventorySlot> items = Inventory.Instance.GetSlotList();
            
            int index = 0;
            foreach (var slot in slots)
            {             
                if (items[index].ItemData == null)
                {
                    slot.ClearSlot();
                    
                }
                else
                {
                    slot.SetItem(items[index].ItemData, items[index].ItemAmount);
                }              
                index++;
            }
        }
    }
}

