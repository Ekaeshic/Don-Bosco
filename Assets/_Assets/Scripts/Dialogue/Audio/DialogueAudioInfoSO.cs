using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "DialogueAudioInfo", menuName = "Voiceover/DialogueAudioInfoSO", order = 1)]
public class DialogueAudioInfoSO : ScriptableObject
{
    public string id;
    [HideInInspector] public DialogueAudioType dialogueAudioType = DialogueAudioType.SimpleTyping;
    [HideInInspector] public AudioClip[] dialogueTypingSoundClips;
    [HideInInspector] public AudioClip[] dialogueAlphabetSoundClips;
    [Range(1, 5)]
    public int frequencyLevel = 2;
    [Range(-3, 3)]
    public float minPitch = 0.5f;
    [Range(-3, 3)]
    public float maxPitch = 3f;
    public bool stopAudioSourceInstantly = false;
    public bool readWaitForAudioBefore = false;
    public bool readPunctuation = false;
    
    [HideInInspector] public bool hashcodeIndexing = true;
    [HideInInspector] public DialoguePitchType predictPitch = DialoguePitchType.Predictable;

    #region Editor
#if UNITY_EDITOR
    [CustomEditor(typeof(DialogueAudioInfoSO))]
    public class DialogueAudioInfoSOEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            DialogueAudioInfoSO dialogueAudioInfoSO = (DialogueAudioInfoSO)target;

            if(dialogueAudioInfoSO.readWaitForAudioBefore && dialogueAudioInfoSO.stopAudioSourceInstantly)
            {
                dialogueAudioInfoSO.stopAudioSourceInstantly = false;
            }

            //Show header
            EditorGUILayout.Space(15);
            EditorGUILayout.LabelField("Predictable Pitch", EditorStyles.boldLabel);
            // Show the Predictable Pitch
            if(dialogueAudioInfoSO.dialogueAudioType == DialogueAudioType.SimpleTyping)
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("hashcodeIndexing"), true);
            }
            if(dialogueAudioInfoSO.hashcodeIndexing == false)
            {
                // Show tips
                EditorGUILayout.HelpBox("If true, the chosen audio clip index will be predicted by their hashcode. Otherwise, the voice will be chosen randomly.", MessageType.Info);
            }
            EditorGUILayout.PropertyField(serializedObject.FindProperty("predictPitch"), true);

            // Show the Dialogue Audio Type
            EditorGUILayout.Space(15);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("dialogueAudioType"), true);
            if(dialogueAudioInfoSO.dialogueAudioType == DialogueAudioType.SimpleTyping)
            {
                // Only show the Easy Typing Sound Clips
                EditorGUILayout.PropertyField(serializedObject.FindProperty("dialogueTypingSoundClips"), true);
            }
            else if(dialogueAudioInfoSO.dialogueAudioType == DialogueAudioType.AlphabetTyping)
            {
                // Only show the Alphabet Typing Sound Clips
                EditorGUILayout.PropertyField(serializedObject.FindProperty("dialogueAlphabetSoundClips"), true);
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
#endif
    #endregion
}


public enum DialogueAudioType
{
    SimpleTyping,
    AlphabetTyping
}

public enum DialoguePitchType
{
    Predictable,
    Random,
    Median
}
