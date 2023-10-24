using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace DonBosco
{
    public class MainMenuManager : MonoBehaviour
    {
        private static MainMenuManager instance;
        public static MainMenuManager Instance { get { return instance; } }

        [Header("References")]
        [SerializeField] private GameObject mainMenuCanvas;
        [SerializeField] private GameObject confirmationOverwriteSaveData;
        [SerializeField] private Button newGameButton;
        [SerializeField] private Button continueGameButton;

        [SerializeField] UnityEvent onBackToMainMenu;

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
            CheckProgress();
        }


        public void DeleteSave()
        {
            SaveSystem.SaveManager.Instance.DeleteSaveData();
        }

        public void StartNewGame()
        {
            bool hasSaveData = SaveSystem.SaveManager.Instance.HasSaveData;
            if(hasSaveData)
            {
                confirmationOverwriteSaveData.SetActive(true);
            }
            else
            {
                newGameButton.GetComponent<TransitionCaller>().TransitionFadeOut();
            }
        }
        
        public void PlayGame()
        {
            mainMenuCanvas.SetActive(false);
        }

        public void InitMainMenu()
        {
            CheckProgress();
            mainMenuCanvas.SetActive(true);
            onBackToMainMenu?.Invoke();
        }

        public void CheckProgress()
        {
            bool hasSaveData = SaveSystem.SaveManager.Instance.HasSaveData;
            continueGameButton.interactable = hasSaveData;

            switch(hasSaveData)
            {
                case true:
                    newGameButton.GetComponentInChildren<TMP_Text>().text = "New Game";
                    break;
                case false:
                    newGameButton.GetComponentInChildren<TMP_Text>().text = "Play";
                    break;
            }
        }


        public void QuitGame()
        {
            Application.Quit();
        }
    }
}
