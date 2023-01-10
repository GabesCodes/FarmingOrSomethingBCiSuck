using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundCheck : MonoBehaviour
{
    CapsuleCollider capsule;

    public GameObject player;
    public LayerMask ground;


    [SerializeField]
    public static bool isGrounded;
    public static int isGroundedInt;

    public float groundCheckDistance;


    public RaycastHit groundHit;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //create var hit, equal to a raycast that takes in the current obj position, looks down to check, outputs data to groundHit, distance of 1?
        var hitFloor = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance);
        Debug.DrawRay(transform.position, Vector3.down, Color.red);

        
        if (hitFloor)
        {
            isGrounded = true;
            //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down), Color.green);
            //Debug.Log(hitFloor);
            isGroundedInt = 1;
        }
        else if(!hitFloor)
        {
            isGrounded = false;
            //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down), Color.red);
            //Debug.Log(hitFloor);
            isGroundedInt = 0;
        }
    }
}  