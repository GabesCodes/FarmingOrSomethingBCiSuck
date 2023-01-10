using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionPoint : MonoBehaviour
{
    public Vector3 connectPos;
    public Quaternion connectRot;
    public List<Vector3> connectPointsPosition;
    public List<Quaternion> connectPointsRotation;

    // Start is called before the first frame update
    void Start()
    {
        connectPos = this.transform.position;
        connectPointsPosition = new List<Vector3>();
        connectPointsPosition.Add(this.connectPos);
        
        connectRot = this.transform.rotation;
        connectPointsRotation = new List<Quaternion>();
        connectPointsRotation.Add(connectRot);

    }
}
