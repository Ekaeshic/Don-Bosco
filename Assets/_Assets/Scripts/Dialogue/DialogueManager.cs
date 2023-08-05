using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.EventSystems;
using System;

namespace DonBosco.Dialogue
{
    /// <summary>
    /// Handles the display of dialogue
    /// </summary>
    /// NOTE: SCRIPT INI HASIL MODIFIKASI SCRIPT DARI Sample\Ink\YTSample\Scripts\Dialogue\DialogueManager.cs,
    /// Kalau mau comot, comot script yg ini aja tapi script2 yang lain pada folder Dialogue juga di comot
    public class DialogueManager : MonoBehaviour
    {
        [Header("Params")]
        [SerializeField] private float typingSpeed = 0.04f;

        [Header("Load Globals JSON")]
        // For load and save variables on ink
        [SerializeField] private TextAsset loadGlobalsJSON;

        [Header("Dialogue UI")]
        [SerializeField] private GameObject dialoguePanel;
        [SerializeField] private GameObject continueIcon;
        [SerializeField] private TextMeshProUGUI dialogueText;
        [SerializeField] private TextMeshProUGUI displayNameText;
        [SerializeField] private Animator portraitAnimator;
        private Animator layoutAnimator;

        [Header("Choices UI")]
        [SerializeField] private GameObject[] choices;
        private TextMeshProUGUI[] choicesText;

        [Header("Audio")]
        [SerializeField] private DialogueAudioInfoSO defaultAudioInfo;
        [SerializeField] private DialogueAudioInfoSO[] audioInfos;
        private DialogueAudioInfoSO currentAudioInfo;
        private Dictionary<string, DialogueAudioInfoSO> audioInfoDictionary;
        private AudioSource audioSource;

        private Story currentStory;
        public bool dialogueIsPlaying { get; private set; }

        private bool canContinueToNextLine = false;

        private bool playAudioNextLine = false;
        private bool hasChosenChoice = false;

        private Coroutine displayLineCoroutine;

        private static DialogueManager instance;

        private const string SPEAKER_TAG = "speaker";
        private const string PORTRAIT_TAG = "portrait";
        private const string LAYOUT_TAG = "layout";
        private const string AUDIO_TAG = "audio";

        private DialogueVariables dialogueVariables;
        private InkExternalFunctions inkExternalFunctions;

        public event Action OnDialogueStarted;
        public event Action OnDialogueLineDisplay;
        public event Action OnDialogueEnded;

        private void Awake() 
        {
            if (instance != null)
            {
                Debug.LogWarning("Found more than one Dialogue Manager in the scene");
            }
            instance = this;

            dialogueVariables = new DialogueVariables(loadGlobalsJSON);
            inkExternalFunctions = new InkExternalFunctions();

            audioSource = this.gameObject.AddComponent<AudioSource>();
            currentAudioInfo = defaultAudioInfo;
        }

        public static DialogueManager GetInstance() 
        {
            return instance;
        }

        private void Start() 
        {
            dialogueIsPlaying = false;
            dialoguePanel.SetActive(false);

            // get the layout animator
            layoutAnimator = dialoguePanel.GetComponent<Animator>();

            // get all of the choices text 
            choicesText = new TextMeshProUGUI[choices.Length];
            int index = 0;
            foreach (GameObject choice in choices) 
            {
                choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
                index++;
            }

            InitializeAudioInfoDictionary();
        }

        private void InitializeAudioInfoDictionary() 
        {
            audioInfoDictionary = new Dictionary<string, DialogueAudioInfoSO>();
            audioInfoDictionary.Add(defaultAudioInfo.id, defaultAudioInfo);
            foreach (DialogueAudioInfoSO audioInfo in audioInfos) 
            {
                audioInfoDictionary.Add(audioInfo.id, audioInfo);
            }
        }

        private void SetCurrentAudioInfo(string id) 
        {
            DialogueAudioInfoSO audioInfo = null;
            audioInfoDictionary.TryGetValue(id, out audioInfo);
            if (audioInfo != null) 
            {
                this.currentAudioInfo = audioInfo;
            }
            else 
            {
                Debug.LogWarning("Failed to find audio info for id: " + id);
            }
        }

