using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace DonBosco
{
    /// <summary>
    /// Loads the scene (based on gameObject's name)
    /// </summary>
    public class LoadScene : MonoBehaviour
    {
        static List<AsyncOperation> asyncLoad = new List<AsyncOperation>();
        [SerializeField] private bool transitionOutOnLoadDone = true;

        [SerializeField] private UnityEvent OnLoadDone;


        public void AddToLoad(string scene = null)
        {
            string sceneName = string.IsNullOrEmpty(scene) ? gameObject.name : scene;

            bool sceneFound = false;
            //Check if current scene is already loaded
            for(int i = 0; i < SceneManager.sceneCount; i++)
            {
                if(SceneManager.GetSceneAt(i).name == sceneName)
                {
                    Debug.Log("Scene is already loaded");
                    sceneFound = true;
                }
            }
            if(!sceneFound)
            {   
                //Load scene
                asyncLoad.Add(SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive));
            }
        }

        public void ExecuteLoadScene()
        {
            //Show loading screen
            Transition.FadeIn(() => {
                LoadingScreen.ShowLoadingScreen(true,asyncLoad, () => {
                    if(transitionOutOnLoadDone)
                    {
                        Transition.FadeOut(() => {
                            OnLoadDone?.Invoke();
                            return;
                        });
                    }
                    OnLoadDone?.Invoke();
                    asyncLoad.Clear();
                });
            });
        }

        public void AddToUnload(string scene = null)
        {
            string sceneName = string.IsNullOrEmpty(scene) ? gameObject.name : scene;

            bool sceneFound = false;
            //Check if current scene is already loaded
            for(int i = 0; i < SceneManager.sceneCount; i++)
            {
                if(SceneManager.GetSceneAt(i).name == sceneName)
                {
                    asyncLoad.Add(SceneManager.UnloadSceneAsync(sceneName));
                    sceneFound = true;
                }
            }
            if(!sceneFound)
            {
                Debug.LogWarning("Trying to unload a scene that is not loaded");
            }
        }
    }

}