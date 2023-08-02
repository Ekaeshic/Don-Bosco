using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// NPC Script that can be interacted with
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class NPCDialogue : MonoBehaviour, IInteractable
{
    // ScriptableObject Dialogue;
    public void Interact()
    {
        Debug.Log("SELAMAT PAGI!");

        //SHOWDIALOG
        //DialogManager.ShowDialogue(Dialogue);

        //Pickup(SENJATA);
    }
}
