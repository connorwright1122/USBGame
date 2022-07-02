using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordTransform : MonoBehaviour
{
    [SerializeField] Transform target;
    public bool isLerp = false;
    void Update()
    {
        //transform.position = new Vector3(target.position.x, target.position.y, target.position.z);
        
        if (isLerp)
        {
            transform.position = Vector3.Lerp(transform.position, target.position, 70 * Time.deltaTime);
        }
        else
        {
            transform.position = new Vector3(target.position.x, target.position.y, target.position.z);
        }
        
        transform.rotation = Quaternion.Euler(target.rotation.eulerAngles.x, target.rotation.eulerAngles.y, target.rotation.eulerAngles.z);
    }
}
