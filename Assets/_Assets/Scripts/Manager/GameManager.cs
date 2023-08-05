using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager
{
    private static GameState gameState;
    public static GameState GameState => gameState;
    
    #region Events
    public static event Action OnGamePause;
    public static event Action OnGamePlay;
    #endregion

    
    /// <summary>
    /// Pause the game and freeze the time
    /// </summary>
    public static void PauseGame()
    {
        gameState = GameState.Pause;
        Time.timeScale = 0;
        OnGamePause?.Invoke();
    }

    /// <summary>
    /// Resume the game and unfreeze the time
    /// </summary>
    public static void ResumeGame()
    {
        gameState = GameState.Play;
        Time.timeScale = 1;
        OnGamePlay?.Invoke();
    }

    /// <summary>
    /// Set the game state to dialogue
    /// </summary>
}


public enum GameState
{
    Play,
    Pause,
    Dialogue,
    Cutscene
}
