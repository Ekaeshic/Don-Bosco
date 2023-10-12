using UnityEngine;
using TMPro;

namespace DonBosco.Audio
{
    public class AudioOptionsManager : MonoBehaviour
    {
        public static float musicVolume { get; private set; }
        public static float soundEffectsVolume { get; private set; }
        public static float dialogueVolume { get; private set; }

        [Header("Texts")]
        [SerializeField] private TextMeshProUGUI musicSliderText;
        [SerializeField] private TextMeshProUGUI soundEffectsSliderText;
        [SerializeField] private TextMeshProUGUI dialogueSliderText;
        [Header("Sliders")]
        [SerializeField] private UnityEngine.UI.Slider musicSlider;
        [SerializeField] private UnityEngine.UI.Slider soundEffectsSlider;
        [SerializeField] private UnityEngine.UI.Slider dialogueSlider;


        void OnEnable()
        {
            musicVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);
            soundEffectsVolume = PlayerPrefs.GetFloat("SoundEffectsVolume", 1f);
            dialogueVolume = PlayerPrefs.GetFloat("DialogueVolume", 1f);

            OnMusicSliderValueChange(musicVolume);
            OnSoundEffectsSliderValueChange(soundEffectsVolume);
            OnDialogueSliderValueChange(dialogueVolume);

            musicSlider.value = musicVolume;
            soundEffectsSlider.value = soundEffectsVolume;
            dialogueSlider.value = dialogueVolume;
        }

        void Start()
        {
            AudioManager.Instance.UpdateMixerVolume();
        }


        void OnDisable()
        {
            PlayerPrefs.SetFloat("MusicVolume", musicVolume);
            PlayerPrefs.SetFloat("SoundEffectsVolume", soundEffectsVolume);
            PlayerPrefs.SetFloat("DialogueVolume", dialogueVolume);
        }

        public void OnMusicSliderValueChange(float value)
        {
            musicVolume = value;
            
            musicSliderText.text = ((int)(value * 100)).ToString();
            AudioManager.Instance.UpdateMixerVolume();
        }

        public void OnSoundEffectsSliderValueChange(float value)
        {
            soundEffectsVolume = value;

            soundEffectsSliderText.text = ((int)(value * 100)).ToString();
            AudioManager.Instance.UpdateMixerVolume();
        }

        public void OnDialogueSliderValueChange(float value)
        {
            dialogueVolume = value;

            dialogueSliderText.text = ((int)(value * 100)).ToString();
            AudioManager.Instance.UpdateMixerVolume();
        }
    }
}