using UnityEngine;

public class PlayerController : MonoBehaviour
{

    float turningConstant = 200.0f;
    float speedConstant = 6.5f;

    Kart kart;

    Floor currFloor = null;

    float currSpeed = 0.0f;

    private float airFriction = 1.6f;

    private float maxDistanceToQueryForRaycastHit = 2.0f;

    void Start()
    {
        kart = this.GetComponent<Kart>();
    }

    void Update()
    {
        //find floor
        currFloor = FindFloorIfAny();

        Move();
    }

    public void Move()
    {
        var x = Input.GetAxis("Horizontal") * Time.deltaTime * turningConstant;
        transform.Rotate(0, x, 0);
        
        //apply gas
        if (Input.GetKey(KeyCode.W))
        {
            currSpeed = currSpeed + (kart.AccelerationConstant * Time.deltaTime);

        }
           
        //apply brakes
        if (Input.GetKey(KeyCode.S))
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
        if (Physics.Raycast(this.transform.position, new Vector3(0, -1, 0), out hit, maxDistanceToQueryForRaycastHit))
        {
            return hit.transform.gameObject.GetComponent<Floor>();
        }

        return null;
    }
}