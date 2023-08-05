using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DonBosco.Dialogue;
using DonBosco.ItemSystem;

namespace DonBosco.Character.NPC.Test
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
        public virtual void Interact()
        {
            DialogueManager.GetInstance().EnterDialogueMode(dialogue);
        }

        public virtual void Interact(Item item)
        {
            DialogueManager.GetInstance().EnterDialogueMode(dialogue);
        }
    }
}
