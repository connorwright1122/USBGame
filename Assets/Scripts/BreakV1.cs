using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakV1 : MonoBehaviour
{
    public Transform brokenObject;
    public float magnitudeCol;//, explosionRadius, explosionPower;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude > magnitudeCol)
        {
            Destroy(gameObject);
            Instantiate(brokenObject, transform.position, transform.rotation);
            brokenObject.localScale = transform.localScale;
            //Vector3 explosionPos = transform.position;
        }
    }
}
