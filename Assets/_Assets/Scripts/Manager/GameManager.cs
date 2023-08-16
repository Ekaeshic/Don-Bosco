using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DonBosco
{
    public class GameManager
    {
        private static GameState gameState = GameState.Pause;
        public static GameState GameState => gameState;
        
        #region Events
        public static event Action OnGamePause;
        public static event Action OnGamePlay;
        public static event Action OnEnterDialogue;
        public static event Action OnEnterCutscene;
        #endregion

        
        /// <summary>
        /// Pause the game and freeze the time
        /// </summary>
        public static void PauseGame()
        {
            gameState = GameState.Pause;
            Time.timeScale = 0;
            OnGamePause?.Invoke();
            InputManager.Instance.SetMovementActionMap(false);
            InputManager.Instance.SetUIActionMap(true);
        }

        /// <summary>
        /// Resume the game and unfreeze the time
        /// </summary>
        public static void ResumeGame()
        {
            gameState = GameState.Play;
            Time.timeScale = 1;
            OnGamePlay?.Invoke();
            InputManager.Instance.SetMovementActionMap(true);
            InputManager.Instance.SetUIActionMap(false);
        }

        /// <summary>
        /// Set the game state to dialogue
        /// </summary>
        public static void SetDialogueState()
        {
            gameState = GameState.Dialogue;
            DonBosco.InputManager.Instance.SetMovementActionMap(false);
            DonBosco.InputManager.Instance.SetUIActionMap(true);
            OnEnterDialogue?.Invoke();
        }

        /// <summary>
        /// Set the game state to cutscene
        /// </summary>
        public static void SetCutsceneState()
        {
            gameState = GameState.Cutscene;
            DonBosco.InputManager.Instance.SetMovementActionMap(false);
            DonBosco.InputManager.Instance.SetUIActionMap(true);
            OnEnterCutscene?.Invoke();
        }
    }


    public enum GameState
    {
        Play,
        Pause,
        Dialogue,
        Cutscene
    }
}
