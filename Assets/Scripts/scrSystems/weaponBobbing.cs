using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class weaponBobbing : MonoBehaviour

{

   // public GunAim aim;

    public float magnitude;

    public float idleSpeed;

    public float walkSpeedMultiplier;

    public float walkSpeedMax;

    public float aimReduction;

    public PlayerScript PlayerScript;




    float sinY = 0f;

    float sinX = 0f;

    Vector3 lastPosition;



    private void Start()

    {
        lastPosition = transform.position;
    }





    void Update()

    {

        if (groundCheck.isGrounded)

        {
            float delta = Time.deltaTime * idleSpeed;
            float velocity = (lastPosition - transform.position).magnitude * walkSpeedMultiplier;
            delta += Mathf.Clamp(velocity, 0, walkSpeedMax);

            // Reduce by two so that the gun animation is more U shaped

            sinX += delta / 2;
            sinY += delta;

            sinX %= Mathf.PI * 2;
            sinY %= Mathf.PI * 2;

            //float magnitude = this.aim.aiming ? this.magnitude / aimReduction : this.magnitude;
            transform.localPosition = Vector3.zero + Vector3.up * Mathf.Sin(sinY) * magnitude;
            transform.localPosition += Vector3.right * Mathf.Sin(sinX) * magnitude;
        }
        else
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, Vector3.zero, Time.deltaTime);
        }
        lastPosition = transform.position;
    }

}