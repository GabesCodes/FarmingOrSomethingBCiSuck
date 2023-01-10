using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamRotation : MonoBehaviour
{

    // Start is called before the first frame update
    private void Start()
    {
        
    }

    private void LateUpdate()
    {
        transform.rotation = Camera.main.transform.rotation;
    }
}


