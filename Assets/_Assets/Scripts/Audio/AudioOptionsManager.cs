using UnityEngine;
using TMPro;

namespace DonBosco.Audio
{
    public class AudioOptionsManager : MonoBehaviour
    {

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
            OnMusicSliderValueChange(AudioManager.musicVolume);
            OnSoundEffectsSliderValueChange(AudioManager.soundEffectsVolume);
            OnDialogueSliderValueChange(AudioManager.dialogueVolume);

            musicSlider.value = AudioManager.musicVolume;
            soundEffectsSlider.value = AudioManager.soundEffectsVolume;
            dialogueSlider.value = AudioManager.dialogueVolume;
        }

        void Start()
        {
            AudioManager.Instance.UpdateMixerVolume();
        }


        public void RefreshSliders()
        {
            musicSlider.value = AudioManager.musicVolume;
            soundEffectsSlider.value = AudioManager.soundEffectsVolume;
            dialogueSlider.value = AudioManager.dialogueVolume;

            musicSliderText.text = ((int)(AudioManager.musicVolume * 100)).ToString();
            soundEffectsSliderText.text = ((int)(AudioManager.soundEffectsVolume * 100)).ToString();
            dialogueSliderText.text = ((int)(AudioManager.dialogueVolume * 100)).ToString();
        }

        public void OnMusicSliderValueChange(float value)
        {
            AudioManager.musicVolume = value;
            
            musicSliderText.text = ((int)(value * 100)).ToString();
            AudioManager.Instance.UpdateMixerVolume();
        }

        public void OnSoundEffectsSliderValueChange(float value)
        {
            AudioManager.soundEffectsVolume = value;

            soundEffectsSliderText.text = ((int)(value * 100)).ToString();
            AudioManager.Instance.UpdateMixerVolume();
        }

        public void OnDialogueSliderValueChange(float value)
        {
            AudioManager.dialogueVolume = value;

            dialogueSliderText.text = ((int)(value * 100)).ToString();
            AudioManager.Instance.UpdateMixerVolume();
        }
    }
}