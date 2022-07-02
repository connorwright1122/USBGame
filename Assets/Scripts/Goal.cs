using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public Rigidbody rb;
    public MouseLook mouseLook;
    public Transform pTransform;
    public Stopwatch stopwatch;
    public PauseMenu pauseMenu;
    public LevelCompleteMenu lcMenu;

    private bool connected = false;

    private float angleAccuracyScore;
    private string timeString;


    void Start()
    {
        rb = GameObject.FindWithTag("Player").GetComponent<Rigidbody>();
        mouseLook = GameObject.FindWithTag("Player").GetComponent<MouseLook>();
        pTransform = GameObject.FindWithTag("Player").transform;
        stopwatch = FindObjectOfType<Stopwatch>();
        pauseMenu = FindObjectOfType<PauseMenu>();
        lcMenu = FindObjectOfType<LevelCompleteMenu>();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //freeze usb //make rb kinematic + mouse look sensitivity = 0
        //
        if (!connected)
        {
            FreezePlayer();
            pauseMenu.canPause = false;
            lcMenu.ActivateCompleteUI();

            angleAccuracyScore = GetValues();
            Debug.Log("aas : " + angleAccuracyScore + "%");

            timeString = stopwatch.currentTimeText.text;
            Debug.Log(timeString);

            //lcMenu.SetScoreValues(angleAccuracyScore, timeString);
            //StartCoroutine(lcMenu.SetScoreValues(angleAccuracyScore, timeString));
            Debug.Log("set");

            connected = true;
        }
        
    }


    void FreezePlayer()
    {
        rb.isKinematic = true;
        mouseLook.sensitivity = 0f;
        stopwatch.StopStopwatch();
    }

    private float GetValues()
    {
        //Debug.Log(pTransform.localEulerAngles);
        //Debug.Log(gameObject.transform.localEulerAngles);

        //Get differenc ebetween 
        /*
        if (pTransform.localEulerAngles.x > 180)
        {
            Debug.Log(360 - pTransform.localEulerAngles.x);
        }
        else
        {
            Debug.Log(pTransform.localEulerAngles.x);
        }
        */


        List<float> angleValues = new List<float>();
        List<float> portValues = new List<float>();
        List<float> flashValues = new List<float>();

        angleValues.Add(pTransform.localEulerAngles.x);
        angleValues.Add(pTransform.localEulerAngles.y);
        angleValues.Add(pTransform.localEulerAngles.z);

        portValues.Add(this.transform.localEulerAngles.x);
        portValues.Add(this.transform.localEulerAngles.y);
        portValues.Add(this.transform.localEulerAngles.z);


        //Debug.Log(angleValues[0]);

        //GET RELATIVE FLASH VALUES
        for (int i = 0; i < 3; i++)
        {
            float flashValue;

            if (angleValues[i] > 180)
            {
                //Debug.Log(360 - angleValues[i]);
                flashValue = 360 - angleValues[i];
            }
            else
            {
                //Debug.Log(angleValues[i]);
                flashValue = angleValues[i];
            }

            flashValues.Add(flashValue);
        }

        //CALCULATE DIFFERENCE FROM PORT ANGLE
        float angleDifference = 0;
        List<float> anglePercents = new List<float>();
        float anglePercentage = 0;

        for (int i = 0; i < 3; i++)
        {
            //Debug.Log(360 - Mathf.Abs(portValues[i] - flashValues[i]));
            float newDifference = 360f - Mathf.Abs(portValues[i] - flashValues[i]);
            angleDifference += newDifference; //360f - Mathf.Abs(portValues[i] - flashValues[i]);
            //Debug.Log((float)portValues[i]);
            //Debug.Log((float)portValues[i] / (float) flashValues[i]);
            //Debug.Log(newDifference / 360f);
            //anglePercents.Add(newDifference)
            anglePercentage += newDifference / 360f; //* 5;
        }

        //FOR ACTUAL PERCENTAGE ~~~~~~~
        //return (anglePercentage / 3) * 100;
        //Debug.Log((anglePercentage / 3) * 100);

        //FOR INFLATED PERCENTAGE ~~~~~~~~
        return (anglePercentage / 3) * 1000 - 900;

        //return angleDifference;


        //try to get percentage --- 

    }
}
