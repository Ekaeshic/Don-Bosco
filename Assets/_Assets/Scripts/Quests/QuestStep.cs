using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DonBosco.Quests
{
    public abstract class QuestStep : MonoBehaviour
    {
        public QuestStepInfoSO questStepInfo;
        public SceneAsset scene;
        private bool isFinished = false;
        private string questId;
        private int stepIndex;

        void OnEnable()
        {
            GameEventsManager.Instance.playerEvents.onChangeScene += OnSceneChange;
        }

        void OnDisable()
        {
            GameEventsManager.Instance.playerEvents.onChangeScene -= OnSceneChange;
        }

        private void OnSceneChange(string sceneName)
        {
            if(sceneName != scene.name)
            {
                Destroy(this.gameObject);
            }
        }

        public void InitializeQuestStep(string questId, int stepIndex, string questStepState)
        {
            this.questId = questId;
            this.stepIndex = stepIndex;
            if (questStepState != null && questStepState != "")
            {
                SetQuestStepState(questStepState);
            }
        }

        protected void FinishQuestStep()
        {
            if (!isFinished)
            {
                isFinished = true;
                GameEventsManager.Instance.questEvents.AdvanceQuest(questId);
                Destroy(this.gameObject);
            }
        }

        protected void InstantFinishQuest()
        {
            if (!isFinished)
            {
                isFinished = true;
                GameEventsManager.Instance.questEvents.FinishQuest(questId);
                Destroy(this.gameObject);
            }
        }

        protected void ChangeState(string newState)
        {
            GameEventsManager.Instance.questEvents.QuestStepStateChange(questId, stepIndex, new QuestStepState(newState));
        }

        protected abstract void SetQuestStepState(string state);
    }
}
