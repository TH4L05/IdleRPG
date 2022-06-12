///<author>ThomasKrahl</author>

using System;
using System.Collections.Generic;
using UnityEngine;

namespace IdleGame
{
    public class Inventory : MonoBehaviour
    {
        #region Events

        public static Action ItemChanged;

        #endregion

        #region Fields

        public static Inventory Instance;
        [SerializeField] private List<InventorySlot> inventorySlots = new List<InventorySlot>();
        [SerializeField] [Range(0,999)] private int slotStackSize = 99;

        #endregion

        #region UnityFunctions

        private void Awake()
        {
            Instance = this;        
        }

        #endregion

        public void AddSlot()
        {
            inventorySlots.Add(new InventorySlot());
        }

        public bool AddItem(ItemData data)
        {
            foreach (var slot in inventorySlots)
            {
                if (slot.ItemData.ItemName == data.ItemName && slot.ItemData.ItemType == data.ItemType && slot.ItemAmount < slotStackSize)
                {
                    slot.IncreaseAmount();
                    ItemChanged?.Invoke();
                    return true;
                }
                else if (slot.ItemData == null)
                {
                    slot.AddItem(data);
                    ItemChanged?.Invoke();
                    return true;
                }
            }

            return false;
        }

        public void RemoveItem(ItemData data)
        {
            foreach (var slot in inventorySlots)
            {
                if(slot.ItemData == null) continue;

                if (slot.ItemData.ItemName == data.ItemName)
                {
                    slot.DecreaseAmount();
                    ItemChanged?.Invoke();
                    return;
                }
            }
        }

        public List<InventorySlot> GetSlotList()
        {
            return inventorySlots;
        }

    }
}

