using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace DonBosco.Dialogue
{
    public class DialogueTimeline : MonoBehaviour
    {
        [SerializeField] private PlayableDirector currentPlayableDirector;
    

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
            InputManager.Instance.SetMovementActionMap(false);
            currentPlayableDirector.Play();
        }

        public void StopTimeline()
        {
            InputManager.Instance.SetMovementActionMap(true);
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