using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class advanced_enemy : abstract_enemy
{
    private void Start()
    {
        health = 300;
        speed = 5;
        velocity = new Vector3(1.0f, 0.0f, 0.0f);
    }
}
