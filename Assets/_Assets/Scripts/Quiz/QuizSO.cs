using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuizSO", menuName = "ScriptableObjects/QuizSO")]
public class QuizSO : ScriptableObject
{
    public string question;
    public string[] answers;
    public int correctAnswer;
    public int points;
}
