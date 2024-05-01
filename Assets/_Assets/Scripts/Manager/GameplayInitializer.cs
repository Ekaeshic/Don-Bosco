using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DonBosco.SaveSystem;

namespace DonBosco
{
    public class GameplayInitializer : MonoBehaviour
    {
        private async void Start() 
        {
            LoadingScreen.ShowLoadingScreen();
            GameManager.PauseGame();
            try
            {
                if(await SaveManager.Instance.LoadGame())
                {
                    // Success load
                }
                else
                {
                    // Failed load
                }
            }
            catch (System.Exception e)
            {
                // Failed load
                Debug.Log(e);
            }
            finally
            {
                SceneLoader.Instance.LoadCurrentScene();
                LoadingScreen.HideLoadingScreen();
                GameManager.ResumeGame();
            }
        }
    }
}
