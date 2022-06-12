///<author>ThomasKrahl</author>

using System.Collections.Generic;
using UnityEngine;

using IdleGame.Unit.Stats;
using IdleGame.Stats;

namespace IdleGame.UI.Ingame
{
    public class InfoSection : MonoBehaviour
    {
        #region Fields

        [SerializeField] private List<InfoSlot> infoSlots = new List<InfoSlot>();

        #endregion

        #region UnityFunctions

        private void Awake()
        {
            UnitStats.UpdateModifiedStatByType += UpdateInfoSlot;
            UnitStats.UpdateModifiedStatByName += UpdateInfoSlot;
            UnitStats.UpdateModifiedStatMinMaxByType += UpdateInfoSlot;
            UnitStats.UpdateModifiedStatMinMaxByName += UpdateInfoSlot;
            UnitStats.UpdateModifiedStringStatByName += UpdateInfoSlot;
        }

        private void OnDestroy()
        {
            UnitStats.UpdateModifiedStatByType -= UpdateInfoSlot;
            UnitStats.UpdateModifiedStatByName -= UpdateInfoSlot;
            UnitStats.UpdateModifiedStatMinMaxByType -= UpdateInfoSlot;
            UnitStats.UpdateModifiedStatMinMaxByName -= UpdateInfoSlot;
            UnitStats.UpdateModifiedStringStatByName -= UpdateInfoSlot;
        }

        #endregion

        private void UpdateInfoSlot(StatType type, float value)
        {
            if (type == StatType.Invalid) return;

            foreach (var slot in infoSlots)
            {
                if (slot.Type == type)
                {
                    slot.UpdateSlotValue(value);
                    return;
                }
            }
        }

        private void UpdateInfoSlot(string name, float value)
        {
            if(string.IsNullOrEmpty(name)) return;

            foreach (var slot in infoSlots)
            {              
                if (slot.SlotName == name)
                {
                    slot.UpdateSlotValue(value);
                    return;
                }
            }
        }

        private void UpdateInfoSlot(string name, string value)
        {
            if (string.IsNullOrEmpty(name)) return;

            foreach (var slot in infoSlots)
            {
                if (slot.SlotName == name)
                {
                    slot.UpdateSlotValue(value);
                    return;
                }
            }
        }

        private void UpdateInfoSlot(StatType type, float min, float max)
        {
            if (type == StatType.Invalid) return;

            foreach (var slot in infoSlots)
            {
                if (slot.Type == type)
                {
                    slot.UpdateSlotValue(min, max);
                    return;
                }
            }
        }

        private void UpdateInfoSlot(string name, float min, float max)
        {          
            if (string.IsNullOrEmpty(name)) return;

            foreach (var slot in infoSlots)
            {
                if (slot.SlotName == name)
                {
                    slot.UpdateSlotValue(min, max);
                    return;
                }
            }
        }
    }
}

