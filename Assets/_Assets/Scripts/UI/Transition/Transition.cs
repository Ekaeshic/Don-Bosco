using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using System.Threading.Tasks;

namespace DonBosco
{
    public class Transition : MonoBehaviour
    {
        private static Transition instance;
        public static Transition Instance { get { return instance; } }
        [SerializeField] private CanvasGroup transitionScreen;

        [SerializeField] float duration = 0.5f;
        //[SerializeField] bool isFaded = true;
        public static bool IsAnimating = false;



        private void Awake() {
            instance = this;
        }

        public static async Task FadeOut(Action OnComplete = null)
        {
            if(instance.transitionScreen.alpha == 1)
            {
                OnComplete?.Invoke();
                return;
            }
            IsAnimating = true;
            instance.transitionScreen.blocksRaycasts = true;
            instance.transitionScreen.DOFade(1, instance.duration).OnComplete(() => {
                OnComplete?.Invoke();
                IsAnimating = false;
                // instance.isFaded = true;
            });
            await Task.CompletedTask;
        }

        public static async Task FadeIn(Action OnComplete = null)
        {
            if(instance.transitionScreen.alpha == 0)
            {
                OnComplete?.Invoke();
                return;
            }
            IsAnimating = true;
            instance.transitionScreen.DOFade(0, instance.duration).OnComplete(() => {
                OnComplete?.Invoke();
                instance.transitionScreen.blocksRaycasts = false;
                IsAnimating = false;
                // instance.isFaded = false;
            });
            await Task.CompletedTask;
        }
    }

}