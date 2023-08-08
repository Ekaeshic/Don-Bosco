using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DonBosco
{
    public class MainMenuManager : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private GameObject mainMenuCanvas;
        [SerializeField] private Camera mainMenuCamera;

        private void OnEnable() 
        {
            
        }


        
        public void PlayGame()
        {
            mainMenuCamera.gameObject.SetActive(false);
            mainMenuCanvas.SetActive(false);
        }

        public void InitMainMenu()
        {
            mainMenuCamera.gameObject.SetActive(true);
            mainMenuCanvas.SetActive(true);
        }
    }
}
