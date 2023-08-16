using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DonBosco
{
    [CreateAssetMenu(fileName = "CharacterHealth", menuName = "Character/Health")]
    public class CharacterHealthSO : ScriptableObject
    {
        [Header("Settings")]
        [SerializeField] private float maxHealth = 100f;
        [SerializeField] private float currentHealth = 100f;
        [SerializeField] private float damageMultiplier = 1f;

        private bool isDead = false;

        public event Action OnDeath;
        public event Action<float> OnRegenHealth;

        public void TakeDamage(float damage)
        {
            if (isDead) return;

            currentHealth -= damage * damageMultiplier;

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                isDead = true;
                OnDeath?.Invoke();
            }
        }


        public void RegenHealth(float health)
        {
            if (isDead) return;

            currentHealth += health;

            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }

            OnRegenHealth?.Invoke(currentHealth);
        }

        public void SetHealth(float health)
        {
            if (isDead) return;

            currentHealth = health;

            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }
        }
    }
}
