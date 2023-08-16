using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

using DG.Tweening;
using DonBosco.ItemSystem;

namespace DonBosco.UI
{
    public class InventorySlotUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [Header("References")]
        [SerializeField] private Image itemImage;
        [SerializeField] private Canvas canvas;
        [HideInInspector] public int slotIndex;
        private Image backgroundImage;
        private InventorySlotListener inventorySlotListener;
        private RectTransform draggedItem;

        private const string SELECTED_COLOR = "#3D3D3D";
        private const string UNSELECTED_COLOR = "#909090";

        private void Awake() 
        {
            inventorySlotListener = GetComponentInParent<InventorySlotListener>();
            backgroundImage = GetComponent<Image>();
        }
        
        public void UpdateUI(Item item)
        {
            if(item != null)
            {
                SpriteRenderer itemSpriteRenderer = item.GetComponent<SpriteRenderer>();
                itemImage.sprite = itemSpriteRenderer.sprite;
                itemImage.color = itemSpriteRenderer.color;
            }
            else
            {
                itemImage.sprite = null;
                itemImage.color = Color.white;
            }
        }

        public void UpdateSelectedUI(bool isSelected)
        {
            if(isSelected)
            {
                backgroundImage.color = ColorUtility.TryParseHtmlString(SELECTED_COLOR, out Color color) ? color : Color.white;
            }
            else
            {
                backgroundImage.color = ColorUtility.TryParseHtmlString(UNSELECTED_COLOR, out Color color) ? color : Color.white;
            }
        }

        #region Events
        public void OnBeginDrag(PointerEventData eventData)
        {
            inventorySlotListener.OnBeginDrag(slotIndex);
            draggedItem = Instantiate(new GameObject(), eventData.position, Quaternion.identity, canvas.transform).AddComponent<RectTransform>();
            draggedItem.position = eventData.position;
            draggedItem.gameObject.AddComponent<Image>().sprite = itemImage.sprite;
            draggedItem.GetComponent<Image>().color = itemImage.color;
            draggedItem.GetComponent<RectTransform>().sizeDelta = itemImage.GetComponent<RectTransform>().sizeDelta;
            draggedItem.GetComponent<Image>().raycastTarget = false;

            itemImage.color = new Color(itemImage.color.r, itemImage.color.g, itemImage.color.b, 0.5f);
        }

        public void OnDrag(PointerEventData eventData)
        {
            draggedItem.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            Destroy(draggedItem.gameObject);
            itemImage.color = new Color(itemImage.color.r, itemImage.color.g, itemImage.color.b, 1f);
        }

        public void OnDrop(PointerEventData eventData)
        {
            inventorySlotListener.OnDrop(slotIndex);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            //Tween the scale of the item
            if(itemImage != null)
                itemImage.rectTransform.DOScale(1.2f, 0.2f);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            //Tween the scale of the item
            if(itemImage != null)
                itemImage.rectTransform.DOScale(1f, 0.2f);
        }
        #endregion
    }
}
