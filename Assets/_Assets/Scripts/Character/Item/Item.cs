using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DonBosco.ItemSystem
{
    public class Item : MonoBehaviour, IPickupable
    {
        [Header("Item Settings")]
        [SerializeField] private bool isPickupable = true;
        [SerializeField] private bool showItemInHand = false;
        public bool ShowItemInHand => showItemInHand;
        public bool IsPickupable { get; set; } = true;

        public void Awake()
        {
            IsPickupable = isPickupable;
        }



        public virtual void Drop()
        {
            gameObject.SetActive(true);
        }

        public virtual void Pickup()
        {
            gameObject.SetActive(false);
        }
    }
}
