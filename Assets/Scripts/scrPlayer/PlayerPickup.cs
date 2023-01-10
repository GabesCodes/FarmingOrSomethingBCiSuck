using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerPickup : MonoBehaviour
{
    public bool isCarryingItem;

    public float pickupDistance = 5f;
    public float placeDistance = 10f;

    public float ItemCarriedValue;

    public GameObject player;
    public GameObject CurrentItemCarried;

    public Transform itemHolder;

    public LayerMask canPickupLayer;
    public LayerMask canPlaceLayer;

    private FoodHandler foodHandler;
    private WeaponManager weaponManager;

    private PlayerScript playerScript;
    private Hunger hunger;

    public TextMeshProUGUI weapon;



    // Start is called before the first frame update
    void Start()
    {
        FoodHandler foodhandler = GetComponent<FoodHandler>();
        WeaponManager weaponManager = GetComponent<WeaponManager>();
        PlayerScript player = GetComponent<PlayerScript>();
        Rigidbody rb = GetComponent<Rigidbody>();
        Hunger hunger = GetComponent<Hunger>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isCarryingItem && !WeaponManager.instance.isUsingWeapon)
        {
            pickupItem();
        }

        if(Input.GetMouseButtonDown(1) && isCarryingItem )
        {
            placeItem();
        }

        if (Input.GetKeyDown(KeyCode.E) && isCarryingItem)
        {
            eatItem();
        }
    }

    private void eatItem()
    {
        player.GetComponent<Hunger>().hungerStat += ItemCarriedValue;
        player.GetComponent<Health>().health += ItemCarriedValue;
        Destroy(CurrentItemCarried);
        isCarryingItem = false;
    }

    private void pickupItem()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, pickupDistance, canPickupLayer))
        {
            hit.collider.transform.parent = itemHolder.transform.parent;
            hit.collider.attachedRigidbody.isKinematic = true;

            Vector3 newPosition = hit.collider.transform.position;

            newPosition = itemHolder.transform.position;

            hit.collider.transform.position = newPosition;

            isCarryingItem = true;

            CurrentItemCarried = hit.collider.gameObject;
            ItemCarriedValue = hit.collider.gameObject.GetComponent<FoodHandler>().foodRestore;

        }
        else
        {
            return;
        }
    }

    private void placeItem()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, placeDistance, canPlaceLayer))
        {
            if (hit.collider.gameObject != null)
            {
                Vector3 placeObjectPosition = hit.point;
                CurrentItemCarried.transform.position = placeObjectPosition;
                CurrentItemCarried.GetComponentInChildren<Rigidbody>().isKinematic = false;
                CurrentItemCarried.transform.parent = null;
                isCarryingItem = false;
                CurrentItemCarried = null;
                Debug.Log(placeObjectPosition);
            }
            else
            {
                return;
            }
        }
    }
}

