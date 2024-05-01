using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DonBosco.ItemSystem
{
    /// <summary>
    /// ItemSlot is a serializable class that holds the item and the amount of the item
    /// </summary>
    [System.Serializable]
    public class ItemSlot
    {
        // public Item item;
        public ItemSO itemSO;
        
        public ItemSlot(ItemSO itemSO)
        {
            this.itemSO = itemSO;
        }

        public ItemData GetItemData()
        {
            if(itemSO == null)
            {
                return null;
            }
            return new ItemData(itemSO.name);
        }

        public void SetItemData(ItemData itemData)
        {
            itemSO = Resources.Load<ItemSO>(itemData.itemHash.ToString());
        }
    }
}
