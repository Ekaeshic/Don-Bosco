using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DonBosco
{
    public class MainMenuManager : MonoBehaviour
    {
        private static MainMenuManager instance;
        public static MainMenuManager Instance { get { return instance; } }

        [Header("References")]
        [SerializeField] private GameObject mainMenuCanvas;
        [SerializeField] private Button continueGameButton;

        void Awake()
        {
            if(instance != null && instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                instance = this;
            }
        }

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
