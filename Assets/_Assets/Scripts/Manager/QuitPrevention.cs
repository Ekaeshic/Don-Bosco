using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitPrevention : MonoBehaviour
{
    static bool WantsToQuit()
    {
        Debug.Log("Player prevented from quitting.");
        return false;
    }

    [RuntimeInitializeOnLoadMethod]
    static void RunOnStart()
    {
        Application.wantsToQuit += WantsToQuit;
    }

    
    public static void RunOnQuit()
    {
        Application.wantsToQuit -= WantsToQuit;
    }
}
