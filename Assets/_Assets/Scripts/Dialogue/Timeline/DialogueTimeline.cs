using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace DonBosco.Dialogue
{
    public class DialogueTimeline : MonoBehaviour
    {
        [SerializeField] private PlayableDirector currentPlayableDirector;
        [Header("Settings")]
        [SerializeField] private bool startTimelineOnStart = true;
        [SerializeField] private bool needPlayerAnimator = false;
        [SerializeField] private bool needTransition = false;

        [SerializeField] private UnityEvent onDialogueEnded;


        private void OnEnable() {
            currentPlayableDirector.stopped += (director) => {
                onDialogueEnded?.Invoke();
            };
        }

        private void OnDisable() {
            currentPlayableDirector.stopped -= (director) => {
                onDialogueEnded?.Invoke();
            };
        }

        private void Start() {
            if(startTimelineOnStart)
            {
                StartTimeLine();
            }
        }
    

        /// <summary>
        /// Start a dialogue and track the director to let it know when the dialogue is finished and to pause the timeline.
        /// </summary>
        public void StartDialogue(TextAsset dialogue, string knotPath = null)
        {
            DialogueManager.Instance.OnDialogueEnded += OnDialogueEnded;
            InputManager.Instance.SetUIActionMap(true);
            DialogueManager.Instance.EnterDialogueModeFromTimeline(dialogue, knotPath);
            PauseTimeline();
        }


        private void OnDialogueEnded()
        {
            DialogueManager.Instance.OnDialogueEnded -= OnDialogueEnded;
            ResumeTimeline();
            InputManager.Instance.SetUIActionMap(false);
        }


        public void StartTimeLine()
        {
            if(needTransition)
            {
                Transition.FadeOut(Process);
            }
            else
            {
                Process(); // Immediately start the timeline
            }


            void Process()
            {
                if(needPlayerAnimator)
                {
                    TimelineManager.Instance.GetPlayerAnimator(out Animator movementAnimator, out Animator spriteAnimator);
                    TimelineAsset timelineAsset = currentPlayableDirector.playableAsset as TimelineAsset;
                    foreach(var track in timelineAsset.GetRootTracks())
                    {
                        // Search for the player track group
                        if(track.name == "Player")
                        {
                            // Search for the player movement track
                            foreach(var t in (track as TrackAsset).GetChildTracks())
                            {
                                if(t.name == "Movement")
                                {
                                    // Set the player movement animator
                                    currentPlayableDirector.SetGenericBinding(t, movementAnimator);
                                }
                                else if(t.name == "Sprite")
                                {
                                    // Set the player sprite animator
                                    currentPlayableDirector.SetGenericBinding(t, spriteAnimator);
                                }
                            }
                            break;
                        }
                    }
                }
                GameManager.SetCutsceneState();
                currentPlayableDirector.Play();
            }
        }

        public void StopTimeline()
        {
            GameManager.ResumeGame();
            currentPlayableDirector.Stop();
        }


        public void PauseTimeline()
        {
            currentPlayableDirector.Pause();
        }

        public void ResumeTimeline()
        {
            currentPlayableDirector.Resume();
        }
    }
}