using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    public Camera mainCam;
    public Camera deathCam;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        PlayerScript player = gameObject.GetComponent<PlayerScript>();

    }

    // Update is called once per frame
    void Update()
    {
        if(!player.GetComponent<PlayerScript>().isPlayerAlive && mainCam == null)
        {
            Debug.Log("test");
            deathCam.gameObject.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            mainCam.gameObject.SetActive(false);
            deathCam.gameObject.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            mainCam.gameObject.SetActive(true);
            deathCam.gameObject.SetActive(false);

        }
    }
}







