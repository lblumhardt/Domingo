using UnityEngine;

public class PlayerController : MonoBehaviour
{

    float turningConstant = 200.0f;
    float speedConstant = 6.5f;

    void Update()
    {
        Move();
    }

    public void Move()
    {
        var x = Input.GetAxis("Horizontal") * Time.deltaTime * turningConstant;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * speedConstant;

        transform.Rotate(0, x, 0);
        transform.Translate(0, 0, z);
    }
}