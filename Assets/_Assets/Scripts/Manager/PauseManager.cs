using System;
using System.Collections;
using System.Collections.Generic;
using DonBosco.SaveSystem;
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

        public async void BackToMainMenu()
        {
            await Transition.FadeIn();
            await SaveManager.Instance.SaveGame();
            for(int i = 0; i < SceneManager.sceneCount; i++)
            {
                //Check if GAMEPLAY scene is already loaded
                if(SceneManager.GetSceneAt(i).name == "GAMEPLAY")
                {
                    SceneManager.UnloadSceneAsync("GAMEPLAY");
                }
            }
            MainMenuManager.Instance.InitMainMenu();
            SceneLoader.Instance.UnloadCurrentSceneInstantly();
        }
    }
}
