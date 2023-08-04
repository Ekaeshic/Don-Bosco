using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Threading.Tasks;

namespace DonBosco.Character
{
    /// <summary>
    /// Handles input
    /// </summary>
    [RequireComponent(typeof(PlayerInput))]
    public class InputManager : MonoBehaviour
    {
        private InputActionMap movementActionMap;
        private InputActionMap UIactionMap;
        private PlayerInput playerInput;

        private Vector2 moveValue = Vector2.zero;
        private bool aimPressed = false;
        private bool interactPressed = false;
        private bool pausePressed = false;
        private bool attackPressed = false;

        private bool submitPressed = false;
        private bool backPressed = false;

        private static InputManager instance;
        public static InputManager Instance => instance;

        #region Events
        public event Action OnAimPressed;
        public event Action OnInteractPressed;
        public event Action OnPausePressed;
        public event Action OnAttackPressed;
        #endregion

        private void Awake() 
        {
            if (instance != null)
            {
                Debug.LogError("Found more than one Input Manager in the scene.");
            }
            instance = this;

            playerInput = GetComponent<PlayerInput>(); 
            movementActionMap = playerInput.actions.FindActionMap("Movement");
            UIactionMap = playerInput.actions.FindActionMap("UI");
        }

        public static async Task<InputManager> GetInstance()
        {
            int waitFrame = 10;
            while(instance == null && waitFrame > 0)
            {
                await Task.Delay(100);
                waitFrame--;
            }
            return instance;
        }

        #region Input
        #region Movement
        public void OnMove(InputValue value)
        {
            moveValue = value.Get<Vector2>();
        }

        public void OnAim(InputValue value)
        {
            aimPressed = value.isPressed;
            OnAimPressed?.Invoke();
        }

        public void OnInteract(InputValue value)
        {
            interactPressed = value.isPressed;
            OnInteractPressed?.Invoke();
        }

        public void OnPause(InputValue value)
        {
            pausePressed = value.isPressed;
            OnPausePressed?.Invoke();
        }

        public void OnAttack(InputValue value)
        {
            attackPressed = value.isPressed;
            OnAttackPressed?.Invoke();
        }
        #endregion



        #region UI
        public void OnSubmit(InputValue value)
        {
            submitPressed = value.isPressed;
        }

        public void OnBack(InputValue value)
        {
            backPressed = value.isPressed;
        }
        #endregion
        #endregion



        #region Getters
        public Vector2 GetMoveValue()
        {
            return moveValue;
        }

        public bool GetAttackValue()
        {
            return attackPressed;
        }


        // for any of the below 'Get' methods, if we're getting it then we're also using it,
        // which means we should set it to false so that it can't be used again until actually
        // pressed again.
        public bool GetAimPressed()
        {
            bool temp = aimPressed;
            aimPressed = false;
            return temp;
        }

        public bool GetInteractPressed()
        {
            bool temp = interactPressed;
            interactPressed = false;
            return temp;
        }

        public bool GetPausePressed()
        {
            bool temp = pausePressed;
            pausePressed = false;
            return temp;
        }



        public bool GetSubmitPressed()
        {
            bool temp = submitPressed;
            submitPressed = false;
            return temp;
        }

        public bool GetBackPressed()
        {
            bool temp = backPressed;
            backPressed = false;
            return temp;
        }
        #endregion

        #region Register
        public bool RegisterSubmitPressed()
        {
            return submitPressed;
        }
        #endregion


        /// <summary>
        /// Set the movement action map
        /// </summary>
        public void SetMovementActionMap(bool value)
        {
            if(value)
            {
                movementActionMap.Enable();
            }
            else
            {
                movementActionMap.Disable();
            }
        }

        public void SetUIActionMap(bool value)
        {
            if(value)
            {
                UIactionMap.Enable();
            }
            else
            {
                UIactionMap.Disable();
            }
        }
    }
}
