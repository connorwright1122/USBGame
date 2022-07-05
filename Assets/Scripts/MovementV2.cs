using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementV2 : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 1f;
    public float turnSpeed = .2f;
    public float maxTurnSpeed = 1f;

    public Rigidbody rb;
    public MouseLook mouseLook;

    private Vector3 savedVelocity;
    private Vector3 savedAngularVelocity;


    //public float decelSpeed;


    [Header("Gravity")]
    public float gravityScale = 1.0f;
    public static float globalGravity = -9.81f;


    [Header("Boost")]
    public float boostSpeed = 50f;
    public int ability = 1;
    public bool canBoost = true;
    public float boostCooldown = 2f;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mouseLook = GetComponent<MouseLook>();
    }


    private void FixedUpdate()
    {
        //GRAVITY
        Vector3 gravity = globalGravity * gravityScale * Vector3.up;
        rb.AddForce(gravity, ForceMode.Acceleration);

        //DECELERATION
        //decelSpeed = 1.5f * rb.velocity.magnitude + 40;
    }



    void Update()
    {
        //FORWARD
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(transform.forward * speed);
        }

        else if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(transform.forward * -speed);
        }

       
        /*
        //DECELERATE
        else
        {
            rb.velocity -= rb.velocity * 0.9f; // * Time.deltaTime;
        }

        Debug.Log(rb.velocity);
        */


        //ROTATE
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddRelativeTorque(0f, 0f, turnSpeed);
            mouseLook.canLook = false;
        }

        else if (Input.GetKey(KeyCode.D))
        {
            rb.AddRelativeTorque(0f, 0f, -turnSpeed);
            mouseLook.canLook = false;
        }

        else
        {
            mouseLook.canLook = true;
            //rb.angularVelocity = Mathf.Clamp(rb.angularVelocity, -maxTurnSpeed, maxTurnSpeed);
            //good 

            //CLAMP ROTATION SPEED -- 7.2.22 removed
            //rb.angularVelocity = Vector3.ClampMagnitude(rb.angularVelocity, maxTurnSpeed);
            

            //Vector3 newAngularVelocity = Vector3.ClampMagnitude(rb.angularVelocity, maxTurnSpeed);
            //rb.angularVelocity = Vector3.Lerp(rb.angularVelocity, newAngularVelocity, 1f);
        }


        //Ability
        if (Input.GetKey(KeyCode.LeftShift))
        {
            ActivateAbility(ability);
        }
    }

    

    public void FreezePlayer()
    {
        savedVelocity = rb.velocity;
        savedAngularVelocity = rb.angularVelocity;
        rb.isKinematic = true;
        mouseLook.sensitivity = 0f;
    }

    public void UnfreezePlayer()
    {
        rb.isKinematic = false;
        rb.AddForce(savedVelocity, ForceMode.VelocityChange);
        rb.AddTorque(savedAngularVelocity, ForceMode.VelocityChange);
        mouseLook.sensitivity = mouseLook.sensitivityCopy;
    }





    //ABILITIES
    public void ActivateAbility(int ability)
    {
        if (canBoost)
        {
            if (ability == 1)
            {
                ActivateBoost();
            }
        }
        
        
    }


    public void ActivateBoost()
    {
        rb.AddForce(transform.forward * boostSpeed, ForceMode.VelocityChange);
        //ability = 2;

        StartCoroutine(ActivateCooldown());
    }

    IEnumerator ActivateCooldown()
    {
        canBoost = false;

        yield return new WaitForSeconds(boostCooldown);

        canBoost = true;
        Debug.Log("br");
    }
}
