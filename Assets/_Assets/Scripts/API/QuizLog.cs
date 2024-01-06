using UnityEngine;

namespace DonBosco.API
{
    [System.Serializable]
    public class QuizLog
    {
        public int no_quiz;
        public int score;


        public QuizLog(int no_quiz, int score)
        {
            this.no_quiz = no_quiz;
            this.score = score;
        }
    }
}