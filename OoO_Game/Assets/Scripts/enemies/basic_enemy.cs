using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basic_enemy : abstract_enemy
{
    private void Start()
    {
        health = 100;
        speed = 5;
        velocity = new Vector3(0.0f, 1.0f, 0.0f);
    }
}
