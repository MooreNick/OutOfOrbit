using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basic_enemy : abstract_enemy
{
    
    private void Start()
    {
        health = 100;
        speed = 5;

        if (this.patrolMode_UpDown)
        {
            velocity = new Vector3(0.0f, 1.0f, 0.0f);
        }
        else if (this.patrolMode_LeftRight)
        {
            velocity = new Vector3(1.0f, 0.0f, 0.0f);
        }
        else // shouldn't hit this
        {
            velocity = new Vector3(0.0f, 0.0f, 0.0f);
        }
    }

    public override void FlipSprite()
    {
        if (this.patrolMode_UpDown)
        {
            if (enemySprite.flipY == false)
                enemySprite.flipY = true;
            else
                enemySprite.flipY = false;
        }

        else if (this.patrolMode_LeftRight)
        {
            if (enemySprite.flipX == false)
                enemySprite.flipX = true;
            else
                enemySprite.flipX= false;
        }
    }
}
