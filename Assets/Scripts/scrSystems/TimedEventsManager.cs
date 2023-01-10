using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TimedEventsManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {   

        switch (TimeManager.instance.timeDisplay)
        {
            case "0.40":
               // Debug.Log("0.40!");
                // code block
                break;

            case "0.50":
                //Debug.Log("0.50!");
                // code block
                break;

            case "0.60":
               // Debug.Log("0.60!");
                // code block
                break;

            case "0.70":
                //Debug.Log("0.70!");
                // code block
                break;

            case "0.80":
               // Debug.Log("0.80!");
                // code block
                break;

            case "0.90":
                //Debug.Log("0.90!");
                // code block
                break;

            default:
                // code block
                break;
        }

    }
}
