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

        public event Action OnDeath;

        void OnEnable()
        {
            healthSO = Instantiate(healthSetting);
            healthSO.OnDeath += Die;
        }

        void OnDisable()
        {
            healthSO.OnDeath -= Die;
        }

        private void Start() 
        {
            currentState = startState;
        }



        private void Update() 
        {
            if(GameManager.GameState != GameState.Play) return;
            
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

        private void Die()
        {
            OnDeath?.Invoke();
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

        public void TakeDamage(float damage, GameObject source = null)
        {
            healthSO.TakeDamage(damage);
            gameObject.GetComponent<NPCAttack>().GetAttacked(source);
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