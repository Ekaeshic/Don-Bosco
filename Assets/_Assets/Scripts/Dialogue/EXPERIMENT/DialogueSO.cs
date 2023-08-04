using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DonBosco.Dialogue
{
    /// <summary>
    /// Scriptable Object for Dialogue
    /// </summary>
    // [CreateAssetMenu(fileName = "DialogueSO", menuName = "Don-Bosco/DialogueSO", order = 0)]
    public class DialogueSO : ScriptableObject 
    {
        [SerializeField] private List<DialogueLine> dialogueLines;
    }


    [System.Serializable]
    public struct DialogueLine
    {
        public string SpeakerName;
        public string Line;
    }

    [System.Serializable]
    public class DialogueChoice
    {
        public string Choice;
        public int NextLine;
    }
}
