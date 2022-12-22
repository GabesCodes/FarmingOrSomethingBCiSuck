using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObject/MeleeWeps", order = 1)]



public class MeleeData : ScriptableObject // holds data we can change
{
    public float damage;
    public float swingRange;
    public float swingRate;  
}
