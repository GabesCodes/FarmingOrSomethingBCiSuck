using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponSwitcher : MonoBehaviour
{
    public TextMeshProUGUI currentWeaponText;

    public int currentWeapon;
    public bool isUsingWeapon;
    public Transform[] weapons;
    string weaponName;

    // Start is called before the first frame update
    void Start()
    {
        changeWeapon(0);
        currentWeaponText.text = "Hand";
        isUsingWeapon = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            weaponName = "Hand";
            isUsingWeapon = false;
            changeWeapon(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            weaponName = "Melee";
            isUsingWeapon = true;

            changeWeapon(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            weaponName = "Assault";
            isUsingWeapon = true;

            changeWeapon(2);
        }  
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            weaponName = "Pistol";
            isUsingWeapon = true;
            changeWeapon(3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            weaponName = "Shield";
            isUsingWeapon = true;
            changeWeapon(4);
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
