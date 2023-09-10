using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DonBosco.Quests
{
    [System.Serializable]
    public class QuestData
    {
        public string questId;
        public QuestState questState;
        public int questStepIndex;
        public QuestStepState[] questStepStates;

        public QuestData(string questID, QuestState questState, int questStepIndex, QuestStepState[] questStepStates)
        {
            this.questId = questID;
            this.questState = questState;
            this.questStepIndex = questStepIndex;
            this.questStepStates = questStepStates;
        }
    }


    
    [System.Serializable]
    public enum QuestState
    {
        Inactive,
        Active,
        CanFinish,
        Completed,
    }
}
