using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DonBosco.ItemSystem
{
    public interface IPickupable
    {
        bool IsPickupable { get; set; }
        /// <summary>
        /// Called when the player picks up the item
        /// </summary>
        void Pickup();
        
        // void Drop();
    }
}
