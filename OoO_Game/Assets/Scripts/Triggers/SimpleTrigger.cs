using UnityEngine;
using UnityEngine.Events;

public class SimpleTrigger : MonoBehaviour
{

    public GameEvent npcZoneTriggered;
    public UnityEvent onTriggerEnter;


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            npcZoneTriggered.Raise();
            onTriggerEnter.Invoke(); //invoke unityevent
        }
    }

}