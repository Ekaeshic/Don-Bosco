using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DonBosco.Character
{
    [RequireComponent(typeof(NPCAttack))]
    public class EnemyBehaviour : MonoBehaviour
    {
        [SerializeField] private EnemyState startState = EnemyState.Alert;

        private EnemyState currentState;



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
    }

    public enum EnemyState
    {
        Idle,
        Patrol,
        Alert,
        Dead
    }
}