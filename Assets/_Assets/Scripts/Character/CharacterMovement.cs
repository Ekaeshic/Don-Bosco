using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;
using System;

namespace DonBosco.Character
{
    /// <summary>
    /// Handles character movement and animation
    /// </summary>
    public class CharacterMovement : MonoBehaviour 
    {
        [Header("References")]
        [SerializeField] private Transform visionTransform;
        [SerializeField] private Animator anim;
        [SerializeField] private CharacterAnimator characterAnimator;
        [Header("Settings")]
        [SerializeField] private float walkSpeed = 5f;
        [SerializeField] private float aimWalkSpeed = 2f;
        
        
        private Rigidbody2D rb;

        private Vector2 movementDirection;
        private Vector2 facingDirection; 
        float facingAngle;
        private bool isAiming = false;

        private const string IDLE = "Idle";
        private const string SIDE = "side";
        private const string UP = "up";
        private const string DOWN = "down";
        string state = "";


        #region MonoBehaviour
        private void Awake() 
        {
            rb = GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            if(!isAiming)
            {
                AnimateMovement();
            }
        }

        void FixedUpdate() 
        {
            Move();
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

            //Rotate towards the target
            Vector2 direction = aimPosition - (Vector2)transform.position;
            AnimateFacingDirection(direction.normalized);
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
            if(visionTransform != null)
            {
                visionTransform?.DOLocalRotate(new Vector3(0, 0, facingAngle), 0.1f);
            }
        }

        /// <summary>
        /// Called once by InputSystem OnMove event trigger
        /// </summary>
        public void Move()
        {
            movementDirection = InputManager.Instance.GetMoveValue().normalized;
            
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

        /// <summary>
        /// Called once by InputSystem OnAim event trigger
        /// </summary>
        public void Aim(bool pressed)
        {
            anim.SetBool("Aiming", pressed);
        }

        private void AnimateMovement()
        {
            anim.speed = 1f;
            bool isMoving = movementDirection.magnitude > 0.1f;
            Vector2 direction = movementDirection.normalized;

            // Y Axis
            if(direction.y > 0.5f)
            {
                state = UP;
            }
            else if(direction.y < -0.5f)
            {
                state = DOWN;
            }

            // X Axis
            else if(direction.x > 0.5f)
            {
                state = SIDE;
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else if(direction.x < -0.5f)
            {
                state = SIDE;
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if(!isMoving && !string.IsNullOrEmpty(state))
            {
                characterAnimator.ChangeState(state+IDLE);
                return;
            }
            characterAnimator.ChangeState(state);
        }

        public void AnimateFacingDirection(Vector2 direction)
        {
            anim.speed = 0.5f;
            bool isMoving = movementDirection.magnitude > 0.1f;

            // Y Axis
            if(direction.y > 0.5f)
            {
                state = UP;
            }
            else if(direction.y < -0.5f)
            {
                state = DOWN;
            }

            // X Axis
            else if(direction.x > 0.5f)
            {
                state = SIDE;
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else if(direction.x < -0.5f)
            {
                state = SIDE;
                transform.localScale = new Vector3(1, 1, 1);
            }
            
            if(!isMoving && !string.IsNullOrEmpty(state))
            {
                characterAnimator.ChangeState(state+IDLE);
                return;
            }
            characterAnimator.ChangeState(state);
        }
        #endregion
    }
}
