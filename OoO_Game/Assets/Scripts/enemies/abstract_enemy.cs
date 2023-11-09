using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class abstract_enemy : MonoBehaviour
{
    public int health;
    public int speed;
    public Vector3 velocity;
    public SpriteRenderer enemySprite;


    public bool canPatrol = true;
    public bool patrolMode_UpDown = true;
    public bool patrolMode_LeftRight = false;

    public int patrolRange = 30;

    // Weapon variables
    public GameObject projectileEnemy;
    private GameObject newProjectileEnemy;
    private float shotTimer = 0.0f;

    [SerializeField]
    private float cooldown = 0.5f;

    public bool canShoot = false;

    public bool burstFire = false;

    [Range(2, 5)]
    public int bursts = 3;

    public bool nonStopShooting = false;    

    private int patrolTimer = 0;
    private int patrolState = 0;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            health -= 100;
        }
    }

    private void FixedUpdate()
    {
        Patrol();
        Shoot();

        if (health <= 0)
            Death();
    }

    public abstract void FlipSprite();
    
    private void Patrol()
    { 
        if (patrolMode_UpDown && patrolMode_LeftRight)
        {
            canPatrol = false; // only one mode allowed per enemy
        }

        if (canPatrol)
        {
                if (patrolTimer <= patrolRange && patrolState == 0)
                {
                    transform.Translate(velocity * speed * Time.deltaTime);
                    patrolTimer++;
                    if (patrolTimer >= 30)
                    {
                        FlipSprite();
                        patrolState = 1;
                    }

                }
                else
                {
                    transform.Translate(-velocity * speed * Time.deltaTime); ;
                    patrolTimer--;
                    if (patrolTimer <= -patrolRange)
                    {
                        FlipSprite();
                        patrolState = 0;
                    }

                }
        }
    }
    
    public void Death()
    {
        Destroy(this.gameObject);
    }

    public void Shoot()
    {
        // Quaterion rotation var is used to rotate the bullet 180 degrees when the enemy is patrolling the opposite direction
        // This makes the bullets travel in the same direction the ship is traveling

        shotTimer = shotTimer + Time.deltaTime;

        if (canShoot)
        {
            if (patrolMode_UpDown)
            {
                if (nonStopShooting)
                {
                    if (patrolState == 0)
                    {
                        newProjectileEnemy = Instantiate(projectileEnemy, this.transform.position, this.transform.rotation) as GameObject;
                    }

                    else
                    {
                        Quaternion rotation = Quaternion.Euler(0, 0, 180);
                        newProjectileEnemy = Instantiate(projectileEnemy, this.transform.position, rotation) as GameObject;
                    }
                }

                if (shotTimer >= cooldown && !nonStopShooting)
                {
                    if (patrolState == 0 && !burstFire)
                    {
                        newProjectileEnemy = Instantiate(projectileEnemy, this.transform.position, this.transform.rotation) as GameObject;
                        shotTimer = 0.0f;
                    }

                    else if (patrolState == 1 && !burstFire)// Patrol state == 1 -> Ship is moving down, we rotate the instantiated bullet
                    {
                        Quaternion rotation = Quaternion.Euler(0, 0, 180);
                        newProjectileEnemy = Instantiate(projectileEnemy, this.transform.position, rotation) as GameObject;
                        shotTimer = 0.0f;
                    }

                    else if (patrolState == 0 && burstFire)
                    {
                        for (int i = 0; i < bursts; ++i)
                        {
                            newProjectileEnemy = Instantiate(projectileEnemy, this.transform.position, this.transform.rotation) as GameObject;
                        }
                        shotTimer = 0.0f;
                    }
                    else if (patrolState == 1 && burstFire)
                    {
                        Quaternion rotation = Quaternion.Euler(0, 0, 180);
                        for (int i = 0; i < bursts; ++i)
                        {
                            newProjectileEnemy = Instantiate(projectileEnemy, this.transform.position, rotation) as GameObject;
                        }
                        shotTimer = 0.0f;
                    }
                }
            } 
            
            else if (patrolMode_LeftRight)
            {
                if (nonStopShooting)
                {
                    Quaternion rotation = Quaternion.Euler(0, 0, 180);
                    newProjectileEnemy = Instantiate(projectileEnemy, this.transform.position, rotation) as GameObject;
                }

                if (shotTimer >= cooldown)
                {
                    if (!burstFire)
                    {
                        Quaternion rotation = Quaternion.Euler(0, 0, 180);
                        newProjectileEnemy = Instantiate(projectileEnemy, this.transform.position, rotation) as GameObject;
                        shotTimer = 0.0f;
                    }


                    else if (burstFire)
                    {
                        
                        Quaternion rotation = Quaternion.Euler(0, 0, 180);
                        for (int i = 0; i < bursts; ++i)
                        {
                            newProjectileEnemy = Instantiate(projectileEnemy, this.transform.position, rotation) as GameObject;
                        }
                        shotTimer = 0.0f;
                    }
                }
            }
        }
    }
}
