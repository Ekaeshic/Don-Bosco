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
        }

        void OnDisable()
        {
            GameManager.OnGamePlay -= ShowScreenUI;


            GameManager.OnEnterDialogue -= HideScreenUI;
        }

        



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
    }
}
