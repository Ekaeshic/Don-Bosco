using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DonBosco.Character
{
    [RequireComponent(typeof(NPCAttack))]
    public class EnemyBehaviour : MonoBehaviour, IDamageable
    {
        [SerializeField] private EnemyState startState = EnemyState.Alert;
        [SerializeField] private CharacterHealthSO healthSetting = null;

        private CharacterHealthSO healthSO;

        private EnemyState currentState;

        void OnEnable()
        {
            healthSO = Instantiate(healthSetting);
            healthSO.OnDeath += OnDeath;
        }

        void OnDisable()
        {
            healthSO.OnDeath -= OnDeath;
        }

        private void Start() 
        {
            currentState = startState;
        }



        private void Update() 
        {
            switch(currentState)
            {
                case EnemyState.Idle:
                    Idle();
                    break;
                case EnemyState.Patrol:
                    Patrol();
                    break;
                case EnemyState.Alert:
                    Alert();
                    break;
                case EnemyState.Dead:
                    Dead();
                    break;
            }
        }

        private void OnDeath()
        {
            Destroy(gameObject);
        }

        private void Dead()
        {
            gameObject.GetComponent<NPCAttack>().SetAlert(false);
        }

        private void Alert()
        {
            gameObject.GetComponent<NPCAttack>().SetAlert(true);
        }

        private void Patrol()
        {
            gameObject.GetComponent<NPCAttack>().SetAlert(false);
        }

        private void Idle()
        {
            gameObject.GetComponent<NPCAttack>().SetAlert(false);
        }



        public void SetState(EnemyState state)
        {
            currentState = state;
        }

        public void TakeDamage(float damage)
        {
            healthSO.TakeDamage(damage);
        }
    }

    public enum EnemyState
    {
        Idle,
        Patrol,
        Alert,
        Dead
    }
}