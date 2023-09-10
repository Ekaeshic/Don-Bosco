using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DonBosco.Quests;
using System;

namespace DonBosco.UI
{
    public class QuestWindowUI : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private GameObject contentParent = null;
        [SerializeField] private GameObject questContentPrefab = null;
        private Dictionary<string, QuestContentUI> questContentUIs = new Dictionary<string, QuestContentUI>();


        void OnEnable()
        {
            GameEventsManager.Instance.questEvents.onQuestStateChange += OnQuestStateChange;
        }

        void OnDisable()
        {
            GameEventsManager.Instance.questEvents.onQuestStateChange -= OnQuestStateChange;
        }

        private void OnQuestStateChange(Quest quest)
        {
            if (questContentUIs.ContainsKey(quest.info.id))
            {
                questContentUIs[quest.info.id].QuestStateChange(quest);
            }
            else
            {
                InstantiateQuestContent(quest);
                questContentUIs[quest.info.id].QuestStateChange(quest);
            }
        }


        private void InstantiateQuestContent(Quest quest)
        {
            GameObject questContent = Instantiate(questContentPrefab, contentParent.transform);
            QuestContentUI questContentUI = questContent.GetComponent<QuestContentUI>();
            questContentUI.SetContent(quest);
            questContentUIs.Add(quest.info.id, questContentUI);
        }
    }

}