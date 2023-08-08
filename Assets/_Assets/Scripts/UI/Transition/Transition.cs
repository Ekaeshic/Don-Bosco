using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

namespace DonBosco
{
    public class Transition : MonoBehaviour
    {
        private static Transition instance;
        public static Transition Instance { get { return instance; } }
        [SerializeField] private CanvasGroup transitionScreen;

        [SerializeField] float duration = 0.5f;



        private void Awake() {
            instance = this;
        }

        public static void FadeIn(Action OnComplete = null)
        {
            instance.transitionScreen.alpha = 0;
            instance.transitionScreen.blocksRaycasts = true;
            instance.transitionScreen.DOFade(1, instance.duration).OnComplete(() => {
                OnComplete?.Invoke();
                instance.transitionScreen.alpha = 0;
            });
        }

        public static void FadeOut(Action OnComplete = null)
        {
            instance.transitionScreen.alpha = 1;
            instance.transitionScreen.DOFade(0, instance.duration).OnComplete(() => {
                OnComplete?.Invoke();
                instance.transitionScreen.blocksRaycasts = false;
            });
        }
    }

}