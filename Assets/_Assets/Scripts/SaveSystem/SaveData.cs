using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DonBosco.ItemSystem;
using DonBosco.Quests;

namespace DonBosco
{
    [System.Serializable]
    public class SaveData
    {
        #region Player Data
        public Vector3 playerPosition;
        public string currentScene;
        public int playerHealth;
        public ItemData[] playerInventory;
        #endregion


        #region Game Data
        public string dialogueVariables = null;
        #endregion

        #region Quest
        
        public QuestData[] questData;
        #endregion


        #region Progression Data
        #endregion
        public string ToJson()
        {
            return JsonUtility.ToJson(this);
        }
    }
}
