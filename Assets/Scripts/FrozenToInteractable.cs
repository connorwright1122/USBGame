using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrozenToInteractable : MonoBehaviour
{
    public Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            rb.isKinematic = false;
        }
    }
}
