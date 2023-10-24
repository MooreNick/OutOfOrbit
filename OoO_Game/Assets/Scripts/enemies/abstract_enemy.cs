using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class abstract_enemy : MonoBehaviour
{
    public int health;
    public int speed;
    public Vector3 velocity;
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

        if (health <= 0)
            Death();
    }

    private void Patrol()
    {
        if (patrolTimer <= 30 && patrolState == 0)
        {
            transform.Translate(velocity * speed * Time.deltaTime);
            patrolTimer++;
            if (patrolTimer >= 30)
            {
                //transform.Rotate(-velocity);
                patrolState = 1;
            }
                
        }
        else
        {
            transform.Translate(-velocity * speed * Time.deltaTime); ;
            patrolTimer--;
            if (patrolTimer <= -30)
            {
                //transform.Rotate(velocity);
                patrolState = 0;
            }
                
        }
    }

    public void Death()
    {
        Destroy(this.gameObject);
    }
}
