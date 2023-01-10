using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HitscanGun : MonoBehaviour //general class for a hitscan gun.
{
    [SerializeField]
    public bool canShoot;
    public bool canReload;
    public bool isReloading;
    public int currentAmmoInClip;
    public int ammoInReserve;

    public CameraShake cameraShake;

    public TextMeshProUGUI playerAmmoText;
    private char slash = '/';

    [SerializeField]
    private GunData data;

    [SerializeField] 
    private LayerMask damagableLayer;

    public Vector3 defaultPosition;
    public Vector3 aimingPosition;


    Animator animator;

    int isReloadingHash;
    int isShootingHash;

    public bool isShooting;


    void Start()
    {
        currentAmmoInClip = data.clipSize;
        ammoInReserve = data.reservedAmmoCapacity;

        animator = GetComponent<Animator>();
        isReloadingHash = Animator.StringToHash("isReloadingAnim");
        isShootingHash = Animator.StringToHash("isShootingAnim");

        animator.SetBool(isShootingHash, false);
        animator.SetBool(isReloadingHash, false);

        canShoot = true;

    }

    private void Update()
    {
        playerAmmoText.text = "Ammo: " + currentAmmoInClip.ToString() + slash + ammoInReserve.ToString();

        if (Input.GetMouseButton(0) && !isShooting && !isReloading && currentAmmoInClip > 0)
        {
            StartCoroutine(ShootHitscanGun());
           
        }


        //reload code 
        if (Input.GetKeyDown(KeyCode.R) && canReload && !isShooting && !isReloading && currentAmmoInClip <= data.clipSize && ammoInReserve >= 0)
        { 
           
            StartCoroutine(ReloadGunIE());
           
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            animator.Play("shooting");

        }
        if (isReloading)
        {
            playerAmmoText.text = "RELOADING!";
        }
    }

    void Hitscan()
    {
        RaycastHit hit;

        //shoot a raycast, if we hit something return true, if true, get whatever object it hit and store info to target var, if it has health, dmg it 
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, data.range, damagableLayer))
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
        isShooting = true;
        canShoot = false;
        canReload = false;
        currentAmmoInClip--;
        Hitscan();
        animator.SetBool(isShootingHash, true);
        yield return new WaitForSeconds(data.fireRate);
        animator.SetBool(isShootingHash, false);

        isShooting = false;
        canShoot = true;
        canReload = true;

    }

    IEnumerator ReloadGunIE()
    {
        isReloading = true;
        canShoot = false;
        ReloadGun();
        animator.Play("reloading");
        yield return new WaitForSeconds(data.reloadTime);
        isReloading = false;
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