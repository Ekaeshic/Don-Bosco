using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DonBosco.Quests
{
    [CreateAssetMenu(fileName = "QuestStepInfo", menuName = "ScriptableObjects/Quests/QuestStepInfoSO")]
    public class QuestStepInfoSO : ScriptableObject
    {
        public QuestInfoSO questInfo;
        public string taskName;
        public string taskDescription;
    }
}
