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
        [SerializeField] public Transform StartingLineTransform;
        [SerializeField] private Transform targetTransform;
        [Header("Settings")]
        [SerializeField] private LayerMask targetableAimLayerMask;
        [SerializeField] private LayerMask collisionAimLayerMask;
        [SerializeField] private float maxDistance = 10f;

        private LineRenderer lineRenderer;

        private Vector3 aimPosition;
        public Vector3 AimPosition { get => aimPosition; }
        public float AngleToTarget { get => Vector2.SignedAngle(Vector2.up, aimPosition - StartingLineTransform.position); }
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
                aimPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                aimPosition.z = 0;
                // Vector2 mouseMovement = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
                // aimPosition += (Vector3)mouseMovement * rotationSpeed * Time.deltaTime;

                //process the aimPosition to be within the maxDistance
                Vector2 direction = (aimPosition - StartingLineTransform.position).normalized;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

                //limit the targetTransform position to the maxDistance
                if(Vector2.Distance(StartingLineTransform.position, aimPosition) > maxDistance)
                {
                    aimPosition = StartingLineTransform.position + (Vector3)direction * maxDistance;
                }
                aimPosition.z = 0;
                targetTransform.position = aimPosition;


                //Check first whether the mouse is hovering on a Targetable layer
                float distance = Vector2.Distance(StartingLineTransform.position, aimPosition);
                RaycastHit2D hit = Physics2D.Raycast(StartingLineTransform.position, direction, distance, targetableAimLayerMask | collisionAimLayerMask);

                //Check the hit collider is not null and the hit layer is on the targetable layer
                if(hit.collider != null && targetableAimLayerMask == (targetableAimLayerMask | (1 << hit.collider.gameObject.layer)))
                {
                    aimPosition = hit.collider.transform.position;
                    UpdateLine(hit.collider.transform.position);
                }
                //Else if it hit a collider that is on collision layer
                else if(hit.collider != null && collisionAimLayerMask == (collisionAimLayerMask | (1 << hit.collider.gameObject.layer)))
                {
                    aimPosition = hit.point;
                    UpdateLine(hit.point);
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
            lineRenderer.SetPosition(0, StartingLineTransform.position);
            lineRenderer.SetPosition(1, target);
        }

        /// <summary>
        /// Draws a line from the player to the mouse position
        /// </summary>
        public void DrawLine()
        {
            aimPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            aimPosition.z = 0;
            targetTransform.position = aimPosition;
            
            lineRenderer.enabled = true;
            isAiming = true;
        }

        public void RemoveLine()
        {
            targetTransform.localPosition = Vector3.zero;

            lineRenderer.enabled = false;
            isAiming = false;
            lineRenderer.SetPosition(0, Vector3.zero);
            lineRenderer.SetPosition(1, Vector3.zero);
        }
        #endregion
    }
}
