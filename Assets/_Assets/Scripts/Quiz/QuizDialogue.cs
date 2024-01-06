using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DonBosco.Dialogue;
using UnityEngine.Events;
using DonBosco.UI;

public class QuizDialogue : MonoBehaviour
{
    public bool IsInteractable { get; set; } = true;
    [SerializeField] protected TextAsset quizDialogue;
    [SerializeField] private int quizId;

    public UnityEvent OnQuizDone;
    [SerializeField] private bool hideUIScreenOnDone = false;

    public void StartDialogue()
    {
        if(quizDialogue == null)
        {
            Debug.LogError("No dialogue or knot path assigned to NPC");
        }
        else
        {
            CheckHasAnswered();
        }
    }

    private void CheckHasAnswered()
    {
        //If player has not answered the quiz, enter dialogue mode
        if(!QuizManager.Instance.CheckHasAnswered(quizId))
        {
            DialogueManager.GetInstance().BindExternalFunction("Quiz", (answer) => SaveAnswer(answer));
            DialogueManager.GetInstance().EnterDialogueMode(quizDialogue).OnDialogueDone((variable) => 
            {
                OnQuizDone.Invoke();

                if(hideUIScreenOnDone)
                {
                    UIManager.Instance.HideScreenUI();
                }
            });

        }
    }

    private void SaveAnswer(string answer)
    {
        //Split string by underscore
        string[] answerSplit = answer.Split('_');
        
        int quizId = int.Parse(answerSplit[0]);
        int quizAnswer = int.Parse(answerSplit[1]);

        QuizManager.Instance.SaveAnswer(quizId, quizAnswer);
    }
}
