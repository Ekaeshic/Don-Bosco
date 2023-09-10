using System.Collections;
using System.Collections.Generic;
using DonBosco.ItemSystem;
using UnityEngine;
using UnityEngine.Events;

namespace DonBosco
{
    public class ObjectInteraction : MonoBehaviour, IInteractable
    {
        [SerializeField] private UnityEvent onInteract = null;
        [SerializeField] private bool disableInputMoveOnEnter = false;

        public bool IsInteractable { get; set; } = true;

        public void Interact()
        {
            if (IsInteractable)
            {
                if(disableInputMoveOnEnter)
                {
                    InputManager.Instance.SetMovementActionMap(false);
                }
                onInteract?.Invoke();
            }
        }

        public void Interact(Item item)
        {
            if (IsInteractable)
            {
                if(disableInputMoveOnEnter)
                {
                    InputManager.Instance.SetMovementActionMap(false);
                }
                onInteract?.Invoke();
            }
        }
    }
}
