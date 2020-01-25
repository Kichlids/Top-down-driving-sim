using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class DayNightCycle : MonoBehaviour
{
    #region Constant Variables

    private const float SECONDS_IN_ONE_DAY = 86400f;        // Number of seconds in 24 hours                       
    private const float TIME_MULTIPLIER = 1500f;            // Speed multiplier at which the in game time passes

    #endregion

    private float time;                                     // Real time in seconds
    private TimeSpan currentTIme;
    private int days = 1;                                   // Number of in game days passed

    // Sunlight variables
    public Transform sunTransform;                          // Sun's transform
    public Light sunLight;                                  // Sun's light
    private float intensity;                                // The intensity of the sunlight
    
    public Color fogColorDay = Color.grey;                  // Color of the fog in daylight
    public Color fogColorNight = Color.black;               // Color of the fog at night

    // UI Variables
    public Text timeText;   
    public Text dayText;

    private void Start()
    {
        dayText.text = "Day " + days;
    }

    private void Update()
    {
        ChangeTime();   
    }

    private void ChangeTime()
    {
        // Increment time
        time += Time.deltaTime * TIME_MULTIPLIER;

        // One day has passed
        if (time > SECONDS_IN_ONE_DAY)
        {
            days++;
            time = 0;
            // Display new date
            dayText.text = "Day " + days;
        }

        // Display current time
        currentTIme = TimeSpan.FromSeconds(time);
        string[] tempTime = currentTIme.ToString().Split(":"[0]);
        timeText.text = tempTime[0] + ":" + tempTime[1];

        // Revolve the sun based on the current time
        sunTransform.rotation = Quaternion.Euler(new Vector3((time - SECONDS_IN_ONE_DAY / 4) / SECONDS_IN_ONE_DAY * 360, 0, 0));

        // Change the intensity of the sunlight and the color of the fog
        if (time < SECONDS_IN_ONE_DAY / 2)
            intensity = 1 - (SECONDS_IN_ONE_DAY / 2 - time) / (SECONDS_IN_ONE_DAY / 2);
        else
            intensity = 1 - (SECONDS_IN_ONE_DAY / 2 - time) / (SECONDS_IN_ONE_DAY / 2) * -1; 

        RenderSettings.fogColor = Color.Lerp(fogColorNight, fogColorDay, Mathf.Pow(intensity, 2));
        sunLight.intensity = intensity;
    }
}
