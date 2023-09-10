using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DonBosco
{
    [RequireComponent(typeof(Animator))]
    public class CharacterAnimator : MonoBehaviour
    {
        private Animator animator;
        private string currentState;


        public void ChangeState(string state)
        {
            if(animator == null) animator = GetComponent<Animator>();
            if(currentState == state) return;
            animator.Play(state);
            currentState = state;
        }
    }

}