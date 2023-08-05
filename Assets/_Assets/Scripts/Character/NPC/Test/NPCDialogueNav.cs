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
    public class NPCDialogueNav : MonoBehaviour, IInteractable
    {
        public bool IsInteractable { get; set; } = true;
        [SerializeField] private TextAsset dialogue;
        // ScriptableObject Dialogue;

        private void Start() {
            if(DialogueManager.GetInstance().GetVariableState("interactable").ToString() == "false")
            {
                IsInteractable = false;
            }
            else
            {
                IsInteractable = true;
            }   
        }
        
        public void Interact()
        {
            Talk();
        }

        public void Interact(Item item)
        {
            Talk();
        }



        private void Talk()
        {
            DialogueManager.GetInstance().BindExternalFunction("PindahScene", (sceneName) => PindahScene(sceneName));
            DialogueManager.GetInstance().BindExternalFunction("TidakMauInteraksi", (interactable) => {
                IsInteractable = false;
            });

            DialogueManager.GetInstance().EnterDialogueMode(dialogue)
            .OnDialogueDone((variable) => 
            {
                // You can do something here when the dialogue is done
            });
        }

        public void PindahScene(string sceneName)
        {
            Debug.Log("Pindah Scene ke " + sceneName);
        }
    }
}
