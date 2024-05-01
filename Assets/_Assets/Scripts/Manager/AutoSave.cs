using System;
using System.Collections;
using System.Collections.Generic;
using DonBosco.SaveSystem;
using UnityEngine;

namespace DonBosco
{
    /// <summary>
    /// This class is used to save the game automatically after a certain delay when the game enters Battle mode
    /// </summary>
    public class AutoSave : MonoBehaviour
    {
        [SerializeField] private float delayInSecond = 1f;


        void OnEnable()
        {
            GameManager.OnGameModeChange += OnGameModeChange;
        }

        void OnDisable()
        {
            GameManager.OnGameModeChange -= OnGameModeChange;
        }

        private void OnGameModeChange(GameMode mode)
        {
            if(mode == GameMode.Battle)
            {
                StartCoroutine(SaveAfterDelay());
            }
            else
            {
                StopAllCoroutines();
            }
        }

        IEnumerator SaveAfterDelay()
        {
            yield return new WaitForSeconds(delayInSecond);
            Debug.Log("Autosaving...");
            SaveManager.Instance.SaveGame();
        }
    }
}
