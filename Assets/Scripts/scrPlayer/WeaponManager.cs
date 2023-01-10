using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    #region Singleton
    public static WeaponManager instance;
    public bool isUsingWeapon;

    void Awake()
    {
        instance = this;
        weapons = this.gameObject;

    }
    private void Update()
    {
        isUsingWeapon = GetComponent<WeaponSwitcher>().isUsingWeapon; // please optimize this bruh 
    }

    #endregion

    private GameObject weapons;


}