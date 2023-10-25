using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public float health = 100;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "enemyProjectile")
        {
            health -= 10;
        }
    }

    void Death()
    {
        if (health <= 0)
            Destroy(gameObject);
    }

    void FixedUpdate()
    {
        Death();
    }
}
