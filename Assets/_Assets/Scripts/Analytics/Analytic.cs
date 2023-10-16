using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DonBosco.SaveSystem;
using UnityEngine;

namespace DonBosco.Analytics
{
    public class Analytic : MonoBehaviour, ISaveLoad
    {
        private static Analytic instance;
        public static Analytic Instance;

        public float timeSpentInGame = 0f;

        private void Awake()
        {
            if(instance == null)
            {
                instance = this;
                Instance = instance;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        void OnEnable()
        {
            SaveManager.Instance.Subscribe(this);
        }

        void OnDisable()
        {
            SaveManager.Instance.Unsubscribe(this);
        }


        
        public async Task Save(SaveData saveData)
        {
            saveData.timeSpentInGame = timeSpentInGame;
            await Task.CompletedTask;
        }

        public async Task Load(SaveData saveData)
        {
            timeSpentInGame = saveData.timeSpentInGame;
            await Task.CompletedTask;
        }
    }
}