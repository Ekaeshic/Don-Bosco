using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DonBosco
{
    public class PlayerPositionTeleporter : MonoBehaviour
    {
        [SerializeField] private Vector3 teleportPosition = Vector3.zero;

        private Transform playerTransform = null;

        void Start()
        {
            TimelineManager.Instance.GetPlayerAnimator(out Animator animator, out Animator spriteAnimator);
            playerTransform = animator.transform;
        }



        public void TeleportPlayer()
        {
            playerTransform.position = teleportPosition;
        }
    }
}
