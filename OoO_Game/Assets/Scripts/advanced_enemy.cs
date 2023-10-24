using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class advanced_enemy : MonoBehaviour
{

    public int health = 300;
    public int speed = 1;

    private int patrolTimer = 0;
    private Vector3 velocity;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            health -= 100;
        }
    }

    private void FixedUpdate()
    {
        if (health <= 0)
            Death();
    }

    void Patrol()
    {
        

    }

    void Death()
    {
            Destroy(this.gameObject);
    }
}
