using System.Collections;
using System.Collections.Generic;
using DonBosco.ItemSystem;
using UnityEngine;

namespace DonBosco.Quests
{
    [CreateAssetMenu(fileName = "New Quest", menuName = "ScriptableObjects/Quests/QuestInfoSO")]
    public class QuestInfoSO : ScriptableObject
    {
        [field: SerializeField] public string id { get; private set; }
        public string questName;
        public string questDescription;
        public QuestState initialState;
        [Header("Steps")]
        public QuestStep[] questSteps;

        [Header("Settings")]
        public QuestInfoSO[] prequisiteQuests;
        [SerializeField] private ItemReward[] itemRewards;



        private void OnValidate() {
            #if UNITY_EDITOR
            id = this.name;
            UnityEditor.EditorUtility.SetDirty(this);
            #endif
        }
        


        public bool IsInventoryEnough()
        {
            int inventoryEmptySlots = Inventory.Instance.EmptySlotCount();
            if(inventoryEmptySlots < itemRewards.Length)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Give the rewards to the player, if the inventory is not enough, return false <br/>
        /// False exception is needed to be handled by the caller to let the player know that the inventory is not enough.
        /// </summary>
        /// <returns></returns>
        public virtual bool GiveRewards()
        {
            if(!IsInventoryEnough()) return false;

            for(int i = 0; i < itemRewards.Length; i++)
            {
                if(!Inventory.Instance.TryAddItem(itemRewards[i].rewardItem))
                {
                    return false;
                }
            }
            return true;
        }

    }

    [System.Serializable]
    public struct ItemReward
    {
        public Item rewardItem;
    }
}
