using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DonBosco.ItemSystem
{
    /// <summary>
    /// ItemData is a serializable class that holds the item hash and the amount of the item
    /// This is used as a format to save the inventory data
    /// </summary>
    [System.Serializable]
    public class ItemData
    {
        public int itemHash;

        public ItemData(string itemHash)
        {
            int hash = Animator.StringToHash(itemHash);
            this.itemHash = hash;
        }
    }
}
