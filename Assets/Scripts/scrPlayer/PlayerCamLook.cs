using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamLook : MonoBehaviour
{

    //the playerLook script is assigned to the whole player object so it effects all objects
    public Transform player;
    float xRotation = 0f;

    public float mouseSensitivity = 1f; //sens

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity ; //store X input to mouseX, multiply it by sensitivity, update it every frame
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity ;//store Y input to mouseX, multiply it by sensitivity, update it every frame


        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        //Vector3 rotPlayer = player.transform.rotation.eulerAngles;
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        player.Rotate(Vector3.up * mouseX);
    }
}


