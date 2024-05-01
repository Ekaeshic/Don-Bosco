using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace DonBosco
{
    [RequireComponent(typeof(NavMeshAgent), typeof(CharacterAnimator))]
    public class NPCNavMovement : MonoBehaviour
    {
        NavMeshAgent agent;
        CharacterAnimator animator;

        private const string IDLE = "Idle";
        private const string SIDE = "side";
        private const string UP = "up";
        private const string DOWN = "down";

        private bool isControlledExternally = false;

        [SerializeField] private StartingDirection startingDirection = StartingDirection.Down;
        //[SerializeField] private Transform followOnStart = null;
        [SerializeField] private bool followPlayerOnStart = false;
        private enum StartingDirection
        {
            Up,
            Down,
            Left,
            Right
        }

        private void Awake() 
        {
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponent<CharacterAnimator>();
            string state = "";
            switch(startingDirection)
            {
                case StartingDirection.Up:
                    state = UP;
                    break;
                case StartingDirection.Down:
                    state = DOWN;
                    break;
                case StartingDirection.Left:
                    state = SIDE;
                    transform.localScale = new Vector3(1, 1, 1);
                    break;
                case StartingDirection.Right:
                    state = SIDE;
                    transform.localScale = new Vector3(-1, 1, 1);
                    break;
            }
            animator.ChangeState(state+IDLE);
        }

        void Start()
        {
            // if(followOnStart != null)
            // {
            //     float distanceStopFollow = 0.5f;
            //     Vector3 followPosition = followOnStart.position + (transform.position - followOnStart.position).normalized * distanceStopFollow;
            //     agent.SetDestination(followPosition);
            // }
            if(followPlayerOnStart)
            {
                TimelineManager.Instance.GetPlayerAnimator(out Animator animator, out Animator sprite);
                Vector3 playerPosition = animator.transform.position;
                
                agent.SetDestination(playerPosition);
            }
        }


        void Update()
        {
            if(!isControlledExternally) 
            {
                AnimateMovement();
            }
        }



        private void AnimateMovement()
        {
            bool isMoving = agent.velocity.magnitude > 0.1f;
            Vector2 direction = agent.velocity.normalized;
            string state = "";

            // Y Axis
            if(direction.y > 0.5f)
            {
                state = UP;
            }
            else if(direction.y < -0.5f)
            {
                state = DOWN;
            }

            // X Axis
            else if(direction.x > 0.5f)
            {
                state = SIDE;
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else if(direction.x < -0.5f)
            {
                state = SIDE;
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if(!isMoving && !string.IsNullOrEmpty(state))
            {
                state += IDLE;
            }
            animator.ChangeState(state);
        }



        public void ForceFaceIdle(Vector2 direction)
        {
            isControlledExternally = true;
            string state = "";

            // Y Axis
            if(direction.y > 0.5f)
            {
                state = UP;
                animator.ChangeState(state+IDLE);
            }
            else if(direction.y < -0.5f)
            {
                state = DOWN;
                animator.ChangeState(state+IDLE);
            }

            // X Axis
            else if(direction.x > 0.5f)
            {
                state = SIDE;
                animator.ChangeState(state+IDLE);
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else if(direction.x < -0.5f)
            {
                state = SIDE;
                animator.ChangeState(state+IDLE);
                transform.localScale = new Vector3(1, 1, 1);
            }
        }

        
        public void ReleaseControl()
        {
            isControlledExternally = false;
        }
    }
}
