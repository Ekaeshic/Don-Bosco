using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DonBosco.Dialogue;
using DonBosco.ItemSystem;
using DonBosco.Character.NPC.Test;
using System;
using UnityEngine.Events;

namespace DonBosco.Quests
{
    [RequireComponent(typeof(Collider2D))]
    public class DialogueQuest : MonoBehaviour, IInteractable
    {
        [SerializeField] protected TextAsset dialogue;
        [SerializeField] protected DialogueQuestConversation[] dialogueQuestConversations;
        [SerializeField] protected ConversationState[] conversationStates;

        private DialogueQuestConversation currentDialogueQuestConversation;
        private ConversationState currentState;

        public bool IsInteractable { get; set; } = true;

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
                
                if(currentDialogueQuestConversation != null)
                {
                    StartDialogueQuestConversation();
                }
                else if(currentState.knotPath != null)
                {
                    DialogueManager.GetInstance().EnterDialogueMode(dialogue, currentState.knotPath);
                }
                else
                {
                    DialogueManager.GetInstance().EnterDialogueMode(dialogue);
                }
            }
        }

        private void StartDialogueQuestConversation()
        {
            switch(currentDialogueQuestConversation.dialogueQuestBehaviour)
            {
                case DialogueQuestBehaviour.StartQuest:
                    GameEventsManager.Instance.questEvents.StartQuest(currentDialogueQuestConversation.questStepInfo.questInfo.id);
                    break;
                case DialogueQuestBehaviour.AdvanceQuest:
                    GameEventsManager.Instance.questEvents.AdvanceQuest(currentDialogueQuestConversation.questStepInfo.questInfo.id);
                    break;
                case DialogueQuestBehaviour.FinishQuest:
                    GameEventsManager.Instance.questEvents.FinishQuest(currentDialogueQuestConversation.questStepInfo.questInfo.id);
                    break;
            }

            // Remove required items
            if(currentDialogueQuestConversation.requiredItems != null)
            {
                for(int i = 0; i < currentDialogueQuestConversation.requiredItems.Length; i++)
                {
                    ItemSO itemSO = currentDialogueQuestConversation.requiredItems[i];
                    Inventory.Instance.Remove(itemSO.itemName);
                }
            }

            DialogueManager.Instance.OnDialogueEnded += OnDialogueEnded;
            // StartDialogue
            if(currentDialogueQuestConversation.knotPath != null)
            {
                DialogueManager.GetInstance().EnterDialogueMode(dialogue, currentDialogueQuestConversation.knotPath);
            }
            else
            {
                DialogueManager.GetInstance().EnterDialogueMode(dialogue);
            }
        }

        private void OnDialogueEnded()
        {
            DialogueManager.Instance.OnDialogueEnded -= OnDialogueEnded;
            currentDialogueQuestConversation.onDialogueDone?.Invoke();
        }

        private void GetCurrentState()
        {
            currentDialogueQuestConversation = null;
            // get current dialogue quest conversation
            for(int i = 0; i < dialogueQuestConversations.Length; i++)
            {
                DialogueQuestConversation dialogueQuestConversation = dialogueQuestConversations[i];
                if(CheckQuestConditions(dialogueQuestConversation))
                {
                    currentDialogueQuestConversation = dialogueQuestConversation;
                    break;
                }
            }

            if(currentDialogueQuestConversation != null || conversationStates == null || conversationStates?.Length == 0)
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

        private bool CheckQuestConditions(DialogueQuestConversation dqc)
        {
            bool requirementsMet = false;
            for(int i = 0; i < dqc.conditions.Length; i++)
            {
                QuestCondition questCondition = dqc.conditions[i];
                Quest quest = QuestManager.Instance.GetQuestById(questCondition.questInfo.id);
                if(quest.state == questCondition.questState || quest.currentStepIndex == questCondition.questStepIndex)
                {
                    if(dqc.requiredItems != null)
                    {
                        bool hasRequiredItems = true;
                        for(int j = 0; j < dqc.requiredItems.Length; j++)
                        {
                            ItemSO itemSO = dqc.requiredItems[j];
                            if(!Inventory.Instance.Contains(itemSO.itemName))
                            {
                                hasRequiredItems = false;
                                break;
                            }
                        }
                        requirementsMet = hasRequiredItems;
                    }
                    else
                    {
                        requirementsMet = true;
                        break;
                    }
                    break;
                }
            }
            return requirementsMet;
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
    public class DialogueQuestConversation
    {
        public QuestCondition[] conditions;
        public string knotPath;
        public ItemSO[] requiredItems;
        public QuestStepInfoSO questStepInfo;
        public DialogueQuestBehaviour dialogueQuestBehaviour;
        public UnityEvent onDialogueDone;
    }

    [System.Serializable]
    public enum DialogueQuestBehaviour
    {
        StartQuest,
        AdvanceQuest,
        FinishQuest,
    }
}
