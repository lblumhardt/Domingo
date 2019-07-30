using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{

    public GameObject player;       //Public variable to store a reference to the player game object


    private float offset;         //Private variable to store the offset distance between the player and camera

    // Use this for initialization
    void Start()
    {
        //distance from camera to player
        offset = 1.3f;
    }

    // LateUpdate is called after Update each frame
    void LateUpdate()
    {

        transform.position = player.transform.position - (player.transform.forward * offset) + new Vector3(0.0f, 0.3f, 0.0f);
        transform.LookAt(player.transform);
    }
}