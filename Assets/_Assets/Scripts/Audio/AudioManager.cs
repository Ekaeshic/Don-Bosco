using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

namespace DonBosco.Audio
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;

        [SerializeField] private AudioMixerGroup musicMixerGroup;
        [SerializeField] private AudioMixerGroup soundEffectsMixerGroup;
        [SerializeField] private AudioMixerGroup dialogueMixerGroup;
        [SerializeField] private Sound[] sounds;
        [SerializeField] private float fadeDuration = 1f;

        private void Awake()
        {
            Instance = this;

            foreach (Sound s in sounds)
            {
                s.source = gameObject.AddComponent<AudioSource>();
                s.source.clip = s.audioClip;
                s.source.loop = s.isLoop;
                s.source.volume = s.volume;

                switch (s.audioType)
                {
                    case Sound.AudioTypes.soundEffect:
                        s.source.outputAudioMixerGroup = soundEffectsMixerGroup;
                        break;

                    case Sound.AudioTypes.music:
                        s.source.outputAudioMixerGroup = musicMixerGroup;
                        break;
                }

                if (s.playOnAwake)
                    s.source.Play();
            }
        }

        public void Play(string clipname)
        {
            Sound s = Array.Find(sounds, dummySound => dummySound.clipName == clipname);
            if (s == null)
            {
                Debug.LogError("Sound: " + clipname + " does NOT exist!");
                return;
            }
            StartCoroutine(FadeIn(s.source, fadeDuration));
        }

        private IEnumerator FadeIn(AudioSource audioSource, float fadeDuration)
        {
            float startVolume = 0.1f;

            audioSource.volume = 0;
            audioSource.Play();

            while (audioSource.volume < startVolume)
            {
                audioSource.volume += startVolume * Time.deltaTime / fadeDuration;
                yield return null;
            }

            audioSource.volume = startVolume;
        }

        public void Stop(string clipname)
        {
            Sound s = Array.Find(sounds, dummySound => dummySound.clipName == clipname);
            if (s == null)
            {
                Debug.LogError("Sound: " + clipname + " does NOT exist!");
                return;
            }

            StartCoroutine(FadeOut(s.source, fadeDuration));
        }

        private IEnumerator FadeOut(AudioSource audioSource, float fadeDuration)
        {
            float startVolume = audioSource.volume;

            while (audioSource.volume > 0)
            {
                audioSource.volume -= startVolume * Time.deltaTime / fadeDuration;
                yield return null;
            }

            audioSource.Stop();
            audioSource.volume = startVolume;
        }

        public void UpdateMixerVolume()
        {
            musicMixerGroup.audioMixer.SetFloat("Music Volume", Mathf.Log10(AudioOptionsManager.musicVolume) * 40);
            soundEffectsMixerGroup.audioMixer.SetFloat("Sound Effects Volume", Mathf.Log10(AudioOptionsManager.soundEffectsVolume) * 40);
            dialogueMixerGroup.audioMixer.SetFloat("Dialogue Volume", Mathf.Log10(AudioOptionsManager.dialogueVolume) * 40);
        }
    }    
}