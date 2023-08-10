using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;
using DonBosco;

/// <summary>
/// Handles debug screen
/// </summary>
public class DebugScreen : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private bool showDebug = false;
    [SerializeField] private TMP_Text debugText;

    [Header("Settings")]
    [SerializeField] private bool showFPS = false;

    private int fps;
    private static string log = "";



    private void Awake() 
    {
        debugText.gameObject.SetActive(showDebug);
    }



    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.F3))
        {
            debugText.gameObject.SetActive(!debugText.gameObject.activeSelf);
        }

        if(debugText.gameObject.activeSelf)
        {
            WriteDebug();
        }
    }



    private void WriteDebug()
    {
        if(showFPS)
        {
            debugText.text = "FPS: " + (1f / Time.unscaledDeltaTime).ToString("000");
        }
        debugText.text += $"\n\nGame State: {GameManager.GameState}";

        //Write the global log
        debugText.text += "\n\n" + log;

        //Clear the log
        log = "";
    }



    #region Static Methods
    /// <summary>
    /// Logs a message to the debug screen
    /// </summary>
    public static void Log(string message)
    {
        log += message + "\n";
    }

    /// <summary>
    /// Adds a new line to the debug screen by the specified amount
    /// </summary>
    public static void NewLine(int count = 1)
    {
        for(int i = 0; i < count; i++)
        {
            log += "\n";
        }
    }
    #endregion
}
