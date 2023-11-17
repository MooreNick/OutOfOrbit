// pulled from unity forum by user villevli
// link: https://discussions.unity.com/t/making-top-down-spaceship-movement-getting-current-speed-acceleration-without-rigidbody/199262

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ship_movement : MonoBehaviour
{
    public Transform npcTransform;
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

    private float oldVertAccel;
    private float oldHorizAccel;

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

    public void unpauseMovement()
    {
        verticalInputAcceleration = oldVertAccel;
        horizontalInputAcceleration = oldHorizAccel;

        oldVertAccel = 0;
        oldHorizAccel = 0;
    }

    public void pauseMovement()
    {
        velocity = velocity / 5;
        zRotationVelocity = 0;

        // save old accel settings
        oldVertAccel = verticalInputAcceleration;
        oldHorizAccel = horizontalInputAcceleration;

        // set accel to zero so player cant move
        verticalInputAcceleration = 0;
        horizontalInputAcceleration = 0;
    }

    public void faceNPC()
    {
        if (npcTransform != null)
        {
            Vector3 direction = npcTransform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            angle += -90;
            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
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
