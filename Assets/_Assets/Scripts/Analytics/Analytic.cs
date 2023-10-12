using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace DonBosco.Analytics
{
    public class Analytic : MonoBehaviour, ISaveLoad
    {
        float timeSpentInGame = 0f;



        private void Update()
        {
            timeSpentInGame += Time.deltaTime;
            Debug.Log("Time spent in game: " + timeSpentInGame);
        }
        
        public Task Save(SaveData saveData)
        {
            saveData.timeSpentInGame = timeSpentInGame;
            return Task.CompletedTask;
        }

        public Task Load(SaveData saveData)
        {
            timeSpentInGame = saveData.timeSpentInGame;
            return Task.CompletedTask;
        }
    }
}