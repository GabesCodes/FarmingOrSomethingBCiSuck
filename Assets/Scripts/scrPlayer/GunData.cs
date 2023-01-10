using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObject/Guns", order = 1)]



public class GunData : ScriptableObject // holds data we can change
{
    public string gunType;
    public float damage;
    public float range;
    public float fireRate;
    public float reloadTime;
    public int clipSize;
    public int reservedAmmoCapacity;
    
}
