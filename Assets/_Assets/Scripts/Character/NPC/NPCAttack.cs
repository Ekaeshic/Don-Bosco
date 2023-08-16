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
        [Header("Settings")]
        [SerializeField] private float scanRange = 1f;
        [SerializeField] private float fireRange = 2f;
        [SerializeField] private float aimDelay = 1f;

        private NavMeshAgent agent;
        private float aimTimer = 0f;
        private bool isAlert = false;
        private bool isEngaging = false;
        private bool isAiming = false;
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
            if(isAlert)
            {
                Attack();
            }
        }
        #endregion



        public void SetAlert(bool alert)
        {
            isAlert = alert;
        }

        private void Attack()
        {
            if(!isEngaging)
            {
                FindAttackable();

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
            agent.isStopped = true;
            //Rotate towards the target
            Vector2 direction = target.position - transform.position;
            float angle = Vector2.SignedAngle(Vector2.up, direction);
            
            aimTimer += Time.deltaTime;
            if(aimTimer >= aimDelay)
            {
                //Check if the target is within fire range
                float distance = Vector2.Distance(transform.position, target.position);
                if(distance <= fireRange)
                {
                    GetComponent<CharacterAttack>().Attack(angle, transform.position);
                }
                else
                {
                    isAiming = false;
                    isEngaging = false;
                    aimTimer = 0f;
                }
            }
        }

        private void Engage()
        {
            float distance = Vector2.Distance(transform.position, target.position);
            if(distance >= fireRange)
            {
                FollowTarget();
                return;
            }
            //Check if the target is clear from obstacles and visible to aim
            int numHits = Physics2D.RaycastNonAlloc(transform.position, target.position, rayHit ,distance, rayLayer);
            
            for(int i = 0; i < numHits; i++)
            {
                if(rayHit[i].transform != target)
                {
                    FollowTarget();
                    return;
                }
            }
            isAiming = true;
        }

        private void FollowTarget()
        {
            isAiming = false;
            agent.SetDestination(target.position);
            agent.isStopped = false;
        }

        private void FindAttackable()
        {
            int numHits = Physics2D.OverlapCircleNonAlloc(transform.position, scanRange, hit, attackLayer);
            GameObject nearestObject = null;
            float nearestDistance = Mathf.Infinity;

            if(numHits > 0)
            {
                for(int i = 0; i < numHits; i++)
                {
                    float distance = Vector2.Distance(transform.position, hit[i].transform.position);
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
