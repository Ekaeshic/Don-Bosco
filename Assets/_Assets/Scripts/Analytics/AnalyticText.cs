using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using TMPro;

namespace DonBosco.Analytics
{
    public class AnalyticText : MonoBehaviour
    {
        [SerializeField] private TMP_Text leftText;
        [SerializeField] private TMP_Text rightText;



        private void OnEnable()
        {
            ShowText();
        }


        private void ShowText()
        {
            // Time Spent
            leftText.text = "Time Spent:";
            int minutes = Mathf.FloorToInt(Analytic.Instance.timeSpentInGame / 60f);
            float seconds = Analytic.Instance.timeSpentInGame % 60f;
            rightText.text = minutes.ToString() + "m:" + seconds.ToString("00") + "s";
        }
    }
}