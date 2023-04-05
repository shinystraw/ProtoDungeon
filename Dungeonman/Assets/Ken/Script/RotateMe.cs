using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateMe : MonoBehaviour
{
    public float rotateTime = 0.5f; // The time in seconds to rotate the object
    public float rotateSpeed = 1080f; // The initial speed of rotation
    private float timer = 0.0f; // The timer to track the rotation time
    private Rigidbody2D rb; // Referendce to the Rigidbody2D component

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Get the reference to the Rigidbody2D component
    }

    void FixedUpdate()
    {
        if (timer < rotateTime)
        {
            float rotateAmount = rotateSpeed * Time.deltaTime;
            rb.AddTorque(rotateAmount); // Apply torque to rotate the object
            timer += Time.deltaTime;
            float drag = Mathf.Lerp(0f, 5f, timer / rotateTime); // Gradually increase the angular drag to slow down the rotation
            rb.angularDrag = drag;
        }
    }
}

