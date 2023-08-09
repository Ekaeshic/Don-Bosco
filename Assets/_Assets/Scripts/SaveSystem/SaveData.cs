using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DonBosco.ItemSystem;

namespace DonBosco
{
    public class SaveData
    {
        #region Player Data
        public Vector3 playerPosition;
        public string currentScene;
        public int playerHealth;
        public Item[] playerInventory;
        #endregion


        #region Game Data
        #endregion


        #region Progression Data
        #endregion
        public string ToJson()
        {
            return JsonUtility.ToJson(this);
        }
    }
}
