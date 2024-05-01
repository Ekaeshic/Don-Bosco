using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DonBosco
{
    public class PlayerTriggerEnter : MonoBehaviour
    {
        [Header("Player Trigger Enter")]
        [SerializeField] private bool disableInputMoveOnEnter = true;
        private LayerMask playerLayer = 1 << 3;
        [SerializeField] private UnityEvent onPlayerEnter = null;



        private void OnTriggerEnter2D(Collider2D other) 
        {
            if((playerLayer & 1 << other.gameObject.layer) != 0)
            {
                if(disableInputMoveOnEnter)
                {
                    InputManager.Instance.SetMovementActionMap(false);
                }
                onPlayerEnter?.Invoke();
            }
        }
    }
}
