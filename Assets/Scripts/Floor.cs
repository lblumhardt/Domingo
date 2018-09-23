using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour {

    private float decelConstant = 6.0f;

    public float DecelConstant
    {
        get
        {
            return decelConstant;
        }

        set
        {
            decelConstant = value;
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
