using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DonBosco.Quests
{
    public class Quest
    {
        public QuestInfoSO info;

        //Data
        public QuestState state;
        public int currentStepIndex;
        public QuestStepState[] questStepStates;


        public Quest(QuestInfoSO questInfo)
        {
            this.info = questInfo;
            state = questInfo.initialState;
            currentStepIndex = 0;
            this.questStepStates = new QuestStepState[info.questSteps.Length];
            for (int i = 0; i < questStepStates.Length; i++)
            {
                questStepStates[i] = new QuestStepState();
            }
        }

        public Quest(QuestInfoSO questInfo, QuestData questData)
        {
            this.info = questInfo;
            state = questData.questState;
            currentStepIndex = questData.questStepIndex;
            questStepStates = questData.questStepStates;

            // if the quest step states and prefabs are different lengths,
            // something has changed during development and the saved data is out of sync.
            if (this.questStepStates.Length != this.info.questSteps.Length)
            {
                Debug.LogWarning("Quest Step Prefabs and Quest Step States are "
                    + "of different lengths. This indicates something changed "
                    + "with the QuestInfo and the saved data is now out of sync. "
                    + "Reset your data - as this might cause issues. QuestId: " + this.info.id);
            }
        }

        
        public bool IsQuestActive() => state == QuestState.Active;
        public bool IsQuestInactive() => state == QuestState.Inactive;
        public bool IsQuestCanFinish() => state == QuestState.CanFinish;
        public bool IsQuestCompleted() => state == QuestState.Completed;


        public void SetQuestState(QuestState state)
        {
            this.state = state;
        }
        
        public void MoveToNextStep()
        {
            currentStepIndex++;
        }

        public QuestData GetQuestData()
        {
            return new QuestData(info.id, state, currentStepIndex, questStepStates);
        }

        public string GetQuestId()
        {
            return info.id;
        }

        internal GameObject InstantiateCurrentQuestStep()
        {
            GameObject questStepPrefab = GetCurrentQuestStepPrefab();
            if (questStepPrefab != null)
            {
                if(SceneLoader.Instance.CurrentScene == questStepPrefab.GetComponent<QuestStep>().scene)
                {
                    QuestStep questStep = GameObject.Instantiate<GameObject>(questStepPrefab)
                        .GetComponent<QuestStep>();
                    questStep.InitializeQuestStep(info.id, currentStepIndex, questStepStates[currentStepIndex].state);
                    return questStep.gameObject;
                }
            }
            return null;
        }

        private GameObject GetCurrentQuestStepPrefab()
        {
            GameObject questStepPrefab = null;
            if (CurrentStepExists())
            {
                questStepPrefab = info.questSteps[currentStepIndex].transform.gameObject;
            }
            else 
            {
                Debug.LogWarning("Tried to get quest step prefab, but stepIndex was out of range indicating that "
                    + "there's no current step: QuestId=" + info.id + ", stepIndex=" + currentStepIndex);
            }
            return questStepPrefab;
        }

        public void StoreQuestStepState(QuestStepState questStepState, int stepIndex)
        {
            if (stepIndex < questStepStates.Length)
            {
                questStepStates[stepIndex].state = questStepState.state;
            }
            else 
            {
                Debug.LogWarning("Tried to access quest step data, but stepIndex was out of range: "
                    + "Quest Id = " + info.id + ", Step Index = " + stepIndex);
            }
        }

        public bool CurrentStepExists()
        {
            return (currentStepIndex < info.questSteps.Length);
        }


        public bool IsCurrentStep(int stepIndex)
        {
            return (currentStepIndex == stepIndex);
        }
    }

}