        private void Update() 
        {
            // return right away if dialogue isn't playing
            if (!dialogueIsPlaying) 
            {
                return;
            }

            // handle continuing to the next line in the dialogue when submit is pressed
            // NOTE: The 'currentStory.currentChoiecs.Count == 0' part was to fix a bug after the Youtube video was made
            if (canContinueToNextLine 
                && currentStory.currentChoices.Count == 0 
                && Character.InputManager.Instance.GetSubmitPressed())
            {
                ContinueStory();
            }
        }

        public void EnterDialogueMode(TextAsset inkJSON, Animator emoteAnimator = null) 
        {
            if(dialogueIsPlaying || inkJSON == null)
            {
                Debug.LogWarning("Dialogue is already playing or inkJSON is null");
                return;
            }
            // Prepare for dialogue (Punyaku, bisa dihapus kalau gak pake)
            PrepareForDialogue(true);
            
            currentStory = new Story(inkJSON.text);
            dialogueIsPlaying = true;
            dialoguePanel.SetActive(true);

            dialogueVariables.StartListening(currentStory);
            if(emoteAnimator != null)
            {
                inkExternalFunctions.Bind(currentStory, emoteAnimator);
            }

            // reset portrait, layout, and speaker as default
            displayNameText.text = "???";
            //portraitAnimator.Play("default");
            portraitAnimator.transform.parent.gameObject.SetActive(true);

            layoutAnimator.Play("default");
            
            OnDialogueStarted?.Invoke();

            ContinueStory();
        }

        private IEnumerator ExitDialogueMode() 
        {
            yield return new WaitForSecondsRealtime(0.2f);

            dialogueVariables.StopListening(currentStory);
            inkExternalFunctions.Unbind(currentStory);

            dialogueIsPlaying = false;
            dialoguePanel.SetActive(false);
            dialogueText.text = "";

            OnDialogueEnded?.Invoke();

            // go back to default audio
            SetCurrentAudioInfo(defaultAudioInfo.id);

            // Reverse the changes made in EnterDialogueMode
            PrepareForDialogue(false);
        }

        private void ContinueStory() 
        {
            if (currentStory.canContinue) 
            {
                // set text for the current dialogue line
                if (displayLineCoroutine != null) 
                {
                    StopCoroutine(displayLineCoroutine);
                }
                string nextLine = currentStory.Continue();
                // handle case where the last line is an external function
                if (nextLine.Equals("") && !currentStory.canContinue)
                {
                    StartCoroutine(ExitDialogueMode());
                }
                // otherwise, handle the normal case for continuing the story
                else 
                {
                    // handle tags
                    HandleTags(currentStory.currentTags);
                    displayLineCoroutine = StartCoroutine(DisplayLine(nextLine));
                }
            }
            else 
            {
                StartCoroutine(ExitDialogueMode());
            }
        }

        private IEnumerator DisplayLine(string line) 
        {
            // set the text to the full line, but set the visible characters to 0
            dialogueText.text = line;
            OnDialogueLineDisplay?.Invoke();
            dialogueText.maxVisibleCharacters = 0;
            // hide items while text is typing
            continueIcon.SetActive(false);
            HideChoices();

            canContinueToNextLine = false;

            bool isAddingRichTextTag = false;

            // display each letter one at a time
            foreach (char letter in line.ToCharArray())
            {
                // if the submit button is pressed, finish up displaying the line right away
                if (Character.InputManager.Instance.GetSubmitPressed()) 
                {
                    if(hasChosenChoice)
                    {
                        hasChosenChoice = false;
                        continue;
                    }
                    dialogueText.maxVisibleCharacters = line.Length;
                    break;
                }

                // check for rich text tag, if found, add it without waiting
                if (letter == '<' || isAddingRichTextTag) 
                {
                    isAddingRichTextTag = true;
                    if (letter == '>')
                    {
                        isAddingRichTextTag = false;
                    }
                }
                // if not rich text, add the next letter and wait a small time
                else 
                {
                    PlayDialogueSound(dialogueText.maxVisibleCharacters, dialogueText.text[dialogueText.maxVisibleCharacters]);
                    dialogueText.maxVisibleCharacters++;
                    yield return new WaitForSecondsRealtime(typingSpeed);
                }
            }

            // actions to take after the entire line has finished displaying
            continueIcon.SetActive(true);
            DisplayChoices();

            canContinueToNextLine = true;
        }

