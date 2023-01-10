using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DayNightController : MonoBehaviour
{
    [Range(0.0f, 1.0f)] //restricts between these two in the inspector slider, helpful!
    public float time;

    public float fullDayLength;

    public float startTime = 0.4f;

    private float timeRate;

    public Vector3 noon; //rotation of sun at noon 

    public TextMeshProUGUI timeDisplayText;

    [Header("Sun")]
    public Light sun;
    public Gradient sunColor;
    public AnimationCurve sunIntensity;

    [Header("Moon")]
    public Light moon;
    public Gradient moonColor;
    public AnimationCurve moonIntensity;

    [Header("Other Lighting")]
    public AnimationCurve lightingIntensityMultiplier;
    public AnimationCurve reflectionsIntensityMultiplier;

    void Start()
    {
        timeRate = 1.0f / fullDayLength;   
        time = startTime;
    }

    void Update()
    {

        //increment time
        time += timeRate * Time.deltaTime;

        string timeString = string.Format("{0:0.00}", time);

        timeDisplayText.text = "Current time: " + timeString;

        if (time >= 1)
            time = 0;

        //light rotation
        sun.transform.eulerAngles = (time - 0.25f) * noon * 4.0f;
        moon.transform.eulerAngles = (time - 0.75f) * noon * 4.0f;

        //light intensity
        sun.intensity = sunIntensity.Evaluate(time);
        moon.intensity = moonIntensity.Evaluate(time);

        // change colors
        sun.color = sunColor.Evaluate(time); //get specific color for that time of day
        moon.color = moonColor.Evaluate(time);

        //enable / disable the sun
        if (sun.intensity == 0 && sun.gameObject.activeInHierarchy)
          sun.gameObject.SetActive(false);
        else if (sun.intensity > 0 && !sun.gameObject.activeInHierarchy)
            sun.gameObject.SetActive(true);


        if (moon.intensity == 0 && moon.gameObject.activeInHierarchy)
             moon.gameObject.SetActive(false);
         else if (moon.intensity > 0 && !moon.gameObject.activeInHierarchy)
             moon.gameObject.SetActive(true);

        

        // lighting and reflections intensity
        RenderSettings.ambientIntensity = lightingIntensityMultiplier.Evaluate(time);
        RenderSettings.reflectionIntensity = reflectionsIntensityMultiplier.Evaluate(time);

 
    }
}
