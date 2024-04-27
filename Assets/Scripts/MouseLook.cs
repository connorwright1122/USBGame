using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    /*
    public float mouseSensitivity = 100f;

    public Transform playerBody;

    private float xRotation = 0f;
    
    // Start is called before the first frame update
    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        playerBody.Rotate(Vector3.up * mouseX);
    }
    */

    public Vector2 turn;
    public float sensitivity = .5f;

    public float sensitivityCopy;

    public bool canLook = true;

    public Rigidbody rb;

    public bool cursorVisible = false;


    //1/6
    //public ConfigurableJoint pelvisJoint;

    private void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked; //change when 
        //Cursor.lockState = CursorLockMode.Confined; //change when in pause
        //Cursor.visible = cursorVisible;
        //Cursor.visible = false;

        //LockCursor(cursorVisible);
        //LockCursor(false);
        Cursor.lockState = CursorLockMode.Locked;
        sensitivityCopy = sensitivity;

        rb = GetComponent<Rigidbody>();
    }

    private void Update() // 1/6 fixed instead of update
    {
        if (canLook)
        {
            //6.13 removed +
            //turn.x += Input.GetAxis("Mouse X") * sensitivity;
            //turn.y += Input.GetAxis("Mouse Y") * sensitivity;
            //6.13 removed +
            turn.x = Input.GetAxis("Mouse X") * sensitivity;
            turn.y = Input.GetAxis("Mouse Y") * sensitivity;

            //1/6 // prevents from going above top when looking around
            //turn.y = Mathf.Clamp(turn.y, -60, 60);

            //OG - edits below
            //transform.localRotation = Quaternion.Euler(-turn.y, turn.x, 0);


            //6.12.22 -- thihis is where z can change 
            //transform.localRotation = Quaternion.Euler(-turn.y, turn.x, turn.x);
            //6.13.22  - changed to current z
            //transform.localRotation = Quaternion.Euler(-turn.y, turn.x, transform.localRotation.z);
            //transform.localEulerAngles = new Vector3(transform.localEulerAngles.x - turn.y, transform.localEulerAngles.y + turn.x, transform.localEulerAngles.z);

            rb.AddRelativeTorque(-turn.y, turn.x, 0f);
        }

        //Debug.Log(transform.localRotation.z);


    }

    /*
    private void FixedUpdate()
    {
        pelvisJoint.targetRotation = Quaternion.Euler(0, -turn.x, 0);
    }
    */


    public void FlipSensitivity()
    {
        if (sensitivity == 0f)
        {
            sensitivity = sensitivityCopy;
        }

        else
        {
            sensitivity = 0f;
        }
    }


    public void LockCursor(bool yes)
    {
        if (yes)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        else
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
    }
}
