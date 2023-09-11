using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DonBosco
{
    public class GameEnd : MonoBehaviour
    {
        public void EndGame()
        {
            PauseManager.Instance.BackToMainMenu();
        }
    }
}