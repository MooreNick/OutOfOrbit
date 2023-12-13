// pulled from unity forum by user villevli
// link: https://discussions.unity.com/t/making-top-down-spaceship-movement-getting-current-speed-acceleration-without-rigidbody/199262

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

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

    private float health = 10;
    public TextMeshProUGUI healthText;

    // Weapon variables
    public GameObject projectile;
    private GameObject newProjectile;
    private float shotTimer = 0.0f;
    private float powerUpTimer = 0.0f;
    [SerializeField]
    private float cooldown = 0.5f;

    public TextMeshProUGUI powerUpText;

    // Upgrade states
    private bool doubleShot = false;
    private bool burstShot = false;
    private bool fullAuto = false;


    private float oldVertAccel;
    private float oldHorizAccel;

    private void Awake()
    {
        playerInput = new PlayerInput();
    }

    private void Start()
    {
        healthText.SetText("Health: 10/10");
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
            Fire();
        }

        if (powerUpTimer > 0)
        {
            --powerUpTimer;
        }

        if (powerUpTimer == 0)
        {
            doubleShot = false;
            burstShot = false;
            fullAuto = false;
            powerUpText.SetText("");
        }

        if (health == 0)
        {
            Destroy(this);
            SceneManager.LoadScene("DeathMenu");
        }

    }
    
    // Player triggers a power up
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "powerUp")
        {
            // Upgrades might not work together so turn off any current ones
            burstShot = false;
            doubleShot = false;
            fullAuto = false;

            // Picks a random upgrade to give to the player
            float pick = Random.Range(1, 4);   
            switch (pick)
            { 
                case 1:
                    burstShot = true; 
                    powerUpTimer = 600.0f;
                    powerUpText.SetText("Burst Fire");
                    break;
                    
                case 2:
                    doubleShot = true;
                    powerUpTimer = 800.0f;
                    powerUpText.SetText("Double Shot");
                    break;

                case 3:
                    fullAuto = true;
                    powerUpTimer = 400.0f;
                    powerUpText.SetText("Full Auto");
                    break;
            }

            // Destroy the powerUp
            
            Destroy(collision.gameObject);
        }

        if (collision.tag == "enemyProjectile")
        {
            --health;
            healthText.SetText("Health: " + health + "/10");
        }
    }

    private void Fire()
    {
        if (doubleShot)
        {
            Instantiate(projectile, transform.position - new Vector3(0.2f, 0.0f, 0.0f), transform.rotation);
            Instantiate(projectile, transform.position + new Vector3(0.2f, 0.0f, 0.0f), transform.rotation);
            shotTimer = 0.0f;
        }
        else if (burstShot) {
            StartCoroutine(BurstFire());
            shotTimer = 0.0f;
        }
        else if (fullAuto)
        {
            Instantiate(projectile, transform.position, transform.rotation);
        }
        else
        {
            Instantiate(projectile, transform.position, transform.rotation);
            shotTimer = 0.0f;
        }
    }

    private IEnumerator BurstFire()
    {
        Instantiate(projectile, transform.position, transform.rotation);
        yield return new WaitForSeconds(0.125f);
        Instantiate(projectile, transform.position, transform.rotation);
        yield return new WaitForSeconds(0.125f);
        Instantiate(projectile, transform.position, transform.rotation);

    }

    public void OnNpcZoneTriggered(Component sender, object data)
    {
        pauseMovement();
        faceNPC();
    }

    public void OnLeaveCutscene(Component sender, object data)
    {
        unpauseMovement();
    }

    public void unpauseMovement()
    {
        verticalInputAcceleration = oldVertAccel;
        horizontalInputAcceleration = oldHorizAccel;

        oldVertAccel = 0;
        oldHorizAccel = 0;
    }

    private void pauseMovement()
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

    private void faceNPC()
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
