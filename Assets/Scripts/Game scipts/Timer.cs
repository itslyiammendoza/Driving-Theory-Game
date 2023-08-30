using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    public TMPro.TMP_Text timerText;
    private bool completed = false;

    public static string minutes;
    public static string seconds;

    // Start is called before the first frame update
    void Start()
    {
        minutes = "0";
        seconds = "0";
    }

    // Update is called once per frame
    void Update()
    {
        if (completed) 
            return; 
        if (QuestGiver.timerStarted == true) 
        {
            float t = Time.time - QuestGiver.startTime;

            minutes = ((int)t / 60).ToString();
            seconds = (t % 60).ToString("f2");

            timerText.text = minutes + ":" + seconds;
            //if the timer has been started it formats the timer properly and displays it
        }
    }

    public void Completed()
    {
        completed = true;
    }
}
