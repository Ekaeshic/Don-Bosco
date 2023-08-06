using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace DonBosco.ItemSystem
{
    public class Item : MonoBehaviour, IPickupable
    {
        [Header("Item Settings")]
        [SerializeField] protected bool isPickupable = true;
        protected bool showSelectedItem = false;
        public bool ShowSelectedItem => showSelectedItem;
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
