using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DonBosco.Quests;

namespace DonBosco
{
    public class QuestTrigger : MonoBehaviour
    {
        [SerializeField] private QuestInfoSO questInfo = null;
        [SerializeField] private QuestState questState = QuestState.Active;



        public void EnterTrigger()
        {
            switch(questState)
            {
                case QuestState.Active:
                    GameEventsManager.Instance.questEvents.StartQuest(questInfo.id);
                    break;
                case QuestState.CanFinish:
                    GameEventsManager.Instance.questEvents.FinishQuest(questInfo.id);
                    break;
            }
        }
    }
}
