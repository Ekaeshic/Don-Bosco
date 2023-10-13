using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DonBosco.Quests
{
    public class QuestPlayerTrigger : QuestStep
    {
        public void EnterTrigger()
        {
            FinishQuestStep();
        }

        protected override void SetQuestStepState(string state)
        {
            // Do nothing
        }
    }

}