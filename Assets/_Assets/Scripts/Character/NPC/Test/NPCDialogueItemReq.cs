using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DonBosco.Dialogue;
using DonBosco.ItemSystem;
using Ink.Runtime;

namespace DonBosco.Character.NPC.Test
{
    public class NPCDialogueItemReq : NPCDialogue
    {
        public override void Interact()
        {
            if(Inventory.Instance.Contains<Item>(out Item item))
            {
                DialogueManager.GetInstance().EnterDialogueModeManually(dialogue)
                .SetVariableState()["hasItem"] = true;

                DialogueManager.GetInstance().StartDialogue();

                Inventory.Instance.Remove<Item>(item);
            }
            else
            {
                DialogueManager.GetInstance().EnterDialogueMode(dialogue);
            }
        }
    }
}