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

        private Item[] itemSlot = new Item[5];
        public Item[] ItemSlot => itemSlot;

        private Item selectedItem = null;
        private int selectedSlot = 0;


        #region Events
        public event System.Action OnItemSlotChange;
        public event System.Action OnSelectedItemSwitched;
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
                Drop();
            }
            int numKeysPressed = InputManager.Instance.GetNumKeysPressed();
            if(numKeysPressed > 0)
            {
                SwitchSelectedItem(numKeysPressed-1);
            }

            #if UNITY_EDITOR
            Debuging();
            #endif    
        }
        #endregion

        private void SwitchSelectedItem(int index)
        {
            //Switch the selected item
            selectedItem = itemSlot[index];
            selectedSlot = index;

            OnSelectedItemSwitched?.Invoke();
        }

        public void SwapItemSlot(int index, int target)
        {
            Item temp = itemSlot[index];
            itemSlot[index] = itemSlot[target];
            itemSlot[target] = temp;

            OnItemSlotChange?.Invoke();
        }
        
        private void Drop()
        {   
            //Drop the item
            if(itemSlot[selectedSlot] == null)
            {
                Debug.LogWarning("Item is null");
                return;
            }

            selectedItem = itemSlot[selectedSlot];
            selectedItem.transform.position = playerTransform.position;
            selectedItem.Drop();

            //Remove the item from the inventory
            itemSlot[selectedSlot] = null;

            //Clear the selected item
            selectedItem = null;
            OnItemSlotChange?.Invoke();
        }

        /// <summary>
        /// Adds an item to the inventory
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool TryAddItem(Item item)
        {
            for(int i = 0; i < itemSlot.Length; i++)
            {
                if(itemSlot[i] == null)
                {
                    itemSlot[i] = item;
                    item.transform.localPosition = Vector3.zero;
                    item.Pickup();
                    OnItemSlotChange?.Invoke();

                    //Refresh the selected item
                    if(selectedSlot == i)
                    {
                        selectedItem = item;
                    }
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Returns true if the inventory contains the item
        /// </summary>
        public bool Contains<T>(out T itemOut) where T : Item
        {
            foreach(Item i in itemSlot)
            {
                if(i is T)
                {
                    itemOut = i as T;
                    return true;
                }
            }
            itemOut = null;
            return false;
        }

        internal void Remove<T>(T item) where T : Item
        {
            for(int i = 0; i < itemSlot.Length; i++)
            {
                if(itemSlot[i] == item)
                {
                    itemSlot[i] = null;
                    OnItemSlotChange?.Invoke();
                    return;
                }
            }
        }

        /// <summary>
        /// Get currently selected item
        /// </summary>
        /// <returns></returns>
        public Item GetSelectedItem()
        {
            selectedItem = itemSlot[selectedSlot];
            return selectedItem;
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
            saveData.playerInventory = new Item[itemSlot.Length];

            //Save the items
            for(int i = 0; i < itemSlot.Length; i++)
            {
                if(itemSlot[i] != null)
                {
                    saveData.playerInventory[i] = itemSlot[i];
                }
            }
            await Task.CompletedTask;
        }

        public async Task Load(SaveData saveData)
        {
            if(saveData == null)
                return;
            Debug.Log("Loading inventory");

            //Load the items
            for(int i = 0; i < saveData.playerInventory.Length; i++)
            {
                if(saveData.playerInventory[i] != null)
                {
                    itemSlot[i] = saveData.playerInventory[i];
                }
            }
            await Task.CompletedTask;
        }
        #endregion


        #region Debug
        private void Debuging()
        {
            DebugScreen.Log("<color=yellow>Inventory</color>");
            DebugScreen.Log("Item list:");
            for(int i = 0; i < itemSlot.Length; i++)
            {
                if(itemSlot[i] != null)
                {
                    DebugScreen.Log($"<color=green>Slot {i}:</color> {itemSlot[i].name}");
                }
                else
                {
                    DebugScreen.Log($"<color=red>Slot {i}:</color> Empty");
                }
            }
            DebugScreen.Log($"<color=yellow>Selected Slot:</color> {selectedSlot}. {itemSlot[selectedSlot]?.name ?? "Empty"}");
            DebugScreen.Log($"<color=yellow>Numkeys pressed:</color> {InputManager.Instance.GetNumKeysPressed()}");

            DebugScreen.NewLine();
        }
        #endregion
    }
}
