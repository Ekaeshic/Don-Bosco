using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DonBosco
{
    public class MainMenuManager : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private GameObject mainMenuCanvas;
        [SerializeField] private Button continueGameButton;

        private void OnEnable() 
        {
            bool hasSaveData = SaveSystem.SaveManager.Instance.HasSaveData;
            continueGameButton.interactable = hasSaveData;
        }


        
        public void PlayGame()
        {
            mainMenuCanvas.SetActive(false);
        }

        public void InitMainMenu()
        {
            mainMenuCanvas.SetActive(true);
        }
    }
}
