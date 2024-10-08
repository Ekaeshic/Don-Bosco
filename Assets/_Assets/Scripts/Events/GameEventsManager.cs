using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DonBosco
{
    public class GameEventsManager : MonoBehaviour
    {
        public static GameEventsManager Instance { get; private set; }
        public PlayerEvents playerEvents;
        public MiscEvents miscEvents;
        public QuestEvents questEvents;

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("Found more than one Game Events Manager in the scene.");
            }
            Instance = this;

            // initialize all events
            playerEvents = new PlayerEvents();
            miscEvents = new MiscEvents();
            questEvents = new QuestEvents();
        }
    }
}