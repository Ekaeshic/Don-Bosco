using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DonBosco
{
    public class LoadingManager : MonoBehaviour
    {
        private static LoadingManager instance;
        public static LoadingManager Instance { get { return instance; } }
        
        [Header("References")]
        [SerializeField] private GameObject loadingScreen;
        [SerializeField] private Slider slider;
        
        float totalProgress = 0;


        #region Events
        private event Action OnLoadDone;
        #endregion

        private void Awake() {
            instance = this;
        }


        /// <summary>
        /// Shows the loading screen and runs the given operations
        /// </summary>
        /// <param name="operations">The operations to run</param>
        /// <param name="hideLoadingScreenOnFinish">Should the loading screen be hidden when the operations are done?</param>
        /// <param name="OnLoadDone">The action to run when the operations are done</param>
        /// <returns></returns>
        public static LoadingManager ShowLoadingScreen(List<AsyncOperation> operations, bool hideLoadingScreenOnFinish, Action OnLoadDone = null)
        {
            if(OnLoadDone != null)
                instance.OnLoadDone += OnLoadDone;
            instance.loadingScreen.SetActive(true);
            instance.StartCoroutine(instance.LoadAsynchronously(operations, hideLoadingScreenOnFinish));
            return instance;
        }


        private IEnumerator LoadAsynchronously(List<AsyncOperation> operations, bool hideLoadingScreenOnFinish = true)
        {
            if(operations.Count > 0)
            {
                for(int i=0; i<operations.Count;i++)
                {
                    while(!operations[i].isDone)
                    {
                        totalProgress = 0;
                        foreach(AsyncOperation op in operations)
                        {
                            totalProgress += op.progress;
                        }
                        
                        totalProgress = Mathf.Clamp01(totalProgress/operations.Count);

                        slider.value = totalProgress;
                        yield return null;
                    }
                }
            }
            OnLoadDone?.Invoke();
            if(hideLoadingScreenOnFinish)
                HideLoadingScreen();
        }

        public static LoadingManager HideLoadingScreen()
        {
            instance.loadingScreen.SetActive(false);
            return instance;
        }
    }
}