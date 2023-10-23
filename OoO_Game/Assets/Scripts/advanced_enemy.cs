using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class advanced_enemy : MonoBehaviour
{

    public int health = 300;
    public int speed = 1;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            health -= 100;
        }
    }

    private void FixedUpdate()
    {
        
    }

    void Death()
    {
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
