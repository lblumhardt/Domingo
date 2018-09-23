using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kart : MonoBehaviour {

    [SerializeField]
    private float accelerationConstant;

    [SerializeField]
    private float topSpeed;

    [SerializeField]
    private float brakeAccel;

    [SerializeField]
    private float minSpeed;

    public float AccelerationConstant
    {
        get
        {
            return accelerationConstant;
        }

        set
        {
            accelerationConstant = value;
        }
    }

    public float TopSpeed
    {
        get
        {
            return topSpeed;
        }

        set
        {
            topSpeed = value;
        }
    }

    public float BrakeAccel
    {
        get
        {
            return brakeAccel;
        }

        set
        {
            brakeAccel = value;
        }
    }

    public float MinSpeed
    {
        get
        {
            return minSpeed;
        }

        set
        {
            minSpeed = value;
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
