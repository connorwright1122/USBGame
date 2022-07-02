using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementOnlyForward : MonoBehaviour
{
    public float speed = 1f;
    public float turnSpeed = .2f;
    public float maxTurnSpeed = 1f;

    public Rigidbody rb;
    public MouseLook mouseLook;

    private Vector3 savedVelocity;
    private Vector3 savedAngularVelocity;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mouseLook = GetComponent<MouseLook>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(transform.forward * speed);
        }

        else if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(transform.forward * -speed);
        }

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
            rb.angularVelocity = Vector3.ClampMagnitude(rb.angularVelocity, maxTurnSpeed);
            

            //Vector3 newAngularVelocity = Vector3.ClampMagnitude(rb.angularVelocity, maxTurnSpeed);
            //rb.angularVelocity = Vector3.Lerp(rb.angularVelocity, newAngularVelocity, 1f);
        }
    }

    
    /*private void FixedUpdate()
    {
        Debug.Log(rb.velocity);
    }
    */

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
}
