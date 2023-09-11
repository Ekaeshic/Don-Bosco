using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DonBosco.Dialogue;
using DonBosco.ItemSystem;
using DonBosco.Quests;

namespace DonBosco.Character.NPC.Test
{
    /// <summary>
    /// NPC Script that can be interacted with
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    public class NPCDialogue : MonoBehaviour, IInteractable
    {
        public bool IsInteractable { get; set; } = true;
        [SerializeField] protected TextAsset dialogue;
        [SerializeField] protected ConversationState[] conversationStates;

        private ConversationState currentState;

        // ScriptableObject Dialogue;
        public virtual void Interact()
        {
            StartDialogue();
        }

        public virtual void Interact(Item item)
        {
            StartDialogue();
        }


        private void StartDialogue()
        {
            if(dialogue == null)
            {
                Debug.LogError("No dialogue or knot path assigned to NPC");
            }
            else
            {
                GetCurrentState();

                if(currentState.knotPath != null)
                {
                    DialogueManager.GetInstance().EnterDialogueMode(dialogue, currentState.knotPath);
                }
                else
                {
                    DialogueManager.GetInstance().EnterDialogueMode(dialogue);
                }
            }
        }

        private void GetCurrentState()
        {
            if(conversationStates == null || conversationStates?.Length == 0)
            {
                return;
            }
            // get current state
            for(int i = 0; i < conversationStates.Length; i++)
            {
                ConversationState conversationState = conversationStates[i];
                if(CheckQuestConditions(conversationState))
                {
                    currentState = conversationState;
                    break;
                }
            }
        }

        private bool CheckQuestConditions(ConversationState conversationState)
        {
            bool requirementsMet = true;
            for(int i = 0; i < conversationState.questConditions.Length; i++)
            {
                QuestCondition questCondition = conversationState.questConditions[i];
                Quest quest = QuestManager.Instance.GetQuestById(questCondition.questInfo.id);
                if(quest.state != questCondition.questState || quest.currentStepIndex != questCondition.questStepIndex)
                {
                    requirementsMet = false;
                    break;
                }
            }
            return requirementsMet;
        }
    }


    [System.Serializable]
    public struct ConversationState
    {
        public QuestCondition[] questConditions;
        public string knotPath;
    }
}
