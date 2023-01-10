using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectPointsManager : MonoBehaviour
{
    public List<Vector3> connectedPointsAll;

    void Start()
    {
        connectedPointsAll = new List<Vector3>();
        foreach(Transform child in transform)
        {
            connectedPointsAll.Add(child.transform.position);
        }

    }

    private void Awake()
    {
        
    }
}
