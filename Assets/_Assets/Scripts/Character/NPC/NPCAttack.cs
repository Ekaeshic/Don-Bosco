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
        [Tooltip("The layer that the NPC will attack")]
        [SerializeField] private LayerMask attackLayer = ~0; 
        [Tooltip("The layer that the NPC will check if the target is clear")]
        [SerializeField] private LayerMask rayLayer = ~0;
        [SerializeField] private Transform firePoint = null;
        [SerializeField] private float adjacentAlliesRange = 0.5f;
        [Header("Settings")]
        [SerializeField] private float scanRange = 1f;
        [SerializeField] private float fireRange = 2f;
        [Tooltip("The range that the NPC will step back to if the target is too close")]
        [SerializeField] private float unpreferedRange = 0.5f;
        [SerializeField] private float aimDelay = 1f;

        private NavMeshAgent agent;
        private float aimTimer = 0f;
        private bool isAlert = false;
        private bool isEngaging = false;
        private bool isAiming = false;
        private Vector3 bulletSpawnPos;
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
            bulletSpawnPos = firePoint.position;
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


        public void GetAttacked(GameObject source)
        {
            isAlert = true;
            //Check if the source is within scan range
            float distance = Vector2.Distance(bulletSpawnPos, source.transform.position);
            if(distance <= scanRange)
            {
                target = source.transform;
                isEngaging = true;
            }
        }

        public void SetAlert(bool alert)
        {
            isAlert = alert;
        }

        private void Attack()
        {
            if(!isEngaging)
            {
                GetComponent<NPCNavMovement>()?.ReleaseControl();
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
            Vector2 direction = target.position - bulletSpawnPos;
            float angle = Vector2.SignedAngle(Vector2.up, direction);
            GetComponent<NPCNavMovement>()?.ForceFaceIdle(direction.normalized);
            
            aimTimer += Time.deltaTime;
            if(aimTimer >= aimDelay)
            {
                //Check if the target is within fire range
                float distance = Vector2.Distance(bulletSpawnPos, target.position);
                if(distance <= fireRange)
                {
                    GetComponent<CharacterAttack>().Attack(angle, bulletSpawnPos);
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

            float distance = Vector2.Distance(bulletSpawnPos, target.position);
            bool isClear = IsRayToTargetClear();
            Transform adjacentAlly = IsNoAdjacentAllies(adjacentAlliesRange);
            if(isClear && distance <= fireRange)
            {
                if(!IsPreferredRange())
                {
                    StepBackFrom(target.position, 1f);
                }
                else
                {
                    isAiming = true;
                    aimTimer = 0f;
                }
            }
            else if(adjacentAlly != null)
            {
                StepBackFrom(adjacentAlly.position, 0.6f);
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

        private void StepBackFrom(Vector3 objectPos, float distanceMultiplier)
        {
            Vector3 direction = bulletSpawnPos - objectPos;
            Vector2 destination = bulletSpawnPos + direction.normalized * unpreferedRange * distanceMultiplier;
            //Check if the destination is within the navmesh
            NavMeshHit hit;
            if(NavMesh.SamplePosition(destination, out hit, 1f, NavMesh.AllAreas))
            {
                agent.SetDestination(hit.position);
            }
            else
            {
                agent.SetDestination(destination);
            }
            agent.isStopped = false;
        }

        /// <summary>
        /// Find the nearest attackable target within the range
        /// </summary>
        /// <param name="range"></param>
        private void FindAttackable(float range)
        {
            int hits = Physics2D.OverlapCircleNonAlloc(bulletSpawnPos, range, hit, attackLayer);
            GameObject nearestObject = null;
            float nearestDistance = Mathf.Infinity;

            if(hits > 0)
            {
                for(int i = 0; i < hits; i++)
                {
                    float distance = Vector2.Distance(bulletSpawnPos, hit[i].transform.position);
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
            float distance = Vector2.Distance(bulletSpawnPos, target.position);
            Vector2 direction = target.position - bulletSpawnPos;
            //Check if the target is clear from obstacles and visible to aim
            int numHits = Physics2D.RaycastNonAlloc(bulletSpawnPos, direction, rayHit , distance, rayLayer);
            // int numHits = Physics2D.CircleCastNonAlloc(bulletSpawnPos, circleCastRadius, direction, rayHit, distance, rayLayer);
            for(int i = 0; i < numHits; i++)
            {
                //Check if the ray is hitting itself
                if(rayHit[i].transform == transform || attackLayer == (attackLayer | (1 << rayHit[i].transform.gameObject.layer)))
                {
                    continue;
                }

                if(rayHit[i].transform != target)
                {
                    Debug.Log($"{gameObject.name} is blocked by {rayHit[i].transform.name}");
                    System.Array.Clear(rayHit, 0, rayHit.Length);
                    return false;
                }
            }
            System.Array.Clear(rayHit, 0, rayHit.Length);
            return true;
        }

        private Transform IsNoAdjacentAllies(float range)
        {
            int hits = Physics2D.OverlapCircleNonAlloc(bulletSpawnPos, range, hit, rayLayer);
            if(hits > 0)
            {
                for(int i = 0; i < hits; i++)
                {
                    if(hit[i].transform == transform)
                    {
                        continue;
                    }
                    if(hit[i].transform.gameObject.layer == gameObject.layer)
                    {
                        return hit[i].transform;
                    }
                }
            }
            return null;
        }
        
        /// <summary>
        /// Check if the target is within the preferred range
        /// </summary>
        /// <returns></returns>
        private bool IsPreferredRange()
        {
            float distance = Vector2.Distance(bulletSpawnPos, target.position);
            if(distance <= unpreferedRange)
            {
                return false;
            }
            return true;
        
        }


        private void OnDrawGizmos() 
        {
            //Draw scan range
            Gizmos.color = Color.red;
            if(isEngaging)
            {
                Gizmos.DrawWireSphere(transform.position, fireRange);
            }
            else
            {
                Gizmos.DrawWireSphere(transform.position, scanRange);
            }

            //Draw fire line circle cast
            if(target != null)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawLine(bulletSpawnPos, target.position);
                Gizmos.DrawWireSphere(bulletSpawnPos, adjacentAlliesRange);
            }

            //Draw path
            if(agent != null)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawLine(transform.position, agent.destination);
            }
        }
    }
}
