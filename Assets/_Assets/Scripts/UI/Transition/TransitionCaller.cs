using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DonBosco
{
    public class TransitionCaller : MonoBehaviour
    {
        public UnityEvent OnTransitionFadeOut;
        public UnityEvent OnTransitionFadeIn;


        public void TransitionFadeOut()
        {
            Transition.FadeOut(() => {
                OnTransitionFadeOut?.Invoke();
            });
        }

        public void TransitionFadeIn()
        {
            Transition.FadeOut(() => {
                OnTransitionFadeIn?.Invoke();
            });
        } 


        public void ShowLoadingScreen()
        {
            LoadingScreen.ShowLoadingScreen();
        }

        public void HideLoadingScreen()
        {
            LoadingScreen.HideLoadingScreen();
        }
    }
}
