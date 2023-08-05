using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DonBosco.Character
{
    /// <summary>
    /// Handles Player state and variables
    /// </summary>
    public class PlayerController : MonoBehaviour
    {
        private PlayerMovement playerMovement;
        private DrawLineRelativeMouse drawLine;

        private Vector2 input;

        private MovementState movementState = MovementState.Walking;
        internal enum MovementState
        {
            Walking,
            Aiming
        }



        #region MonoBehaviour
        private void Awake() 
        {
            playerMovement = GetComponent<PlayerMovement>();
            drawLine = GetComponentInChildren<DrawLineRelativeMouse>();
        }

        private void OnEnable() {
            InputManager.Instance.OnAimPressed += OnAimPressed;
        }

        private void OnDisable() {
            InputManager.Instance.OnAimPressed -= OnAimPressed;
        }

        private void FixedUpdate() {
            switch(movementState)
            {
                case MovementState.Walking:
                    playerMovement.WalkState();
                    break;
                case MovementState.Aiming:
                    playerMovement.AimState(drawLine.AimPosition);

                    //Change the sprite image to face the mouse position
                    Vector2 lookDirection = drawLine.AimPosition - transform.position;
                    float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
                    //continue...
                    break;
            }
        }
        #endregion



        #region Input
        public void OnAimPressed()
        {
            Debug.Log("Aim Pressed");
            bool value = InputManager.Instance.GetAimPressed();
            //Draw the aim line to the mouse position (Top priority)
            switch(value)
            {
                case true:
                    drawLine.DrawLine();
                    break;
                case false:
                    drawLine.RemoveLine();

                    //Reset cursor to center
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.lockState = CursorLockMode.None;
                    break;
            }

            movementState = value ? MovementState.Aiming : MovementState.Walking;
            
            //Hide the cursor when pressed, show it when released
            //Cursor.visible = !value.isPressed;
        }
        #endregion
    }
}
