using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSystem : MonoBehaviour
{
    [Header("Time Settings")]
    [Tooltip("In game minute ratio to real time seconds")]
    [SerializeField] private float minuteToRealTime = 2f;
    [SerializeField] private int startHour = 12;
    [SerializeField] private int startMinute = 0;
    [SerializeField] private float dayTimeRatio = 0f;
    public float DayTimeRatio { get { return dayTimeRatio; } }

    private float minute;
    private float hour;



    private void Awake() {
        //Set the start time
        minute = startMinute;
        hour = startHour;

        UpdateTime();
    }
    


    private void Update() 
    {
        UpdateTime();
    }



    private void UpdateTime()
    {
        //Update the time
        minute += Time.deltaTime * minuteToRealTime;
        if(minute >= 60f)
        {
            minute -= 60f;
            hour++;
        }
        if(hour >= 24f)
        {
            hour -= 24f;
        }

        dayTimeRatio = (hour + minute / 60f) / 24f;

        DebugScreen.Log("<color=yellow>TimeSystem</color>");
        DebugScreen.Log("DayTimeRatio: " + dayTimeRatio);
        DebugScreen.Log("Current Time: " + hour.ToString("00") + ":" + minute.ToString("00"));
        DebugScreen.NewLine();

        Shader.SetGlobalFloat("_DayTimeRatio", dayTimeRatio);
    }
}
