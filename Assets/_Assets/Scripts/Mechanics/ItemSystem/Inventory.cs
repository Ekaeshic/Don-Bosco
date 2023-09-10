using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DonBosco.SaveSystem;
using System.Threading.Tasks;

namespace DonBosco.ItemSystem
{
    public class Inventory : MonoBehaviour, ISaveLoad
    {
        [Header("References")]
        [SerializeField] private Transform playerTransform;
        private static Inventory instance;
        public static Inventory Instance => instance;

        private ItemSlot[] itemSlots = new ItemSlot[5];
        public ItemSlot[] ItemSlots => itemSlots;

        private ItemSlot selectedItem = null;
        private int selectedSlot = 0;

        private Dictionary<int, ItemSO> itemSODictionary = new Dictionary<int, ItemSO>();


        #region Events
        public event System.Action OnItemSlotChange;
        public event System.Action<int> OnSelectedItemSwitched;
        #endregion
        

        #region MonoBehaviour
        private void Awake() 
        {
            if (instance != null)
            {
                Debug.LogError("Found more than one Inventory in the scene.");
            }
            instance = this;
        }

        private void OnEnable() 
        {
            SaveManager.Instance.Subscribe(this);
        }

        private void OnDisable() 
        {
            SaveManager.Instance.Unsubscribe(this);
        }

        private void Update() 
        {
            if(InputManager.Instance.GetDropPressed())
            {
                // Drop();
            }
            int numKeysPressed = InputManager.Instance.GetNumKeysPressed();
            if(numKeysPressed > 0)
            {
                SwitchSelectedItem(numKeysPressed-1);
            }

            #if UNITY_EDITOR
            // Debuging();
            #endif    
        }
        #endregion

        private void CreateItemDictionary()
        {
            ItemSO[] itemSOs = Resources.LoadAll<ItemSO>("Items");

            for(int i = 0; i < itemSOs.Length; i++)
            {
                int key = Animator.StringToHash(itemSOs[i].itemName);

                if(!itemSODictionary.ContainsKey(key))
                {
                    itemSODictionary.Add(key, itemSOs[i]);
                }
            }
        }

        private void SwitchSelectedItem(int index)
        {
            //Switch the selected item
            if(itemSlots[index] == null)
            {
                selectedItem = null;
                selectedSlot = index;
            }
            else
            {
                selectedItem = itemSlots[index];
                selectedSlot = index;
            }

            OnSelectedItemSwitched?.Invoke(selectedSlot);
        }

        public void SwapItemSlot(int index, int target)
        {
            if(itemSlots[index] == null)
            {
                return;
            }
            else if(itemSlots[target] == null)
            {
                itemSlots[target] = itemSlots[index];
                itemSlots[index] = null;
            }
            else
            {
                ItemSlot temp = itemSlots[target];
                itemSlots[target] = itemSlots[index];
                itemSlots[index] = temp;
            }

            OnItemSlotChange?.Invoke();
        }
        
        // private void Drop()
        // {   
        //     //Drop the item
        //     if(itemSlots[selectedSlot] == null)
        //     {
        //         Debug.LogWarning("Item is null");
        //         return;
        //     }

        //     selectedItem = itemSlots[selectedSlot].item;
        //     selectedItem.transform.position = playerTransform.position;
        //     selectedItem.Drop();

        //     //Remove the item from the inventory
        //     itemSlots[selectedSlot] = null;

        //     //Clear the selected item
        //     selectedItem = null;
        //     OnItemSlotChange?.Invoke();
        // }

