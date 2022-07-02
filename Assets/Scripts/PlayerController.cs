using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float strafeSpeed;
    public float jumpForce;
    //public float upForce;

    public Rigidbody hips;
    public bool isGrounded = false;
    public bool isGrounded1 = false;
    public GameObject flatEmpty;

    public Animator animator;


    void Start()
    {
        hips = GetComponent<Rigidbody>();
        //distToGround = hips.GetComponent<Collider>().collider.bounds.extents.y;
    }

    private void FixedUpdate()
    {
        if (!Input.anyKey)
        {
            animator.SetBool("isWalk", false);
        }

        else
        {



            if (Input.GetKey(KeyCode.W))
            {
                animator.SetBool("isWalk", true);

                if (Input.GetKey(KeyCode.LeftShift))
                {
                    hips.AddForce(hips.transform.forward * speed * 1.5f);
                    //hips.AddForce(Vector3.forward * speed * 1.5f);
                }

                else
                {
                    //hips.AddForce(hips.transform.forward * speed);
                    //hips.AddForce(Vector3.forward * speed); // global forward

                    //need to go forward with local z only

                    //when rotation of x acis = 90*, z is flat 
                    //just make a new gameobject to with flat z axis 
                    //hips.AddForce(new Vector3(0, 0, hips.transform.localRotation.z) * speed);
                    //flatEmpty.transform.localRotation.eulerAngles.z = hips.rotation.z;
                    //flatEmpty.transform.localRotation.z = 1;

                    //float z = flatEmpty.eulerAngles.z;

                    //Vector3 newRotation = new Vector3(0, 0, flatEmpty.transform.localRotation.z);

                    //hips.AddForce(flatEmpty.transform.forward * speed);
                    //hips.AddForce(newRotation * speed);
                    hips.AddForce(flatEmpty.transform.forward * speed);
                    //Debug.Log("Forward");

                }

            }
            else
            {
                //animator.SetBool("isWalk", false);
            }


            if (Input.GetKey(KeyCode.A))
            {
                hips.AddForce(-hips.transform.right * strafeSpeed);
                //hips.AddForce(-Vector3.right * strafeSpeed);
                animator.SetBool("isWalk", true);
            }
            else
            {
                //animator.SetBool("isWalk", false);
            }


            if (Input.GetKey(KeyCode.S))
            {
                hips.AddForce(-hips.transform.forward * speed);
                //hips.AddForce(-Vector3.forward * speed);
                animator.SetBool("isWalk", true);
            }
            else
            {
               // animator.SetBool("isWalk", false);
            }


            if (Input.GetKey(KeyCode.D))
            {
                hips.AddForce(hips.transform.right * strafeSpeed);
                //hips.AddForce(Vector3.right * strafeSpeed);
                animator.SetBool("isWalk", true);
            }
            else
            {
                //animator.SetBool("isWalk", false);
            }


            if (Input.GetKey(KeyCode.T))
            {
                animator.SetBool("isT", true);
            }
            else
            {
                animator.SetBool("isT", false);
            }


            if (Input.GetAxis("Jump") > 0)
            {
                isGrounded1 = IsGrounded();
                //Debug.Log("isGrounded1" + isGrounded1);
                if (isGrounded1)
                {
                    //Debug.Log("Jump");
                    hips.AddForce(new Vector3(0, jumpForce, 0));
                    isGrounded1 = false;
                }
            }

        }


        //always up force
        //hips.AddForce(Vector3.up * upForce);
    }


    private bool IsGrounded()
    {
        //woek on this 

        //return (Physics.Raycast(transform.position, -Vector3.up, 1f, hit) && hit.collider.tag == "Grounded");
        //return (Physics.Raycast(transform.position, out hit1, -Vector3.up * 1f, out hit) && hit.collider.tag == "Grounded");
        Ray ray = new Ray(transform.position, -Vector3.up);
        RaycastHit hit;
        //var hit : RaycastHit ;

        //int layerMask = 10;

        /*
        if(Physics.Raycast(ray, out hit))
        {
            if (hit.collider.tag.Equals("Ground"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
        */

        //return (Physics.Raycast(transform.position, -Vector3.up, out hit, 1f, layerMask)) ;
          

        //change maxDistance (10f)
        if (Physics.Raycast(transform.position, -Vector3.up, out hit, 3f))
        {
            if (hit.collider.tag.Equals("Ground"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }





    /*
     * private void SwitchState(State state)
    {
        switch (currentState)
        {
            case State.Walking:
                ExitWalkingState();
                break;
            case State.Knockback:
                ExitKnockbackState();
                break;
            case State.Dead:
                ExitDeadState();
                break;
        }

        switch (state)
        {
            case State.Walking:
                EnterWalkingState();
                break;
            case State.Knockback:
                EnterKnockbackState();
                break;
            case State.Dead:
                EnterDeadState();
                break;
        }

        currentState = state;
    }


    private void Update()
    {
        switch (currentState)
        {
            case State.Walking:
                UpdateWalkingState();
                break;
            case State.Knockback:
                UpdateKnockbackState();
                break;
            case State.Dead:
                UpdateDeadState();
                break;
        }
    }
    private Animator anim;
    private string currentStateAnim;

    const string KEP_PATROL = "Kep_Patrol";
    const string KEP_ENGAGE = "Kep_Engage";
    const string KEP_ROLL = "Kep_Roll";

    private void ChangeAnimationState(string newState)
    {
        if (currentStateAnim == newState) return;
        
        anim.Play(newState);

        currentStateAnim = newState;
    }

    ChangeAnimationState(KEP_ENGAGE);

     */
}
