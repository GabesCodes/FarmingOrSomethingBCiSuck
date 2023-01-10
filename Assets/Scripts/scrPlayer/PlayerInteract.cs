using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private Hunger hunger;
    private FoodHandler foodHandler;
    private PlayerScript playerScript;
    //public LayerMask food;

    Vector3 foodCurrentPosition;
    Vector3 playerArmPosition;


    // Start is called before the first frame update
    void Start()
    {
        hunger = GetComponent<Hunger>();
        playerScript = GetComponent<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButton(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, 10))
            {
                //if(hit.collider.)
            }
        }
    }
}
