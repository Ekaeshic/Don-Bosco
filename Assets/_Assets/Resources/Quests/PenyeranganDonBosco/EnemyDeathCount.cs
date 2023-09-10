using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DonBosco.Character;
using System;
using UnityEngine.Events;

namespace DonBosco.Quests
{
    public class EnemyDeathCount : QuestStep
    {
        [Header("Settings")]
        [SerializeField] private int enemiesToKill = 5;

        private EnemyBehaviour[] enemies = null;
        private int currentEnemiesKilled = 0;

        public UnityEvent OnQuestComplete = null;

        void Start()
        {
            enemies = GetComponentsInChildren<EnemyBehaviour>();
            for(int i=0; i<enemies.Length; i++)
            {
                enemies[i].OnDeath += OnAnEnemyDeath;
            }
        }

        private void OnAnEnemyDeath()
        {
            if(currentEnemiesKilled < enemiesToKill)
            {
                currentEnemiesKilled++;
            }
            if(currentEnemiesKilled >= enemiesToKill)
            {
                OnQuestComplete?.Invoke();
                FinishQuestStep();
            }
        }

        private void UpdateState()
        {
            ChangeState(currentEnemiesKilled.ToString());
        }

        protected override void SetQuestStepState(string state)
        {
            currentEnemiesKilled = int.Parse(state);
            UpdateState();
        }
    }
}
