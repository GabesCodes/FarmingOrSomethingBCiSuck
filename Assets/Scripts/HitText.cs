using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitText : MonoBehaviour
{
    Camera cameraMain;

    public float DestroyTextTime;
    public Vector3 offset = new Vector3(0,1,0);
    // Start is called before the first frame update
    void Start()
    {
        cameraMain = Camera.main;
        transform.LookAt(cameraMain.transform);
        transform.rotation = Quaternion.LookRotation(cameraMain.transform.forward);

        Destroy(gameObject, DestroyTextTime);
        transform.localPosition += offset;
    }
}
