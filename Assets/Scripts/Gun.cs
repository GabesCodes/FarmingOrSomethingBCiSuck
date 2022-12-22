using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Gun : MonoBehaviour //general class for a hitscan gun.
{
    [SerializeField]
    public bool canShoot;
    public bool canReload;
    public bool reloading;
    public int currentAmmoInClip;
    public int ammoInReserve;

    [SerializeField]
    private GunData data;

    [SerializeField] 
    private LayerMask enemy;

    public Vector3 defaultPosition;
    public Vector3 aimingPosition;

    public float aimSmoothing;





    //public GameObject impactEffect;

    // Start is called before the first frame update
    void Start()
    {

        currentAmmoInClip = data.clipSize;
        ammoInReserve = data.reservedAmmoCapacity;

        canShoot = true;

    }

    // Update is called once per frame
    private void Update()
    {

        //initiate shoot 
        if (Input.GetMouseButton(0) && canShoot && !reloading && currentAmmoInClip > 0)
        {
            canReload = true;
            canShoot = false;
            currentAmmoInClip--;
            StartCoroutine(ShootHitscanGun());
        }

        //reload code
        if (Input.GetKeyDown(KeyCode.R) && canReload && !reloading &&  currentAmmoInClip  < data.clipSize && ammoInReserve > 0)
        {
            StartCoroutine(ReloadGunIE());

        }
    }

    void Hitscan()
    {
        RaycastHit hit;

        //shoot a raycast, if we hit something return true, if true, get whatever object it hit and store info to target var, if it has health, dmg it 
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, data.range))
        {
            Health target = hit.transform.GetComponent<Health>();
            if (target != null) //  only do this if component is found, target is not null (nothing)
            {
                Debug.Log("gun hit!");
                target.TakeDamage(damage: data.damage);
            }
            
        }
    }
    IEnumerator ShootHitscanGun()
    {
        Hitscan();
        yield return new WaitForSeconds(data.fireRate);
        canShoot = true;
    }

    IEnumerator ReloadGunIE()
    {
        ReloadGun();
        canReload = false;
        reloading = true;
        canShoot = false;
        yield return new WaitForSeconds(1.20f);
        reloading = false;
        canShoot = true;
    }

    void ReloadGun()
    {
        {
            int amountNeeded = data.clipSize - currentAmmoInClip; //calculate how much to take from reserves

            if (amountNeeded >= ammoInReserve)
            {
                currentAmmoInClip += ammoInReserve;
                ammoInReserve -= amountNeeded;
            }
            else
            {
                currentAmmoInClip = data.clipSize;
                ammoInReserve -= amountNeeded; //math is hard
            }
        }
    }
}