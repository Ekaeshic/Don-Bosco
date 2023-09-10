using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DonBosco.UI
{
    public class UIManager : MonoBehaviour
    {
        private static UIManager _instance;
        public static UIManager Instance { get { return _instance; } }
        [SerializeField] private List<GameObject> uiScreen;

        private Stack<GameObject> uiStack = new Stack<GameObject>();


        void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                _instance = this;
            }
        }

        void OnEnable()
        {
            GameManager.OnGamePlay += ShowScreenUI;


            GameManager.OnEnterDialogue += HideScreenUI;
            GameManager.OnEnterCutscene += HideScreenUI;
        }

        void OnDisable()
        {
            GameManager.OnGamePlay -= ShowScreenUI;


            GameManager.OnEnterDialogue -= HideScreenUI;
            GameManager.OnEnterCutscene -= HideScreenUI;
        }

        void Update()
        {
            if(InputManager.Instance.GetPausePressed())
            {
                if(uiStack.Count > 0)
                {
                    PopUI();
                }
                else
                {
                    TryToPause();
                }
            }
        }



        private void TryToPause()
        {
            if(GameManager.GameState == GameState.Play && !Transition.IsAnimating)
            {
                PauseManager.Instance.ShowPauseMenu();
                GameManager.PauseGame();
            }
        }



        #region Screen UI
        private void HideScreenUI()
        {
            for(int i = 0; i < uiScreen.Count; i++)
            {
                uiScreen[i].SetActive(false);
            }
        }

        private void ShowScreenUI()
        {
            for (int i = 0; i < uiScreen.Count; i++)
            {
                uiScreen[i].SetActive(true);
            }
        }
        #endregion



        #region UI Stack
        /// <summary>
        /// Add UI to the stack
        /// </summary>
        /// <param name="ui"></param>
        public void PushUI(GameObject ui)
        {
            uiStack.Push(ui);
            ui.SetActive(true);
        }

        /// <summary>
        /// Pop UI from the stack
        /// </summary>
        public void PopUI()
        {
            if(uiStack.Count > 0)
            {
                GameObject ui = uiStack.Pop();
                ui.SetActive(false);
            }
        }

        public void ClearUIStack()
        {
            while(uiStack.Count > 0)
            {
                GameObject ui = uiStack.Pop();
                ui.SetActive(false);
            }
        }
        #endregion
    }
}
