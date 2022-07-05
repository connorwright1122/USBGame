using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCamera : MonoBehaviour
{
    public GameObject cam1;
    public GameObject cam2;

    private int currentCam;

    private void Start()
    {
        currentCam = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SwitchCam(currentCam);
        }
    }


    void SwitchCam(int currentCam1)
    {
        if (currentCam1 == 1)
        {
            cam1.SetActive(false);
            cam2.SetActive(true);
            currentCam = 2;
        }

        else if (currentCam1 == 2)
        {
            cam1.SetActive(true);
            cam2.SetActive(false);
            currentCam = 1;
        }

    }
}
