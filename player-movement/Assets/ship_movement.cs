// pulled from unity forum by user villevli
// link: https://discussions.unity.com/t/making-top-down-spaceship-movement-getting-current-speed-acceleration-without-rigidbody/199262

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ship_movement : MonoBehaviour
{
    public float verticalInputAcceleration = 25;
    public float horizontalInputAcceleration = 180;

    public float maxSpeed = 350;
    public float maxRotationSpeed = 700;

    public float velocityDrag = 1;
    public float rotationDrag = 1.1f;

    private Vector3 velocity;
    private float zRotationVelocity;

    private void Update()
    {
        if (Input.GetAxis("Vertical") >= 0) 
        {
            // apply forward input
            Vector3 acceleration = Input.GetAxis("Vertical") * verticalInputAcceleration * transform.up;
            velocity += acceleration * Time.deltaTime; 
        }
        

        // apply turn input
        float zTurnAcceleration = -1 * Input.GetAxis("Horizontal") * horizontalInputAcceleration;
        zRotationVelocity += zTurnAcceleration * Time.deltaTime;
    }

    private void FixedUpdate()
    {
        // apply velocity drag
        velocity = velocity * (1 - Time.deltaTime * velocityDrag);

        // clamp to maxSpeed
        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);

        // apply rotation drag
        zRotationVelocity = zRotationVelocity * (1 - Time.deltaTime * rotationDrag);

        // clamp to maxRotationSpeed
        zRotationVelocity = Mathf.Clamp(zRotationVelocity, -maxRotationSpeed, maxRotationSpeed);

        // update transform
        transform.position += velocity * Time.deltaTime;
        transform.Rotate(0, 0, zRotationVelocity * Time.deltaTime);
    }
}
