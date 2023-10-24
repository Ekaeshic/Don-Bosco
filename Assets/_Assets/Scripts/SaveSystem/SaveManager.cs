using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using DonBosco.API;
using UnityEngine;

#if UNITY_EDITOR
using UnityEngine.SceneManagement;
#endif

namespace DonBosco.SaveSystem
{
    public class SaveManager : MonoBehaviour
    {
        private static SaveManager instance;
        public static SaveManager Instance { get { return instance; } }

        [SerializeField] LoadScene loadData;

        private List<ISaveLoad> listeners = new List<ISaveLoad>();
        private SaveData saveData;
        const string FILENAME = "/playerdata.dat";
        public string currentAccountID;

        public bool HasSaveData { get; private set; }
        private bool isDispatching = false;

        private void Awake() 
        {
            instance = this;

            //Try to load the game in the beginning
            // HasSaveData = ReadSaveDataLocal();

            #if UNITY_EDITOR
            for(int i = 0; i < SceneManager.sceneCount; i++)
            {
                Scene scene = SceneManager.GetSceneAt(i);
                if(scene.name == "GAMEPLAY")
                {
                    loadData.ExecuteLoadScene();
                    return;
                }
            }
            #endif
            loadData.ExecuteLoadScene(() => loadData.AddToLoad());
        }



        public async Task SaveGame()
        {
            saveData = new SaveData();
            for(int i = 0; i < listeners.Count; i++)
            {
                await listeners[i].Save(saveData);
            }
            string json = saveData.ToJson();
            WriteSaveDataLocal(json);
            await Task.CompletedTask;
        }

        /// <summary>
        /// Load the game from the save file
        /// </summary>
        /// <returns>True if the save file exists</returns>
        public async Task<bool> LoadGame()
        {
            try
            {
                for(int i = 0; i < listeners.Count; i++)
                {
                    await listeners[i].Load(saveData);
                }
                return true;
            }
            catch (System.ArgumentException e)
            {
                Debug.LogError(e);
                return false;
            }
        }

        
        /// <summary>
        /// Delete the save data from the local file when new game is started
        /// </summary>
        public void DeleteSaveData()
        {
            string path;
            if(string.IsNullOrEmpty(currentAccountID))
            {
                // If the player is not logged in, delete the data from the default file
                path = Application.persistentDataPath + FILENAME;
            }
            else
            {
                // If the player is logged in, delete the data from the file based on the account id
                path = Application.persistentDataPath + currentAccountID + ".dat";
            }

            // Then delete the file
            if(System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
                HasSaveData = false;
                saveData = null;
            }
        }


        /// <summary>
        /// Write the save data to the local file
        /// </summary>
        /// <param name="json"></param>
        private void WriteSaveDataLocal(string json)
        {
            string path;
            if(string.IsNullOrEmpty(currentAccountID))
            {
                // If the player is not logged in, save the data to the default file
                path = Application.persistentDataPath + FILENAME;
            }
            else
            {
                // If the player is logged in, save the data to the file based on the account id
                path = Application.persistentDataPath + currentAccountID + ".dat";
            }
            
            //Create a binary formatter which can read binary files
            BinaryFormatter formatter = new BinaryFormatter();
            
            //JSON String into binary
            if(System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            System.IO.FileStream file = System.IO.File.Create(path);
            formatter.Serialize(file, json);
            file.Close();
            HasSaveData = true;
        }

        /// <summary>
        /// Read the save data from the local file, this is used when the player is not logged in
        /// </summary>
        /// <returns></returns>
        public bool ReadSaveDataLocal()
        {
            saveData = null;

            string path = Application.persistentDataPath + FILENAME;
            if(System.IO.File.Exists(path))
            {
                try
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    System.IO.FileStream file = System.IO.File.Open(path, System.IO.FileMode.Open);
                    string jsonState = (string)formatter.Deserialize(file);
                    file.Close();
                    saveData = JsonUtility.FromJson<SaveData>(jsonState);
                    HasSaveData = true;
                    return true;
                }
                catch (System.Exception e)
                {
                    Debug.LogError(e);
                }
            }
            HasSaveData = false;
            return false;
        }


        /// <summary>
        /// Read the save data from the local file based on the account id, this is used when the player is logged in
        /// </summary>
        /// <param name="id"></param>
        public bool ReadSaveDataAccount(string id)
        {
            saveData = null;

            string fileName = id + ".dat";
            string path = Application.persistentDataPath + fileName;
            if(System.IO.File.Exists(path))
            {
                try
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    System.IO.FileStream file = System.IO.File.Open(path, System.IO.FileMode.Open);
                    string jsonState = (string)formatter.Deserialize(file);
                    file.Close();
                    saveData = JsonUtility.FromJson<SaveData>(jsonState);
                    HasSaveData = true;
                    return true;
                }
                catch (System.Exception e)
                {
                    Debug.LogError(e);
                }
            }
            HasSaveData = false;
            return false;
        }



        #region Subscription
        public void Subscribe(ISaveLoad saveLoad)
        {
            if(isDispatching)
            {
                Debug.LogError($"{saveLoad} is trying to subscribe while dispatching");
                return;
            }
            listeners.Add(saveLoad);
        }

        public void Unsubscribe(ISaveLoad saveLoad)
        {
            if(isDispatching)
            {
                Debug.LogError($"{saveLoad} is trying to unsubscribe while dispatching");
                return;
            }
            listeners.Remove(saveLoad);
        }
        #endregion
    }
}
