// pulled from unity forum by user villevli
// link: https://discussions.unity.com/t/making-top-down-spaceship-movement-getting-current-speed-acceleration-without-rigidbody/199262

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ship_movement : MonoBehaviour
{
    public float verticalInputAcceleration = 7;         // how fast ship accelerates forward
    public float horizontalInputAcceleration = 200;     // how fast ship turns

    public float maxSpeed = 800;
    public float maxRotationSpeed = 500;

    public float velocityDrag = 1.1f;
    public float rotationDrag = 1.8f;

    private Vector3 velocity;
    private float zRotationVelocity;

    private PlayerInput playerInput;

    // Weapon variables
    public GameObject projectile;
    private GameObject newProjectile;
    private float shotTimer = 0.0f;
    [SerializeField]
    private float cooldown = 0.5f;

    private void Awake()
    {
        playerInput = new PlayerInput();
    }

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

        // shotTimer increments until it's above cooldown,
        // then the player can fire.
        shotTimer = shotTimer + Time.deltaTime;

        if (playerInput.Player.Fire.ReadValue<float>() != 0
            && shotTimer >= cooldown)
        {
            newProjectile = Instantiate(projectile, transform.position, transform.rotation);
            shotTimer = 0.0f;
        }

    }

    private void OnEnable()
    {
        playerInput.Player.Enable();
    }

    private void OnDisable()
    {
        playerInput.Player.Disable();
    }
}   
