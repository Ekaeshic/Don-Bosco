using System;
using System.Collections;
using System.Collections.Generic;
using DonBosco.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DonBosco
{
    public class PauseManager : MonoBehaviour
    {
        private static PauseManager instance;
        public static PauseManager Instance { get { return instance; } }

        [SerializeField] private GameObject pauseMenu;
        


        private void Awake() 
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


        public void ShowPauseMenu()
        {
            UIManager.Instance.PushUI(pauseMenu);
        }

        private void HidePauseMenu()
        {
            UIManager.Instance.PopUI();
        }

        public void ResumeGame()
        {
            HidePauseMenu();
            GameManager.ResumeGame();
        }

        public void BackToMainMenu()
        {
            SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName("GAMEPLAY"));
            SceneLoader.Instance.UnloadCurrentSceneInstantly();
            MainMenuManager.Instance.InitMainMenu();
            Transition.FadeIn();
        }
    }
}
