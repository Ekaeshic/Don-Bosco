using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DonBosco.ItemSystem;
using System;

namespace DonBosco.UI
{
    public class InventorySlotListener : MonoBehaviour
    {
        private Item[] itemSlots;
        private int draggedItemIndex = -1;
        private int dropItemIndex = -1;
        private InventorySlotUI[] inventorySlotUIs;

        private void Awake() 
        {
            inventorySlotUIs = GetComponentsInChildren<InventorySlotUI>();
            for(int i = 0; i < inventorySlotUIs.Length; i++)
            {
                inventorySlotUIs[i].slotIndex = i;
            }
        }


        private void OnEnable() 
        {
            itemSlots = Inventory.Instance.ItemSlot;
            Inventory.Instance.OnItemSlotChange += UpdateUI;
            Inventory.Instance.OnSelectedItemSwitched += UpdateSelectedUI;
        }

        private void OnDisable() 
        {
            Inventory.Instance.OnItemSlotChange -= UpdateUI;
            Inventory.Instance.OnSelectedItemSwitched -= UpdateSelectedUI;
        }

        private void UpdateSelectedUI()
        {
            throw new NotImplementedException();
        }

        private void UpdateUI()
        {
            Item[] itemSlots = Inventory.Instance.ItemSlot;
            for(int i = 0; i < itemSlots.Length; i++)
            {
                inventorySlotUIs[i].UpdateUI(itemSlots[i]);
            }
        }

        public void OnBeginDrag(int index)
        {
            draggedItemIndex = index;
        }

        public void OnDrop(int index)
        {
            dropItemIndex = index;

            if(draggedItemIndex != -1 && dropItemIndex != -1)
            {
                SwapItemSlot();
            }

            //Reset the indices
            draggedItemIndex = -1;
            dropItemIndex = -1;
        }

        private void SwapItemSlot()
        {
            Inventory.Instance.SwapItemSlot(draggedItemIndex, dropItemIndex);
        }
    }
}
