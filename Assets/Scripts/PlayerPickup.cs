using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    public bool isCarryingItem;

    public float pickupDistance = 5f;
    public float placeDistance = 10f;

    public float ItemCarriedValue;

    public GameObject player;
    public GameObject CurrentItemCarried;
    private Rigidbody rb;

    public Transform itemHolder;

    public LayerMask canPickupLayer;
    public LayerMask canPlaceLayer;

    private FoodHandler foodHandler;
    private PlayerScript playerScript;
    private Hunger hunger;


    // Start is called before the first frame update
    void Start()
    {
        FoodHandler foodhandler = GetComponent<FoodHandler>();
        PlayerScript player = GetComponent<PlayerScript>();
        Rigidbody rb = GetComponent<Rigidbody>();
        Hunger hunger = GetComponent<Hunger>();

        isCarryingItem = false;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward, Color.red);

        if (Input.GetMouseButtonDown(0) && !isCarryingItem)
        {
            pickupItem();
        }

        if(Input.GetMouseButtonDown(1) && isCarryingItem)
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

