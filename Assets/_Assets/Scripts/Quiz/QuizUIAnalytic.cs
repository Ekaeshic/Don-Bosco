using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class QuizUIAnalytic : MonoBehaviour
{
    [SerializeField] private QuizUIQuestion answerPrefab;
    [SerializeField] private Transform answerParent;
    [Header("UI")]
    [SerializeField] private TMP_Text quizScoreText;


    void OnEnable()
    {
        ShowQuizAnalytic();
    }

    private void ShowQuizAnalytic()
    {
        int quizScore = 0;
        int totalScore = 0;
        for(int i=0; i<QuizManager.Instance.QuizSOs.Length; i++)
        {
            QuizSO quizSO = QuizManager.Instance.QuizSOs[i];
            int playerAnswer = QuizManager.Instance.QuizAnswers[i];
            QuizUIQuestion quizUIQuestion = Instantiate(answerPrefab, answerParent);
            quizUIQuestion.SetQuestion(quizSO, playerAnswer);

            if(playerAnswer == quizSO.correctAnswer)
            {
                quizScore += quizSO.points;
            }
            totalScore += quizSO.points;
        }
        quizScoreText.text = $"{quizScore}/{totalScore}";
    }
}
