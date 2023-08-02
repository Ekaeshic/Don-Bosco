using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

namespace DonBosco.Character
{
    /// <summary>
    /// Handles the player movement and animation
    /// </summary>
    [RequireComponent(typeof(PlayerController))]
    public class PlayerMovement : MonoBehaviour 
    {
        [Header("References")]
        [SerializeField] private Transform visionTransform;
        [Header("Settings")]
        [SerializeField] private float walkSpeed = 5f;
        [SerializeField] private float aimWalkSpeed = 2f;
        
        private Animator anim;
        private Rigidbody2D rb;
        private PlayerController playerController;

        private Vector2 movementDirection;
        private Vector2 facingDirection; 
        float facingAngle;
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

                //Rotate the vision transform when moving and not aiming
                RotateVision();
            }
        }
        #endregion


        #region Methods
        public void WalkState()
        {
            isAiming = false;
            visionTransform.GetComponent<FaceTowardGameObject>().startFacingTarget = false;
            anim.SetFloat("Speed", movementDirection.normalized.magnitude);
            anim.SetBool("Aiming", false);
        }
        
        public void AimState(Vector2 aimPosition)
        {
            isAiming = true;
            visionTransform.GetComponent<FaceTowardGameObject>().startFacingTarget = true;
            anim.SetFloat("Speed", movementDirection.normalized.magnitude);
            anim.SetBool("Aiming", true);
        }

        /// <summary>
        /// Rotates the vision transform to face the direction of the movement
        /// </summary>
        private void RotateVision()
        {
            if(movementDirection != Vector2.zero)
            {
                facingDirection = movementDirection;
                facingAngle = Mathf.Atan2(facingDirection.y, facingDirection.x) * Mathf.Rad2Deg;
            }
            visionTransform.DOLocalRotate(new Vector3(0, 0, facingAngle), 0.1f);
        }

        /// <summary>
        /// Called once by InputSystem OnMove event trigger
        /// </summary>
        public void Move(Vector2 value)
        {
            movementDirection = value.normalized;
        }

        /// <summary>
        /// Called once by InputSystem OnAim event trigger
        /// </summary>
        public void Aim(bool pressed)
        {
            anim.SetBool("Aiming", pressed);
        }
        #endregion
    }
}
