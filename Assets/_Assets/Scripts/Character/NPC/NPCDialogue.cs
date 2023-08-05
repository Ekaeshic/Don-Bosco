using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DonBosco.Dialogue;

namespace DonBosco.Character.NPC
{
    /// <summary>
    /// NPC Script that can be interacted with
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    public class NPCDialogue : MonoBehaviour, IInteractable
    {
        public bool IsInteractable { get; set; } = true;
        [SerializeField] private TextAsset dialogue;
        // ScriptableObject Dialogue;
        public void Interact()
        {
            DialogueManager.GetInstance().EnterDialogueMode(dialogue);
        }
    }
}
