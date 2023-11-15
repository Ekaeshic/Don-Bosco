using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Networking;

namespace DonBosco.API
{
    public class APIManager : MonoBehaviour
    {
        private static APIManager instance;
        public static APIManager Instance { get { return instance; } }
        public static string baseURL = "https://mtsnuhati.com/sigamingclub/api/inc/";
        public Account account { get; private set; }
        public string previousAccessToken { get; private set;} //Subject to change, whether the API should be able to read the previous access token or not
        const string FILENAME = "/meta.dat";
        public const int ID_GAME = 2; //Don Bosco's ID in the database is 2


        void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else Destroy(gameObject);
            
            // AutoLogin
            //ReadData();
        }

        public void SetAccount(Account account)
        {
            this.account = account;
            // if(account != null)
            // {
            //     WriteData(account.accessToken);
            // }
            // else
            // {
            //     WriteData("");
            // }
        }

        /// <summary>
        /// Writes the access token into a binary file in the local storage
        /// </summary>
        /// <param name="token"></param>
        private void WriteData(string token)
        {
            string path = Application.persistentDataPath + FILENAME;
            //Create a binary formatter which can read binary files
            BinaryFormatter formatter = new BinaryFormatter();
            
            //JSON String into binary
            if(System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            System.IO.FileStream file = System.IO.File.Create(path);
            formatter.Serialize(file, token);
            file.Close();
        }

        /// <summary>
        /// Checks if the user has logged in, by reading data file from the local storage
        /// </summary>
        /// <returns>Returns true if the user has logged in</returns>
        private bool ReadData()
        {
            string path = Application.persistentDataPath + FILENAME;
            if(System.IO.File.Exists(path))
            {
                try
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    System.IO.FileStream file = System.IO.File.Open(path, System.IO.FileMode.Open);
                    previousAccessToken = (string)formatter.Deserialize(file);
                    file.Close();
                    return true;
                }
                catch (System.Exception e)
                {
                    Debug.LogError(e);
                }
            }
            return false;
        }
    }
}
