﻿using UnityEngine;

public class PlayerController : MonoBehaviour
{

    float turningConstant = 200.0f;
    float speedConstant = 6.5f;

    Kart kart;

    Floor currFloor = null;

    float currSpeed = 0.0f;

    private float airFriction = 1.6f;

    private float maxDistanceToQueryForRaycastHit = 0.5f;

    [SerializeField]
    private float jumpeHeight = 10.0f;

    [SerializeField]
    private Joystick joystick;

    [SerializeField]
    private GameObject brakeButtonObject;

    private BrakeButton brakeButtonScript;

    int jumpDebug = 0;

    public bool gasDown = false;

    void Start()
    {
        kart = this.GetComponent<Kart>();
        brakeButtonScript = brakeButtonObject.GetComponent<BrakeButton>();
    }

    void Update()
    {
        //find floor
        //currFloor = FindFloorIfAny();
        if (currFloor != null)
        {
            //Debug.Log("there's a floor beneath me!");
        }
        else
        {
            //Debug.Log("I'm jumping!!");
        }
        Move();
    }

    public void Move()
    {
        //var x = Input.GetAxis("Horizontal") * Time.deltaTime * turningConstant;
        var x = joystick.Horizontal * Time.deltaTime * turningConstant;
        transform.Rotate(0, x, 0);
        
        //jump
        if (Input.GetKey(KeyCode.Space) && !isJumping())
        {
            jumpDebug++;
            Debug.Log("I've jumped " + jumpDebug + " times");
            transform.Translate(0, jumpeHeight, 0);
        }

        //apply gas
        if (Input.GetKey(KeyCode.W) || gasDown)
        {
            currSpeed = currSpeed + (kart.AccelerationConstant * Time.deltaTime);

        }
           
        //apply brakes
        if (Input.GetKey(KeyCode.S) || brakeButtonScript.pressed)
        {
            currSpeed = currSpeed - (kart.BrakeAccel * Time.deltaTime);
        }


        if (currSpeed > kart.TopSpeed)
        {
            currSpeed = kart.TopSpeed;
        }
        if (currSpeed < kart.MinSpeed)
        {
            currSpeed = kart.MinSpeed;
        }

        //If not applying gas or brakes, bring speed closer to 0
        if (!Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W)) 
        {
            float decelConstant = airFriction;
            if (currFloor != null)
            {
                decelConstant = currFloor.DecelConstant;
            }

            if (currSpeed < 0)
            {
                currSpeed = currSpeed + (decelConstant * Time.deltaTime);
                if (currSpeed > 0)
                {
                    currSpeed = 0;
                }
            }
            else
            {
                currSpeed = currSpeed - (decelConstant * Time.deltaTime);
                if (currSpeed < 0)
                {
                    currSpeed = 0;
                }
            }
        }

        float distance = currSpeed * Time.deltaTime;
        transform.Translate(0, 0, distance);

    }

    /*
     * Finds the floor directly below the player. If player is in the air, floor will be null
     * used to apply decel and effects
     * */
    private Floor FindFloorIfAny()
    {
        //raycast directly below player
        RaycastHit hit;
        if (Physics.Raycast(this.transform.position, new Vector3(0, -0.01f, 0), out hit, maxDistanceToQueryForRaycastHit))
        {
            return hit.transform.gameObject.GetComponent<Floor>();
        }

        return null;
    }

    //Colliding with players and Impassable obstacles
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Impassable"))
        {
            Vector3 backwards = -transform.forward;
            GetComponent<Rigidbody>().AddForce(backwards * 20.0f);
            currSpeed = -currSpeed * 0.3f;
            if (currSpeed > -1.5f)
            {
                currSpeed = -1.5f;
            }
        }
    }

    /**
     * Returns true if the player is in the air this frame
     */
    private bool isJumping()
    {
        //find floor
        Floor floor = FindFloorIfAny();
        if (floor != null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}