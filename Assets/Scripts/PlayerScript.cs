using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;


public class PlayerScript : MonoBehaviour
{   
    [SerializeField]
    public float currentSpeed;
    public float walkSpeed = 7.5f;
    public float sprintModifier = 2.5f;

    [SerializeField]
    private float jumpSpeed = 3.5f;

    [SerializeField]
    private float gravity = 9.81f;
    public float xPos;
    public float zPos;
    public Vector3 velocity;

    public bool isMoving;
    public bool isSprinting;
    public bool isPlayerAlive;

    public float playerHP;

    public TextMeshProUGUI playerHealthText;
    public TextMeshProUGUI playerHungerText;

    public GameObject player;
    Vector3 lastPos;

    private CharacterController ch;

    // Start is called before the first frame update
    void Start()
    {
        playerHP = 100f;
        player = this.gameObject;
        ch = gameObject.GetComponent<CharacterController>();
        isPlayerAlive = true;
        isSprinting = false;
        isMoving = false;

    }
    

    // Update is called once per frame
    void Update()
    {
        playerHungerText.text = "Hunger: " + GetComponent<Hunger>().hungerStat.ToString();
        playerHealthText.text = "Health: " + GetComponent<Health>().health.ToString(); 
 
        if (player.transform.position != lastPos)
        {
            isMoving = true;
           
        }
        else
        {
            isMoving = false;
        }

        lastPos = player.transform.position;

        //collect input
        xPos = Input.GetAxis("Horizontal"); //x is horizontal, i.e A and D key 
        zPos = Input.GetAxis("Vertical"); //z is vertical, i.e W and S key, 

        //keeps our velocity from forever incrementing
        if (groundCheck.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (groundCheck.isGrounded )
        {
            if (Input.GetButtonDown("Jump"))
            {
                //directionY is equal to jumpSpeed value
                velocity.y = jumpSpeed;
            }
        }
 
        //sprint code - look into turning this into a method
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isSprinting = true;
            Sprint();
        }
        else
        {
            isSprinting = false;
            currentSpeed = walkSpeed;
        }

        void Sprint()
        {
            currentSpeed = walkSpeed + sprintModifier;
            //Debug.Log("running!");
        }
   

        //create vector 3 move, transform (move player) to the right * xPos and do the same for zPos
        //this is better than previous implementation bc this takes in the objects current situation instead of global 
        Vector3 move = transform.right * xPos + transform.forward * zPos;

        //direction Y is then subtracted by the gravity value every frame?
        velocity.y -= gravity * Time.deltaTime;

        //then we update our move vector3 y value with our calculated directionY value
        move.y = velocity.y;

        //ultimately moves our character according to our calculations from the previous move vector3 * speed * deltatime(idk this one)
        ch.Move(move * currentSpeed * Time.deltaTime);
    }

}



