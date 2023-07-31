using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DonBosco.Character
{
    /// <summary>
    /// Handles the player movement and animation
    /// </summary>
    [RequireComponent(typeof(PlayerController))]
    public class PlayerMovement : MonoBehaviour 
    {
        [Header("Settings")]
        [SerializeField] private float walkSpeed = 5f;
        [SerializeField] private float aimWalkSpeed = 2f;
        [SerializeField] private float rotationSpeed = 1f;
        
        private Animator anim;
        private Rigidbody2D rb;
        private PlayerController playerController;

        private Vector2 movementDirection;
        private Vector2 lookDirection; 
        private bool isAiming = false;



        #region MonoBehaviour
        private void Awake() 
        {
            anim = GetComponent<Animator>();
            rb = GetComponent<Rigidbody2D>();
            playerController = GetComponent<PlayerController>();
        }

        void FixedUpdate() 
        {
            if(isAiming)
            {
                rb.MovePosition(rb.position + movementDirection * aimWalkSpeed * Time.fixedDeltaTime);
            }
            else
            {
                rb.MovePosition(rb.position + movementDirection * walkSpeed * Time.fixedDeltaTime);
            }
        }
        #endregion


        #region Methods
        public void WalkState()
        {
            isAiming = false;
            anim.SetFloat("Speed", movementDirection.normalized.magnitude);
            anim.SetBool("Aiming", false);
        }
        
        public void AimState(Vector2 aimPosition)
        {
            isAiming = true;
            lookDirection = aimPosition - rb.position;
            float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
            anim.SetFloat("Speed", movementDirection.normalized.magnitude);
            anim.SetBool("Aiming", true);
        }

        public void Move(Vector2 value)
        {
            movementDirection = value.normalized;
        }

        public void Aim(bool pressed)
        {
            anim.SetBool("Aiming", pressed);
        }
        #endregion
    }
}
