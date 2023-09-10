using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DonBosco.Dialogue;
using System;
using UnityEngine.Events;
using DonBosco.Quests;

namespace DonBosco.ItemSystem
{
    public class Selebaran : Item
    {
        [SerializeField] private TextAsset textAsset;

        [SerializeField] private UnityEvent onDialogueEnded;
        [Header("Quest")]
        [SerializeField] private QuestInfoSO questInfoSO;
        private QuestState questState;

        private void OnEnable() 
        {
            GameEventsManager.Instance.questEvents.onQuestStateChange += OnQuestStateChange;

            if(DialogueManager.Instance.GetVariableState("readSelebaranOnce").ToString() == "true")
            {
                gameObject.SetActive(false);
            }
        }

        private void OnDisable() 
        {
            GameEventsManager.Instance.questEvents.onQuestStateChange -= OnQuestStateChange;
        }



        public override void Pickup()
        {
            CheckSelebaranRead();
            base.Pickup();
        }

        private void CheckSelebaranRead()
        {
            if(DialogueManager.Instance.GetVariableState("readSelebaranOnce").ToString() == "true")
            {
                return;
            }
            GameEventsManager.Instance.questEvents.AdvanceQuest(questInfoSO.id);
            
            DialogueManager.Instance.OnDialogueEnded += OnDialogueEnded;
            DialogueManager.Instance.EnterDialogueMode(textAsset);  
        }

        private void OnDialogueEnded()
        {
            DialogueManager.Instance.OnDialogueEnded -= OnDialogueEnded;
            onDialogueEnded?.Invoke();
        }

        private void OnQuestStateChange(Quest quest)
        {
            if(quest.info == questInfoSO)
            {
                questState = quest.state;
            }
        }
    }
}