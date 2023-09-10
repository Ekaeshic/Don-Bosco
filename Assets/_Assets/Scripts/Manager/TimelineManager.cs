using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

namespace DonBosco
{
    public class TimelineManager : MonoBehaviour
    {
        private static TimelineManager _instance;
        public static TimelineManager Instance { get { return _instance; } }
        [Header("References")]
        [SerializeField] private Animator playerMovementAnimator;
        [SerializeField] private Animator playerSpriteAnimator;

        [Header("Settings")]
        [SerializeField] private float timeToWaitBeforePlaying = 1f;


        private void Awake() {
            if (_instance != null && _instance != this) 
            {
                Destroy(this.gameObject);
            } else
            {
                _instance = this;
            }
        }



        public void GetPlayerAnimator(out Animator movementAnimator, out Animator spriteAnimator)
        {
            movementAnimator = playerMovementAnimator;
            spriteAnimator = playerSpriteAnimator;
        }
    }
}
