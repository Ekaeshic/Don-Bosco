using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DonBosco.ItemSystem;

namespace DonBosco.Character
{
    public class PlayerItem : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private GameObject hintPrefab;
        [Header("Settings")]
        [SerializeField] private float pickupRadius = 3f;
        [SerializeField] private LayerMask interactionLayer = ~0;

        private Collider2D[] hit = new Collider2D[10]; //You can change the size of this array if you need to
        private GameObject selectedObject;
        private GameObject spawnedHint;

        #region Events
        public event Action OnPickup;
        #endregion

        #region MonoBehaviour
        private void Update() 
        {
            if(InputManager.Instance.GetPickupPressed() && selectedObject != null)
            {
                Pickup();
            }
        }

        private void FixedUpdate()
        {
            //Find an IInteractable object within the interaction radius
            FindPickupable();

            //Clear the overlap alloc (this is important)
            System.Array.Clear(hit, 0, hit.Length);
        }
        #endregion

        
        /// <summary>
        /// Finds an IPickupable object within the interaction radius
        /// </summary>
        private void FindPickupable()
        {
            //Find all colliders within the interaction radius
            int count = Physics2D.OverlapCircleNonAlloc(transform.position, pickupRadius, hit, interactionLayer);

            GameObject nearestObject = null;
            float nearestDistance = Mathf.Infinity;

            //Loop through all the colliders that were found
            for(int i=0; i<count; i++)
            {
                if(hit[i].GetComponent<IPickupable>() != null)
                {
                    if(!hit[i].GetComponent<IPickupable>().IsPickupable)
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

        private void Pickup()
        {
            if(selectedObject != null && selectedObject.GetComponent<Item>() != null)
            {
                if(Inventory.Instance.TryAddItem(selectedObject.GetComponent<Item>()))
                {
                    DestroyHint();
                    selectedObject.GetComponent<IPickupable>().Pickup();
                    OnPickup?.Invoke();
                }
            }
        }
    }
}
