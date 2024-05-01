using System.Collections;
using System.Collections.Generic;
using DonBosco.SaveSystem;
using UnityEngine;

namespace DonBosco
{
    public class GameEnd : MonoBehaviour
    {
        public void EndGame()
        {
            SaveManager.Instance.SaveGame();
            PauseManager.Instance.BackToMainMenu();
        }
    }
}