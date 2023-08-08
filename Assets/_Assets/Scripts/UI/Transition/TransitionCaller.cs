using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DonBosco
{
    public class TransitionCaller : MonoBehaviour
    {
        public UnityEvent OnTransitionIn;
        public UnityEvent OnTransitionOut;


        public void TransitionIn()
        {
            Transition.FadeIn(() => {
                OnTransitionIn?.Invoke();
            });
        }

        public void TransitionOut()
        {
            Transition.FadeOut(() => {
                OnTransitionOut?.Invoke();
            });
        } 
    }
}
