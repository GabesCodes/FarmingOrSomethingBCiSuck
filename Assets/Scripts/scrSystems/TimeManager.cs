using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    #region Singleton
    public static TimeManager instance;
    public string timeDisplay;
    void Awake()
    {
        instance = this;

    }

    public void Update()
    {
        time = GetComponent<DayNightController>().time;
        string timeString = string.Format("{0:0.00}", time);
        timeDisplay = timeString;
    }

    #endregion

    private float time;

    
}