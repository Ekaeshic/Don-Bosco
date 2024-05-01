using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DonBosco.SaveSystem;
using UnityEngine;
using UnityEngine.UI;

namespace DonBosco.Character
{
    /// <summary>
    /// Handles the player health and UI for the health and game over
    /// </summary>
    public class PlayerHealth : MonoBehaviour, IDamageable, ISaveLoad
    {
        [Header("UI")]
        [SerializeField] private Slider healthSlider;
        [SerializeField] private GameObject gameOverPanel;

        [Header("Health")]
        [SerializeField] private float maxHealth = 100f;
        private float currentHealth;


        #region MonoBehaviour

        void OnEnable()
        {
            SaveManager.Instance.Subscribe(this);
            GameManager.OnGameModeChange += OnGameModeChange;

            GameEventsManager.Instance.playerEvents.onPlayerHeal += Heal;
        }

        void OnDisable()
        {
            SaveManager.Instance.Unsubscribe(this);
            GameManager.OnGameModeChange -= OnGameModeChange;

            GameEventsManager.Instance.playerEvents.onPlayerHeal -= Heal;
        }
        #endregion



        private void OnGameModeChange(GameMode mode)
        {
            if(mode == GameMode.Battle)
            {
                healthSlider.gameObject.SetActive(true);
            }
            else
            {
                healthSlider.gameObject.SetActive(false);
            }
        }

        public void TakeDamage(float damage, GameObject source = null)
        {
            UpdateHealth(currentHealth - damage);

            if(currentHealth <= 0)
            {
                Die();
            }
        }

        private void UpdateHealth(float health)
        {
            currentHealth = health < 0 ? currentHealth = 0 : health > maxHealth ? maxHealth : health;
            healthSlider.value = currentHealth;
            healthSlider.maxValue = maxHealth;
        }

        private void Heal(float amount)
        {
            UpdateHealth(currentHealth + amount);

            if(currentHealth <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            GameManager.SetGameOverState();
            gameOverPanel.SetActive(true);
        }



        #region SaveLoad
        public Task Load(SaveData saveData)
        {
            if(saveData != null)
            {
                UpdateHealth(saveData.playerHealth);
            }
            else
            {
                currentHealth = maxHealth;
                UpdateHealth(currentHealth);
            }
            return Task.CompletedTask;
        }

        public Task Save(SaveData saveData)
        {
            saveData.playerHealth = currentHealth;
            return Task.CompletedTask;
        }
        #endregion
    }
}
