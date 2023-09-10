using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DonBosco.ItemSystem
{
    [System.Serializable]
    public class ItemSlot
    {
        // public Item item;
        public ItemSO itemSO;
        public int amount;
        
        public ItemSlot(ItemSO itemSO, int amount)
        {
            this.itemSO = itemSO;
            this.amount = amount;
        }

        public ItemData GetItemData()
        {
            if(itemSO == null)
            {
                return null;
            }
            return new ItemData(itemSO.name, amount);
        }

        public void SetItemData(ItemData itemData)
        {
            itemSO = Resources.Load<ItemSO>(itemData.itemHash.ToString());
            amount = itemData.amount;
        }

        public void AddAmount(int amount)
        {
            this.amount += amount;
        }

        public void RemoveAmount(int amount)
        {
            this.amount -= amount;
        }

        public void SetAmount(int amount)
        {
            this.amount = amount;
        }
    }
}