        /// <summary>
        /// Adds an item to the inventory
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool TryAddItem(Item item)
        {
            for(int i = 0; i < itemSlots.Length; i++)
            {
                if(itemSlots[i] == null)
                {
                    ItemSlot itemSlot = new ItemSlot(item.ItemSO, 1);
                    itemSlots[i] = itemSlot;
                    item.transform.localPosition = Vector3.zero;
                    OnItemSlotChange?.Invoke();

                    //Refresh the selected item
                    if(selectedSlot == i)
                    {
                        selectedItem = itemSlot;
                    }
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Returns true if the inventory contains the item
        /// </summary>
        // public bool Contains<T>(out T itemOut) where T : Item
        // {
        //     foreach(ItemSlot i in itemSlots)
        //     {
        //         if(i. is T)
        //         {
        //             itemOut = i.item as T;
        //             return true;
        //         }
        //     }
        //     itemOut = null;
        //     return false;
        // }

        // internal void Remove<T>(T item) where T : Item
        // {
        //     for(int i = 0; i < itemSlots.Length; i++)
        //     {
        //         if(itemSlots[i].item == item)
        //         {
        //             itemSlots[i] = null;
        //             OnItemSlotChange?.Invoke();
        //             return;
        //         }
        //     }
        // }

        public bool Contains(string itemName)
        {
            foreach(ItemSlot itemSlot in itemSlots)
            {
                if(itemSlot != null && itemSlot.itemSO.itemName == itemName)
                {
                    return true;
                }
            }
            return false;
        }

        public void Remove(string itemName)
        {
            for(int i = 0; i < itemSlots.Length; i++)
            {
                if(itemSlots[i].itemSO.itemName == itemName)
                {
                    itemSlots[i] = null;
                    OnItemSlotChange?.Invoke();
                    return;
                }
            }
        }

        /// <summary>
        /// Get currently selected item
        /// </summary>
        /// <returns></returns>
        public ItemSlot GetSelectedItem()
        {
            selectedItem = itemSlots[selectedSlot];
            return selectedItem;
        }
        
        public int EmptySlotCount()
        {
            int count = 0;
            for(int i = 0; i < itemSlots.Length; i++)
            {
                if(itemSlots[i] == null)
                {
                    count++;
                }
            }
            return count;
        }

        #region Visuals
        [Header("Visuals")]
        [SerializeField] private Transform itemSelectedVisualPos;
        private string itemSelectedSortingLayer;


        // private void ShowSelectedItem()
        // {
        //     if(ItemSlot[selectedSlot])
        //     {
        //         if(!ItemSlot[selectedSlot].ShowSelectedItem)
        //         {
        //             return;
        //         }

        //         selectedItem.transform.position = itemSelectedVisualPos.position;
        //         selectedItem.transform.parent = itemSelectedVisualPos;
        //         selectedItem.gameObject.SetActive(true);
        //         selectedItem.GetComponent<Collider>().enabled = false;
        //         itemSelectedSortingLayer = selectedItem.GetComponent<SpriteRenderer>().sortingLayerName;
        //         selectedItem.GetComponent<SpriteRenderer>().sortingLayerName = "AboveGround";
        //     }
        // }

        // private void HideSelectedItem()
        // {
        //     if(ItemSlot[selectedSlot])
        //     {
        //         if(!ItemSlot[selectedSlot].ShowSelectedItem)
        //         {
        //             return;
        //         }

        //         selectedItem.transform.parent = null;
        //         selectedItem.GetComponent<Collider>().enabled = true;
        //         selectedItem.gameObject.SetActive(false);
        //         if(itemSelectedSortingLayer != null)
        //         {
        //             selectedItem.GetComponent<SpriteRenderer>().sortingLayerName = itemSelectedSortingLayer;
        //             itemSelectedSortingLayer = null;
        //         }
        //     }
        // }
        #endregion


        #region Save/Load
        public async Task Save(SaveData saveData)
        {
            //Save the selected slot
            saveData.playerInventory = new ItemData[itemSlots.Length];

            //Save the items
            for(int i = 0; i < itemSlots.Length; i++)
            {
                if(itemSlots[i] != null)
                {
                    saveData.playerInventory[i] = itemSlots[i].GetItemData();
                }
            }
            await Task.CompletedTask;
        }

        public async Task Load(SaveData saveData)
        {
            CreateItemDictionary();
            ItemSlot[] savedItems = new ItemSlot[5];

            if(saveData != null)
            {
                itemSlots = savedItems;
                //Load the items
                for(int i = 0; i < saveData.playerInventory.Length; i++)
                {
                    if(saveData.playerInventory[i] != null && saveData.playerInventory[i].itemHash != 0)
                    {
                        ItemSO itemSO = itemSODictionary[saveData.playerInventory[i].itemHash];
                        savedItems[i] = new ItemSlot(itemSO, saveData.playerInventory[i].amount);
                    }
                }
            }

            itemSlots = savedItems;
            OnItemSlotChange?.Invoke();
            await Task.CompletedTask;
        }
        #endregion


        #region Debug
        private void Debuging()
        {
            DebugScreen.Log("<color=yellow>Inventory</color>");
            DebugScreen.Log("Item list:");
            for(int i = 0; i < itemSlots.Length; i++)
            {
                if(itemSlots[i] != null)
                {
                    DebugScreen.Log($"<color=green>Slot {i}:</color> {itemSlots[i].itemSO.name}");
                }
                else
                {
                    DebugScreen.Log($"<color=red>Slot {i}:</color> Empty");
                }
            }
            DebugScreen.Log($"<color=yellow>Selected Slot:</color> {selectedSlot}. {itemSlots[selectedSlot]?.itemSO.name ?? "Empty"}");
            DebugScreen.Log($"<color=yellow>Numkeys pressed:</color> {InputManager.Instance.GetNumKeysPressed()}");

            DebugScreen.NewLine();
        }
        #endregion
    }
}
