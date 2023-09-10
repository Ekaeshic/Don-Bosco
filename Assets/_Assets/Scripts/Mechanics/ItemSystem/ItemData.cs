using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DonBosco.ItemSystem
{
    [System.Serializable]
    public class ItemData
    {
        public int itemHash;
        public int amount;

        public ItemData(string itemHash, int amount)
        {
            int hash = Animator.StringToHash(itemHash);
            this.itemHash = hash;
            this.amount = amount;
        }
    }
}