        private void PlayDialogueSound(int currentDisplayedCharacterCount, char currentCharacter)
        {
            if(!playAudioNextLine)
            {
                return;
            }
            //filter out the character that is not a letter or number
            if(!Char.IsLetterOrDigit(currentCharacter) && !currentAudioInfo.readPunctuation)
            {
                return;
            }
            if(currentAudioInfo.readWaitForAudioBefore && audioSource.isPlaying)
            {
                return;
            }

            // set variables for the below based on our config
            AudioClip[] dialogueTypingSoundClips = null;
            if(currentAudioInfo.dialogueAudioType == DialogueAudioType.SimpleTyping)
            {
                dialogueTypingSoundClips = currentAudioInfo.dialogueTypingSoundClips;
            }
            else if(currentAudioInfo.dialogueAudioType == DialogueAudioType.AlphabetTyping)
            {
                dialogueTypingSoundClips = currentAudioInfo.dialogueAlphabetSoundClips;
            }
            
            int frequencyLevel = currentAudioInfo.frequencyLevel;
            float minPitch = currentAudioInfo.minPitch;
            float maxPitch = currentAudioInfo.maxPitch;
            bool stopAudioSource = currentAudioInfo.stopAudioSourceInstantly;
            int hashCode = currentCharacter.GetHashCode();

            // play the sound based on the config
            if (currentDisplayedCharacterCount % frequencyLevel == 0)
            {
                if (stopAudioSource) 
                {
                    audioSource.Stop();
                }
                AudioClip soundClip = null;
                
                // create predictable audio from hashing
                if (currentAudioInfo.hashcodeIndexing && currentAudioInfo.dialogueAudioType == DialogueAudioType.SimpleTyping) 
                {
                    // sound clip
                    int predictableIndex = hashCode % dialogueTypingSoundClips.Length;
                    soundClip = dialogueTypingSoundClips[predictableIndex];
                }
                else if(currentAudioInfo.dialogueAudioType == DialogueAudioType.AlphabetTyping)
                {
                    // sound clip
                    // convert alphabet order (from a-z) to index (from 0-25)
                    int index = char.ToUpper(currentCharacter) - 65;
                    soundClip = dialogueTypingSoundClips[index];
                }
                // otherwise, randomize the audio
                else 
                {
                    // sound clip
                    int randomIndex = UnityEngine.Random.Range(0, dialogueTypingSoundClips.Length);
                    soundClip = dialogueTypingSoundClips[randomIndex];
                }

                // pitch
                switch(currentAudioInfo.predictPitch)
                {
                    case DialoguePitchType.Predictable:
                        int minPitchInt = (int) (minPitch * 100);
                        int maxPitchInt = (int) (maxPitch * 100);
                        int pitchRangeInt = maxPitchInt - minPitchInt;
                        // cannot divide by 0, so if there is no range then skip the selection
                        if (pitchRangeInt != 0) 
                        {
                            int predictablePitchInt = (hashCode % pitchRangeInt) + minPitchInt;
                            float predictablePitch = predictablePitchInt / 100f;
                            audioSource.pitch = predictablePitch;
                        }
                        else 
                        {
                            audioSource.pitch = minPitch;
                        }
                        break;
                    case DialoguePitchType.Random:
                        audioSource.pitch = UnityEngine.Random.Range(minPitch, maxPitch);
                        break;
                    case DialoguePitchType.Median:
                        audioSource.pitch = (minPitch + maxPitch) / 2f;
                        break;
                }
                
                // play sound
                audioSource.PlayOneShot(soundClip);
            }
        }

        private void HideChoices() 
        {
            foreach (GameObject choiceButton in choices) 
            {
                choiceButton.SetActive(false);
            }
        }

