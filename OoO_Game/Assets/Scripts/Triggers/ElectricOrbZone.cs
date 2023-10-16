using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricOrbZone : MonoBehaviour
{
    public GameEvent electricOrbTriggered;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            electricOrbTriggered.Raise(this);
        }
    }
}
