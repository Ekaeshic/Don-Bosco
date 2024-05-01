using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using DonBosco.Quests;
using System;

namespace DonBosco.UI
{
    public class QuestContentUI : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private TMP_Text titleText = null;
        [SerializeField] private TMP_Text descriptionText = null;
        private Quest quest = null;



        public void SetContent(Quest quest)
        {
            this.quest = quest;
            QuestStateChange(quest);
        }

        public void QuestStateChange(Quest quest)
        {
            if(this.quest == quest)
            {
                switch(quest.state)
                {
                    case QuestState.Inactive:
                        gameObject.SetActive(false);
                        break;
                    case QuestState.Active:
                        OnInProgress();
                        break;
                    case QuestState.CanFinish:
                        OnCanFinish();
                        break;
                    case QuestState.Completed:
                        OnComplete();
                        break;
                }
            }
        }

        private void OnInProgress()
        {
            gameObject.SetActive(true);
            titleText.text = quest.info.questName;
            int currentStepIndex = quest.currentStepIndex;
            string description = quest.info.questSteps[currentStepIndex].questStepInfo.taskName;
            descriptionText.text = description;
        }

        private void OnCanFinish()
        {
            gameObject.SetActive(true);
            titleText.text = quest.info.questName;
            int currentStepIndex = quest.currentStepIndex;
            string description = quest.info.questSteps[currentStepIndex-1].questStepInfo.taskName;
            descriptionText.text = description;
            description = "<s>" + descriptionText.text + "</s>";
            descriptionText.text = description;
        }

        private void OnComplete()
        {
            gameObject.SetActive(false);
        }
    }
}
