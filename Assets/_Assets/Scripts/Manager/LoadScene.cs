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
        List<AsyncOperation> asyncLoad;
        [SerializeField] private bool transitionOutOnLoadDone = true;

        [SerializeField] private UnityEvent<List<AsyncOperation>> additionalLoadOperations;
        [SerializeField] private UnityEvent OnLoadDone;


        public void Load()
        {
            string sceneName = gameObject.name;
            asyncLoad = new List<AsyncOperation>();

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

            //Invoke additional load operations list to be added into the asyncLoad list
            additionalLoadOperations?.Invoke(asyncLoad);   

            //Show loading screen
            Transition.FadeIn(() => {
                LoadingManager.ShowLoadingScreen(asyncLoad, true, () => {
                    if(transitionOutOnLoadDone)
                    {
                        Transition.FadeOut(() => {
                            OnLoadDone?.Invoke();
                            return;
                        });
                    }
                    OnLoadDone?.Invoke();
                });
            });
        }

        public void Unload(List<AsyncOperation> ops)
        {
            string sceneName = gameObject.name;

            bool sceneFound = false;
            //Check if current scene is already loaded
            for(int i = 0; i < SceneManager.sceneCount; i++)
            {
                if(SceneManager.GetSceneAt(i).name == sceneName)
                {
                    ops.Add(SceneManager.UnloadSceneAsync(gameObject.name));
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