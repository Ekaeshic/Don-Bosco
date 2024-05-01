using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DonBosco.Audio
{
    public class AudioPlayer : MonoBehaviour
    {
        [SerializeField] private AudioClip clipName;
        [SerializeField] private bool playOnAwake;
        [SerializeField] private bool stopOnDisable;


        private void OnEnable()
        {
            if (playOnAwake)
                Play();
        }

        private void OnDisable()
        {
            if (stopOnDisable)
                Stop();
        }

        public void Play()
        {
            AudioManager.Instance.Play(clipName.name);
        }

        public void Stop()
        {
            AudioManager.Instance.Stop(clipName.name);
        }
    }
}
