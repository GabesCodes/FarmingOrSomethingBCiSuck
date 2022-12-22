using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponManager : MonoBehaviour
{
    public TextMeshProUGUI currentWeaponText;


    public int currentWeapon;
    public Transform[] weapons;
    string weaponName;

    // Start is called before the first frame update
    void Start()
    {
        changeWeapon(0);
        currentWeaponText.text = "Melee";
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            weaponName = "Melee";
            changeWeapon(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            weaponName = "Gun";
            changeWeapon(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            weaponName = "Shield";
            changeWeapon(2);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
           // changeWeapon(currentWeapon);
        }
    }

    public void changeWeapon(int num)
    {
        currentWeaponText.text = weaponName;

        currentWeapon = num;
        for (int i = 0; i < weapons.Length; i++)
        {
            if (i == num)
                weapons[i].gameObject.SetActive(true);
            else
                weapons[i].gameObject.SetActive(false);

            if(num > weapons.Length)
            {
                currentWeapon = 0;
            }
        }
    }


}
