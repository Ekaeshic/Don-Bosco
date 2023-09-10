using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

using DonBosco.ItemSystem;
using System;

namespace DonBosco.UI
{
    public class InventorySlotListener : MonoBehaviour
    {
        public bool alwaysShow = false;
        [Header("UI Fade")]
        [SerializeField] private float visibleTime = 3f;
        [SerializeField] private float fadeTime = 1f;


        private ItemSlot[] itemSlots;
        private int draggedItemIndex = -1;
        private int dropItemIndex = -1;
        private InventorySlotUI[] inventorySlotUIs;
        private CanvasGroup canvasGroup;

        Tween fadeTween;

        private float visibleTimer = 0f;

        private void Awake() 
        {
            inventorySlotUIs = GetComponentsInChildren<InventorySlotUI>();
            canvasGroup = GetComponent<CanvasGroup>();
            for(int i = 0; i < inventorySlotUIs.Length; i++)
            {
                inventorySlotUIs[i].slotIndex = i;
            }
        }


        private void OnEnable() 
        {
            itemSlots = Inventory.Instance.ItemSlots;
            Inventory.Instance.OnItemSlotChange += ItemChanged;
            Inventory.Instance.OnSelectedItemSwitched += SelectedItemSwitch;
        }

        private void OnDisable() 
        {
            fadeTween?.Kill();
            Inventory.Instance.OnItemSlotChange -= UpdateUI;
            Inventory.Instance.OnSelectedItemSwitched -= UpdateSelectedUI;
        }

        void Update()
        {
            DeteroriateUI();
        }



        private void ItemChanged()
        {
            UpdateUI();
            WakeUI();
        }

        private void SelectedItemSwitch(int obj)
        {
            UpdateSelectedUI(obj);
            WakeUI();
        }

        private void DeteroriateUI()
        {
            if(alwaysShow)
            {
                return;
            }

            if(visibleTimer > 0f)
            {
                visibleTimer -= Time.deltaTime;
                if(visibleTimer <= 0f)
                {
                    FadeUI();
                }
            }
        }

        private void FadeUI()
        {
            fadeTween = canvasGroup.DOFade(0f, fadeTime).OnComplete(() => {
                canvasGroup.interactable = false;
                canvasGroup.blocksRaycasts = false;
            });
        }

        public void WakeUI()
        {
            fadeTween?.Kill();
            canvasGroup.alpha = 1f;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
            visibleTimer = visibleTime;
        }

        private void UpdateSelectedUI(int index)
        {
            for(int i = 0; i < inventorySlotUIs.Length; i++)
            {
                inventorySlotUIs[i].UpdateSelectedUI(i == index);
            }
        }

        private void UpdateUI()
        {
            ItemSlot[] itemSlots = Inventory.Instance.ItemSlots;
            for(int i = 0; i < itemSlots.Length; i++)
            {
                inventorySlotUIs[i].UpdateUI(itemSlots[i]?.itemSO);
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
