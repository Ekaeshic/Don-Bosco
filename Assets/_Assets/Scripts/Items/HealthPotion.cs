using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DonBosco.ItemSystem
{
    [CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Item System/Health Potion")]
    public class HealthPotion : ItemSO
    {
        [SerializeField] public float healAmount = 10f;

        public override void Use()
        {
            Heal();
        }

        private void Heal()
        {
            GameEventsManager.Instance.playerEvents.PlayerHeal(healAmount);
        }
    }
}
