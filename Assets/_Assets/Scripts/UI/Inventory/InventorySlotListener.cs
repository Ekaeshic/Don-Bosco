using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

using DonBosco.ItemSystem;
using System;
using TMPro;

namespace DonBosco.UI
{
    public class InventorySlotListener : MonoBehaviour
    {
        public bool alwaysShow = false;
        [Header("UI Fade")]
        [SerializeField] private float visibleTime = 3f;
        [SerializeField] private float fadeTime = 1f;
        [Header("References")]
        [SerializeField] private TMP_Text usingText;


        private ItemSlot[] itemSlots;
        private int draggedItemIndex = -1;
        private int dropItemIndex = -1;
        private InventorySlotUI[] inventorySlotUIs;
        private CanvasGroup canvasGroup;
        private ItemSO lastUsedItem;

        Tween fadeTween;
        Coroutine showItemEffectTextCoroutine;

        private float visibleTimer = 0f;


        #region MonoBehaviour
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
            Inventory.Instance.OnSelectedItemUsed += SelectedItemUsed;
        }

        private void OnDisable() 
        {
            fadeTween?.Kill();
            Inventory.Instance.OnItemSlotChange -= ItemChanged;
            Inventory.Instance.OnSelectedItemSwitched -= SelectedItemSwitch;
            Inventory.Instance.OnSelectedItemUsed -= SelectedItemUsed;
        }

        void Update()
        {
            DeteroriateUI();
        }
        #endregion


        #region Subscribers
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
        
        private void SelectedItemUsed(int index, bool status, bool success)
        {
            ItemSlot[] itemSlots = Inventory.Instance.ItemSlots;
            Debug.Log(lastUsedItem);
            if(success)
            {
                inventorySlotUIs[index].UseDone();
                if(showItemEffectTextCoroutine != null)
                {
                    StopCoroutine(showItemEffectTextCoroutine);
                }
                showItemEffectTextCoroutine = StartCoroutine(ShowItemEffectText(lastUsedItem.UseEffectText));
            }
            //Start or cancel use animation
            if(status)
            {
                // Null check for itemSO
                if(itemSlots[index] != null)
                {
                    lastUsedItem = itemSlots[index].itemSO;
                }

                //Play use animation
                inventorySlotUIs[index].Use();

                // Show using text
                if(itemSlots[index].itemSO != null)
                {
                    usingText.gameObject.SetActive(true);
                    usingText.text = itemSlots[index].itemSO.UseText;
                }
            }
            else
            {
                //Cancel animation
                inventorySlotUIs[index].UseDone();

                // Hide using text
                if(showItemEffectTextCoroutine == null)
                {
                    usingText.gameObject.SetActive(false);
                }
            }
        }
        #endregion

        private IEnumerator ShowItemEffectText(string text)
        {
            usingText.gameObject.SetActive(true);
            usingText.text = text;
            yield return new WaitForSeconds(4f);
            usingText.gameObject.SetActive(false);
            showItemEffectTextCoroutine = null;
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


        #region EventSystem
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
        #endregion
    }
}
