///<author>ThomasKrahl</author>

using UnityEngine;

namespace IdleGame
{
    public enum ItemType
    {
        Default,
        Potion,
    }

    public enum ItemInfluencedValue
    {
        Invalid = -1,
        CurrentHealth,
        CurrentMana,
        CurrentSP,
        CurrentEXP,
    }

    [CreateAssetMenu(fileName = "New ItemData", menuName = "IdleGame/Inventory/ItemData")]
    public class ItemData : ScriptableObject
    {
        #region Fields

        [SerializeField] private string itemName;
        [SerializeField] [Multiline(5)] private string tooltip;
        [SerializeField] private ItemType itemType = ItemType.Default;
        [SerializeField] private Sprite itemIcon;
        [SerializeField] private ItemInfluence[] itemInfluencedValues;

        #endregion

        #region PublicFields

        public string ItemName => itemName;
        public string Tooltip => tooltip;
        public ItemType ItemType => itemType;
        public Sprite ItemIcon => itemIcon;

        #endregion

        internal void Use()
        {
            foreach (var item in itemInfluencedValues)
            {
                switch (item.itemInfluencedValue)
                {
                    case ItemInfluencedValue.Invalid:
                        break;
                    case ItemInfluencedValue.CurrentHealth:
                        break;
                    case ItemInfluencedValue.CurrentMana:
                        break;
                    case ItemInfluencedValue.CurrentSP:
                        break;
                    case ItemInfluencedValue.CurrentEXP:
                        break;
                    default:
                        break;
                }
            }
        }
    }

    [System.Serializable]
    public class ItemInfluence
    {
        public ItemInfluencedValue itemInfluencedValue;
        public string amount;
    }
}

