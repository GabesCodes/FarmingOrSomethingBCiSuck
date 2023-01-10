using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WaypointManager : MonoBehaviour
{
    public Transform[] childTransforms;
    private void Awake()
    {
        childTransforms = new Transform[transform.root.childCount];
        childTransforms = GetComponentsInChildren<Transform>();
        childTransforms = childTransforms.Skip(1).ToArray();
    }
}