        // Modified
        private void HandleTags(List<string> currentTags)
        {
            // loop through each tag and handle it accordingly
            foreach (string tag in currentTags) 
            {
                string[]splitTag = tag.Split(" ");
                List<string> newSplitTag = new List<string>(splitTag.Length);

                for(int i = 0; i < splitTag.Length; i++)
                {
                    //Check if curreent subString is one of defined tag, if yes continue
                    if(splitTag[i].Contains(SPEAKER_TAG) || splitTag[i].Contains(PORTRAIT_TAG) || splitTag[i].Contains(LAYOUT_TAG) || splitTag[i].Contains(AUDIO_TAG))
                    {
                        newSplitTag.Add(splitTag[i]);
                        continue;
                    }
                    else
                    {
                        //Merge the tag with the previous sub split tag in the list
                        newSplitTag[newSplitTag.Count - 1] += " " + splitTag[i];
                    }
                }

                foreach(string remainingTag in newSplitTag)
                {
                    ProcessTag(remainingTag.Split(':'));
                }
                // parse the tag
                // string[] splitTag = tag.Split(':');
                // if(splitTag.Length == 2)
                // {
                //     ProcessTag(splitTag);
                // }
                // else if(tag.Contains(" "))
                // {
                //     foreach(string remainingTag in tag.Split(' '))
                //     {
                //         ProcessTag(remainingTag.Split(':'));
                //     }
                // }
            }

            void ProcessTag(string[] splitTag)
            {
                string tagKey = splitTag[0].Trim();
                string tagValue = splitTag[1].Trim();
                
                // handle the tag
                switch (tagKey) 
                {
                    case SPEAKER_TAG:
                        if(tagValue.Equals("null"))
                        {
                            displayNameText.transform.parent.gameObject.SetActive(false);
                        }
                        else
                        {
                            displayNameText.transform.parent.gameObject.SetActive(true);
                            displayNameText.text = tagValue;
                        }
                        break;
                    case PORTRAIT_TAG:
                        if(tagValue.Equals("null"))
                        {
                            portraitAnimator.transform.parent.gameObject.SetActive(false);
                        }
                        else
                        {
                            portraitAnimator.transform.parent.gameObject.SetActive(true);
                            portraitAnimator.Play(tagValue);
                        }
                        break;
                    case LAYOUT_TAG:
                        layoutAnimator.Play(tagValue);
                        break;
                    case AUDIO_TAG: 
                        if(tagValue.Equals("null"))
                        {
                            playAudioNextLine = false;
                        }
                        else
                        {
                            playAudioNextLine = true;
                            SetCurrentAudioInfo(tagValue);
                        }
                        break;
                    default:
                        Debug.LogWarning("Tag came in but is not currently being handled: " + tag);
                        break;
                }
            }
        }

        private void DisplayChoices() 
        {
            List<Choice> currentChoices = currentStory.currentChoices;

            // defensive check to make sure our UI can support the number of choices coming in
            if (currentChoices.Count > choices.Length)
            {
                Debug.LogError("More choices were given than the UI can support. Number of choices given: " 
                    + currentChoices.Count);
            }
            else if (currentChoices.Count > 0) 
            {
                continueIcon.SetActive(false);
            }

            int index = 0;
            // enable and initialize the choices up to the amount of choices for this line of dialogue
            foreach(Choice choice in currentChoices) 
            {
                choices[index].gameObject.SetActive(true);
                choicesText[index].text = choice.text;
                index++;
            }
            // go through the remaining choices the UI supports and make sure they're hidden
            for (int i = index; i < choices.Length; i++) 
            {
                choices[i].gameObject.SetActive(false);
            }

            //StartCoroutine(SelectFirstChoice());
        }

        private IEnumerator SelectFirstChoice() 
        {
            // Event System requires we clear it first, then wait
            // for at least one frame before we set the current selected object.
            EventSystem.current.SetSelectedGameObject(null);
            yield return new WaitForEndOfFrame();
            EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
        }

        public void MakeChoice(int choiceIndex)
        {
            if (canContinueToNextLine) 
            {
                currentStory.ChooseChoiceIndex(choiceIndex);
                // NOTE: The below two lines were added to fix a bug after the Youtube video was made
                Character.InputManager.Instance.RegisterSubmitPressed(); // this is specific to my InputManager script
                hasChosenChoice = true;
                ContinueStory();
            }
        }

        public Ink.Runtime.Object GetVariableState(string variableName) 
        {
            Ink.Runtime.Object variableValue = null;
            dialogueVariables.variables.TryGetValue(variableName, out variableValue);
            if (variableValue == null) 
            {
                Debug.LogWarning("Ink Variable was found to be null: " + variableName);
            }
            return variableValue;
        }

        // This method will get called anytime the application exits.
        // Depending on your game, you may want to save variable state in other places.
        public void OnApplicationQuit() 
        {
            dialogueVariables.SaveVariables();
        }



        private void PrepareForDialogue(bool isStarting)
        {
            if(isStarting)
            {
                GameManager.PauseGame();
                Character.InputManager.Instance.SetMovementActionMap(false);
                Character.InputManager.Instance.SetUIActionMap(true);
            }
            else
            {
                GameManager.ResumeGame();
                Character.InputManager.Instance.SetMovementActionMap(true);
                Character.InputManager.Instance.SetUIActionMap(false);
            }
        }

    }

}