using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DonBosco;
using DonBosco.API;
using DonBosco.SaveSystem;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class QuizManager : MonoBehaviour, ISaveLoad
{
    private static QuizManager instance;
    public static QuizManager Instance { get { return instance; } }
    private QuizSO[] quizSOs;
    public QuizSO[] QuizSOs { get { return quizSOs; } }
    private int[] quizAnswers;
    public int[] QuizAnswers { get { return quizAnswers; } }



    void Awake()
    {
        instance = this;
    }


    void OnEnable()
    {
        SaveManager.Instance.Subscribe(this);

        GameEventsManager.Instance.miscEvents.onChangeData += LoadQuizData;
    }

    void OnDisable()
    {
        SaveManager.Instance.Unsubscribe(this);

        GameEventsManager.Instance.miscEvents.onChangeData -= LoadQuizData;
    }


    private void GetQuizResources()
    {
        quizSOs = Resources.LoadAll<QuizSO>("Quizzes");
        quizAnswers = new int[quizSOs.Length];
    }

    public bool CheckHasAnswered(int quizId)
    {
        return quizAnswers[quizId-1] != 0;
    }

    public void SaveAnswer(int quizId, int quizAnswer)
    {
        quizAnswers[quizId-1] = quizAnswer;
    }

    private async void LoadQuizData(SaveData data)
    {
        if(data != null)
        {
            await Load(data);
        }
    }

    #region ISaveLoad
    public async Task Load(SaveData saveData)
    {
        GetQuizResources();
        
        if(saveData != null)
        {
            int[] quizAnswers = saveData.quizAnswers;

            for (int i = 0; i < quizAnswers.Length; i++)
            {
                this.quizAnswers[i] = quizAnswers[i];
            }
        }
        await Task.CompletedTask;
    }

    public async Task Save(SaveData saveData)
    {
        saveData.quizAnswers = quizAnswers;
        await Task.CompletedTask;
    }
    #endregion



    #region API
    /// <summary>
    public QuizLog[] GetQuizLogs()
    {
        QuizLog[] quizLogs = new QuizLog[quizSOs.Length];
        int i = 0;
        for(; i < quizSOs.Length; i++)
        {
            int score = quizAnswers[i] == quizSOs[i].correctAnswer ? quizSOs[i].points : 0;
            quizLogs[i] = new QuizLog(i+1, score);
        }
        return quizLogs;
    }
    #endregion
}
