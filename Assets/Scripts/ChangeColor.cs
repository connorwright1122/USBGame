using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    //[SerializeField] 
    private Renderer myObject;

    private void Start()
    {
        myObject = this.GetComponent<Renderer>();
    }

    public void ShowAbilityCharged()
    {
        //myObject.material.color = Color.green;
        myObject.material.SetColor("_EmissionColor", Color.green);
    }
    
    public void ShowAbilityDepleted()
    {
        //myObject.material.color = Color.red;
        myObject.material.SetColor("_EmissionColor", Color.red);
    }
}
