using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuizUIQuestion : MonoBehaviour
{
    [SerializeField] private TMP_Text leftText;
    [SerializeField] private TMP_Text rightText;


    public void SetQuestion(QuizSO quizSO, int playerAnswer)
    {
        leftText.text = quizSO.question;
        if(playerAnswer == quizSO.correctAnswer)
        {
            leftText.text += "\n<color=#20841F>";
            rightText.text = $"<color=#20841F> +{quizSO.points}";
        }
        else
        {
            leftText.text += "\n<color=#841F2C>";
            rightText.text = $"<color=#841F2C> 0";
        }
        leftText.text += quizSO.answers[playerAnswer-1];
    }
}
