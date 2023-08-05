using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


namespace DonBosco.Character
{
    /// <summary>
    /// Handles player interaction that attaches to the player
    /// </summary>
    public class PlayerInteraction : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private GameObject hintPrefab;
        [Header("Settings")]
        [SerializeField] private float interactionRadius = 3f;
        [SerializeField] private LayerMask interactionLayer = ~0;
        //[SerializeField] private KeyCode interactKey = KeyCode.E;

        private Collider2D[] hit = new Collider2D[10]; //You can change the size of this array if you need to
        private GameObject selectedObject;
        private GameObject spawnedHint;


        #region Events
        // Event that fired when the player interacts with an object and notifys any script
        // that is listening for this event
        // (Ex. A pause handler script immediately pauses the game when the player interacts with an object)
        public event Action OnInteractEvent;
        #endregion


        #region MonoBehaviour
        private void OnEnable()
        {
            InputManager.Instance.OnInteractPressed += OnInteractPressed;
        }

        private void OnDisable() 
        {
            InputManager.Instance.OnInteractPressed -= OnInteractPressed;
        }

        private void Update() 
        {
            //If you use legacy input, uncomment this and set the interactKey to the key you want to use
            //LegacyInput();
        }

        private void FixedUpdate() 
        {
            //Find an IInteractable object within the interaction radius
            FindInteractable();

            //Clear the overlap alloc (this is important)
            System.Array.Clear(hit, 0, hit.Length);
        }
        #endregion


        /// <summary>
        /// Listens for the interact input by Input System
        /// </summary>
        public void OnInteractPressed()
        {
            //If the selected object is not null
            if(selectedObject != null)
            {
                //If the selected object has an IInteractable component
                if(selectedObject.GetComponent<IInteractable>() != null)
                {
                    DestroyHint();
                    //Interact with the object
                    selectedObject.GetComponent<IInteractable>().Interact();

                    OnInteractEvent?.Invoke();
                }
            }
        }

        /// <summary>
        /// Finds an IInteractable object within the interaction radius
        /// </summary>
        private void FindInteractable()
        {
            //Find all colliders within the interaction radius
            int count = Physics2D.OverlapCircleNonAlloc(transform.position, interactionRadius, hit, interactionLayer);

            GameObject nearestObject = null;
            float nearestDistance = Mathf.Infinity;

            //Loop through all colliders
            for(int i = 0; i < count; i++)
            {
                //If the collider has an IInteractable component
                if(hit[i].GetComponent<IInteractable>() != null)
                {
                    //Check if the object is not interactable then skip it
                    if(!hit[i].GetComponent<IInteractable>().IsInteractable)
                    {
                        continue;
                    }
                    
                    //Search for the nearest object
                    float thisDistance = Vector2.Distance(transform.position, hit[i].transform.position);
                    if(thisDistance < nearestDistance)
                    {
                        nearestObject = hit[i].gameObject;
                        nearestDistance = thisDistance;
                    }
                }
            }

            if(nearestObject != null)
            {
                //Spawn the hint prefab above the object
                if(spawnedHint == null || nearestObject != selectedObject && hintPrefab != null )
                {
                    DestroyHint();
                    float yOffset = (nearestObject.GetComponent<Collider2D>().bounds.size.y / 2f) + 1f;
                    spawnedHint = Instantiate(hintPrefab, nearestObject.transform.position + Vector3.up * yOffset, Quaternion.identity);
                }
            }
            
            //Set the selected object
            selectedObject = nearestObject;

            if(selectedObject == null)
            {
                DestroyHint();
            }
        }

        private void DestroyHint()
        {
            if(spawnedHint != null)
            {
                Destroy(spawnedHint);
            }
        }

        /// <summary>
        /// Legacy input handler, use this if you are not using the new input system
        /// </summary>
        // private void LegacyInput()
        // {
        //     if(Input.GetKeyDown(interactKey))
        //     {
        //         if(selectedObject != null)
        //         {
        //             selectedObject.GetComponent<IInteractable>().Interact();
        //             DestroyHint();

        //             OnInteractEvent?.Invoke();
        //         }
        //     }
        // }



        #if UNITY_EDITOR
        //Draw 2d gizmos
        private void OnDrawGizmosSelected() 
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(transform.position, interactionRadius);
        }
        #endif
    }
}
