using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace DonBosco.Character
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class NPCAttack : MonoBehaviour
    {
        [SerializeField] private LayerMask attackLayer = ~0;
        [SerializeField] private LayerMask rayLayer = ~0;
        [SerializeField] private Transform firePoint = null;
        [Header("Settings")]
        [SerializeField] private float scanRange = 1f;
        [SerializeField] private float fireRange = 2f;
        [SerializeField] private float aimDelay = 1f;

        private NavMeshAgent agent;
        private float aimTimer = 0f;
        private bool isAlert = false;
        private bool isEngaging = false;
        private bool isAiming = false;
        private Vector3 startingPosition;
        private Collider2D[] hit = new Collider2D[10]; //You can change the size of this array if you need to
        private RaycastHit2D[] rayHit = new RaycastHit2D[10];
        private Transform target;


        #region MonoBehaviour
        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            agent.updateRotation = false;
            agent.updateUpAxis = false;
        }

        private void Update() 
        {
            startingPosition = firePoint.position;
            if(isAlert)
            {
                Attack();
            }
        }
        
        void OnDisable()
        {
            isAlert = false;
            isEngaging = false;
            isAiming = false;
            target = null;
            if(agent.isActiveAndEnabled)
            {
                agent.isStopped = false;
                agent.ResetPath();
            }
        }
        #endregion


        public void GetAttacked()
        {
            isAlert = true;
            FindAttackable(fireRange);
        }

        public void SetAlert(bool alert)
        {
            isAlert = alert;
        }

        private void Attack()
        {
            if(!isEngaging)
            {
                FindAttackable(scanRange);

                //Clear the overlap alloc (this is important)
                System.Array.Clear(hit, 0, hit.Length);
            }
            else
            {
                if(isAiming)
                {
                    Aim();
                }
                else
                {
                    Engage();
                }
            }
            
        }

        private void Aim()
        {
            if(target == null)
            {
                isAiming = false;
                isEngaging = false;
                return;
            }

            agent.isStopped = true;
            //Rotate towards the target
            Vector2 direction = target.position - startingPosition;
            float angle = Vector2.SignedAngle(Vector2.up, direction);
            GetComponent<NPCNavMovement>()?.ForceFaceIdle(direction.normalized);
            
            aimTimer += Time.deltaTime;
            if(aimTimer >= aimDelay)
            {
                //Check if the target is within fire range
                float distance = Vector2.Distance(startingPosition, target.position);
                if(distance <= fireRange)
                {
                    GetComponent<CharacterAttack>().Attack(angle, startingPosition);
                }
                aimTimer = 0f;
                isAiming = false;
                GetComponent<NPCNavMovement>()?.ReleaseControl();
            }
        }

        private void Engage()
        {
            if(target == null)
            {
                isEngaging = false;
                return;
            }

            float distance = Vector2.Distance(startingPosition, target.position);
            if(distance >= fireRange)
            {
                FollowTarget();
                return;
            }
            
            bool isClear = IsRayToTargetClear();
            if(isClear)
            {
                isAiming = true;
                aimTimer = 0f;
            }
            else
            {
                FollowTarget();
            }
        }

        private void FollowTarget()
        {
            isAiming = false;
            agent.SetDestination(target.position);
            agent.isStopped = false;
        }

        private void FindAttackable(float range)
        {
            int numHits = Physics2D.OverlapCircleNonAlloc(startingPosition, range, hit, attackLayer);
            GameObject nearestObject = null;
            float nearestDistance = Mathf.Infinity;

            if(numHits > 0)
            {
                for(int i = 0; i < numHits; i++)
                {
                    float distance = Vector2.Distance(startingPosition, hit[i].transform.position);
                    if(distance < nearestDistance)
                    {
                        nearestDistance = distance;
                        nearestObject = hit[i].gameObject;
                    }
                }
            }

            target = nearestObject != null ? nearestObject.transform : null;
            isEngaging = target != null;
        }

        private bool IsRayToTargetClear()
        {
            float distance = Vector2.Distance(startingPosition, target.position);
            Vector2 direction = target.position - startingPosition;
            //Check if the target is clear from obstacles and visible to aim
            int numHits = Physics2D.RaycastNonAlloc(startingPosition, direction, rayHit , distance, rayLayer);
            for(int i = 0; i < numHits; i++)
            {
                if(rayHit[i].transform != target)
                {
                    System.Array.Clear(rayHit, 0, rayHit.Length);
                    return false;
                }
            }
            System.Array.Clear(rayHit, 0, rayHit.Length);
            return true;
        }


        private void OnDrawGizmos() 
        {
            Gizmos.color = Color.red;
            if(isEngaging)
            {
                Gizmos.DrawWireSphere(transform.position, fireRange);
            }
            else
            {
                Gizmos.DrawWireSphere(transform.position, scanRange);
            }
        }
    }
}
