using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimateController : MonoBehaviour
{
    public enum TimeOfDay { morning, afternoon, evening , night};
    private TimeOfDay timeState;

    // Number of seconds in one in-game day
    public float lengthOfDay = 600f;

    private float morningBeginTime;
    private float afternoonBeginTime;
    private float eveningBeginTime;
    private float nightBeginTime;

    private const float MORNING_RATIO = 5 / 24f;
    private const float AFTERNOON_RATIO = 5 / 24f;
    private const float EVENING_RATIO = 3 / 24f;
    private const float NIGHT_RATIO = 11 / 24f;

    private float time;

    private void Start()
    {
        timeState = TimeOfDay.morning;

        morningBeginTime = 0;
        afternoonBeginTime = lengthOfDay * MORNING_RATIO;
        eveningBeginTime = afternoonBeginTime + lengthOfDay * AFTERNOON_RATIO;
        nightBeginTime = eveningBeginTime + lengthOfDay * EVENING_RATIO;

        time = 0;
    }

    private void Update()
    {
        time += Time.deltaTime;

        if (timeState == TimeOfDay.morning)
        {
            if (time >= afternoonBeginTime)
            {
                timeState = TimeOfDay.afternoon;
                print(timeState + ", " + time);
            }
        }
        else if (timeState == TimeOfDay.afternoon)
        {
            if (time >= eveningBeginTime)
            {
                timeState = TimeOfDay.evening;
                print(timeState + ", " + time);
            }
        }
        else if (timeState == TimeOfDay.evening)
        {
            if (time >= nightBeginTime)
            {
                timeState = TimeOfDay.night;
                print(timeState + ", " + time);
            }
        }
        else if (timeState == TimeOfDay.night)
        {
            if (time >= lengthOfDay)
            {
                timeState = TimeOfDay.morning;
                time = 0;
                print(timeState + ", " + time);
            }
        }
    }

}
