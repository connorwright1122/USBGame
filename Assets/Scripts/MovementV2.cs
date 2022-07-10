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
    public int ability = 1;
    public float boostCooldown = 2f;
    public float boostSpeed = 50f;
    public bool canBoost = true;
    


    [Header("Explosion")]
    [SerializeField] private float explosionRadius = 5f;
    [SerializeField] private float explosionForce = 500f;


    [Header("Wind")]
    public bool inWindZone = false;
    public GameObject windZone;


    


    //CHANGE COLOR
    private ChangeColor colorChanger;

    //private FixedJoint fj;
    //private GameObject grabbedObject;
    //private Rigidbody grabbedObjectRB;
    //private Transform grabbedParent;
    public bool isHolding = false;
    private Rigidbody grabbedRB;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mouseLook = GetComponent<MouseLook>();

        colorChanger = GetComponentInChildren<ChangeColor>();
        //fj = GetComponent<FixedJoint>();
    }


    private void FixedUpdate()
    {
        //GRAVITY
        Vector3 gravity = globalGravity * gravityScale * Vector3.up;
        rb.AddForce(gravity, ForceMode.Acceleration);

        //DECELERATION
        //decelSpeed = 1.5f * rb.velocity.magnitude + 40;

        //WIND
        if (inWindZone)
        {
            WindArea wa = windZone.GetComponent<WindArea>();
            rb.AddForce(wa.direction * wa.windStrength);
        }
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

        if (Input.GetKey(KeyCode.F) && isHolding) //grabbedObject != null)
        {
            DropObject();
        }
    }


    //WIND
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "WindArea")
        {
            windZone = other.gameObject;
            inWindZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "WindArea")
        {
            inWindZone = false;
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        GameObject collisionGO = collision.gameObject;

        if (collisionGO.tag == "CanGrab" && !isHolding)//grabbedObject == null)
        {
            PickupObject(collisionGO);
        }
    }


    void PickupObject(GameObject touchedOBJ)
    {
        /*
        //fj.connectedBody = collision.gameObject.GetComponent<Rigidbody>(); //fixedjoint fail - freezes
        //Rigidbody objRB = touchedOBJ.GetComponent<Rigidbody>();
        grabbedObjectRB = touchedOBJ.GetComponent<Rigidbody>();
        grabbedObjectRB.useGravity = false;
        grabbedObjectRB.drag = 10;
        grabbedObjectRB.constraints = RigidbodyConstraints.FreezeRotation;
        //objRB.constraints = RigidbodyConstraints.

        grabbedObjectRB.transform.parent = transform;
        grabbedObject = touchedOBJ;
        */

        grabbedRB = touchedOBJ.GetComponent<Rigidbody>();
        grabbedRB.useGravity = false;

        var fj = gameObject.AddComponent<FixedJoint>();

        fj.connectedBody = grabbedRB;
        isHolding = true;


        Debug.Log("p");
    }
    
    void DropObject()
    {
        /*
        //fj.connectedBody = collision.gameObject.GetComponent<Rigidbody>(); //fixedjoint fail - freezes
        //grabbedObjectRB = touchedOBJ.GetComponent<Rigidbody>();
        grabbedObjectRB.useGravity = true;
        grabbedObjectRB.drag = 1;
        grabbedObjectRB.constraints = RigidbodyConstraints.None;
        //objRB.constraints = RigidbodyConstraints.

        grabbedObject.transform.parent = null;
        grabbedObject = null;
        */

        grabbedRB.useGravity = true;
        grabbedRB = null;

        Destroy(GetComponent<FixedJoint>());

        isHolding = false;

        Debug.Log("d");
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
            else if (ability == 2)
            {
                ActivateExplosion();
            }

            colorChanger.ShowAbilityDepleted();
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
        //colorChanger.ShowAbilityDepleted();

        yield return new WaitForSeconds(boostCooldown);

        canBoost = true;
        colorChanger.ShowAbilityCharged();
        Debug.Log("br");
    }



    public void ActivateExplosion()
    {
        var surroundingObjects = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (var obj in surroundingObjects)
        {
            var rb = obj.GetComponent<Rigidbody>();
            if (rb == null) continue;

            rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
        }

        StartCoroutine(ActivateCooldown());
    }
}
