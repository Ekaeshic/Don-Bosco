using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DonBosco.Character
{
    /// <summary>
    /// Draws a line from the player to the mouse position
    /// </summary>
    [RequireComponent(typeof(LineRenderer))]
    public class DrawLineRelativeMouse : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Transform startingLineTransform;
        [SerializeField] private Transform targetTransform;
        [Header("Settings")]
        [SerializeField] private LayerMask targetableLayerMask;
        [SerializeField] private float maxDistance = 10f;

        private LineRenderer lineRenderer;

        private Vector3 aimPosition;
        public Vector3 AimPosition { get => aimPosition; }
        bool isAiming = false;



        #region MonoBehavior
        private void Awake() 
        {
            lineRenderer = GetComponent<LineRenderer>();
        }

        private void Update()
        {
            if (isAiming)
            {
                //Move targetPosition to the mouse position by maxDistance
                aimPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                aimPosition.z = 0;
                aimPosition = Vector3.ClampMagnitude(aimPosition - startingLineTransform.position, maxDistance) + startingLineTransform.position;
                targetTransform.position = aimPosition;


                //Check first whether the mouse is hovering on a Targetable layer
                RaycastHit2D hit = Physics2D.Raycast(aimPosition, Vector2.zero, maxDistance, targetableLayerMask);

                if(hit.collider != null)
                {
                    aimPosition = hit.collider.transform.position;
                    UpdateLine(hit.collider.transform.position);
                }
                else
                {
                    UpdateLine(aimPosition);
                }

                
            }
        }
        #endregion

        

        #region Methods
        /// <summary>
        /// Updates the line renderer
        /// </summary>
        private void UpdateLine(Vector3 target)
        {
            lineRenderer.SetPosition(0, startingLineTransform.position);
            lineRenderer.SetPosition(1, target);
        }

        /// <summary>
        /// Draws a line from the player to the mouse position
        /// </summary>
        public void DrawLine()
        {
            lineRenderer.enabled = true;
            isAiming = true;
        }

        public void RemoveLine()
        {
            lineRenderer.enabled = false;
            isAiming = false;
            lineRenderer.SetPosition(0, Vector3.zero);
            lineRenderer.SetPosition(1, Vector3.zero);
        }
        #endregion
    }
}
