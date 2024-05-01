using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DonBosco.Quests;

namespace DonBosco
{
    public class MapTransporter : MonoBehaviour
    {
        [SerializeField] private TeleportInfo[] teleportInfos;
        [SerializeField] private TeleportInfo defaultTeleportInfo;



        public void TransportPlayer()
        {
            for(int i = 0; i < teleportInfos.Length; i++)
            {
                if(CheckQuestConditions(teleportInfos[i]))
                {
                    teleportInfos[i].sceneLoaderAgent.ExecuteLoadScene();
                    if(teleportInfos[i].playerPositionTeleporter != null)
                    {
                        teleportInfos[i].playerPositionTeleporter.TeleportPlayer(); 
                    }
                    return;
                }
            }
            // If no teleport info is found, use the default one
            defaultTeleportInfo.sceneLoaderAgent.ExecuteLoadScene();
            if(defaultTeleportInfo.playerPositionTeleporter != null)
            {
                defaultTeleportInfo.playerPositionTeleporter.TeleportPlayer(); 
            }
        }


        private bool CheckQuestConditions(TeleportInfo teleportInfo)
        {
            bool requirementsMet = true;
            for(int i = 0; i < teleportInfo.questConditions.Length; i++)
            {
                QuestCondition questCondition = teleportInfo.questConditions[i];
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
    public struct TeleportInfo
    {
        public QuestCondition[] questConditions;
        public SceneLoaderAgent sceneLoaderAgent;
        public PlayerPositionTeleporter playerPositionTeleporter;
    }


    [System.Serializable]
    public struct QuestCondition
    {
        public QuestInfoSO questInfo;
        public QuestState questState;
        public int questStepIndex;
    }
}