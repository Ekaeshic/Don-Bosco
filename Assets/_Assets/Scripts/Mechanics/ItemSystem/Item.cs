using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace DonBosco.ItemSystem
{
    [System.Serializable]
    public class Item : MonoBehaviour, IPickupable
    {
        [Header("Item Settings")]
        [SerializeField] private ItemSO itemSO;
        public string ItemName => itemSO.itemName;
        public ItemSO ItemSO => itemSO;
        [SerializeField] protected bool isPickupable = true;
        protected bool showSelectedItem = false;
        public bool ShowSelectedItem => showSelectedItem;
        public bool IsPickupable { get; set; } = true;

        public void Awake()
        {
            IsPickupable = isPickupable;
        }



        // public virtual void Drop()
        // {
        //     GameEventsManager.Instance.playerEvents.ItemDrop(this);
        // }

        public virtual void Pickup()
        {
            Destroy(gameObject);
        }
    }
}
