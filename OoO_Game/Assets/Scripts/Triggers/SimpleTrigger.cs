using UnityEngine;
using UnityEngine.Events;

public class SimpleTrigger : MonoBehaviour
{

    public GameEvent npcZoneTriggered;
    public UnityEvent onTriggerEnter;
    public GameObject michaelDialoguePanel;


    void OnTriggerEnter2D(Collider2D other)
    {
        //set dialogue panel active so can listen for gameevents
        if (michaelDialoguePanel != null) michaelDialoguePanel.SetActive(true); 
        if (other.tag == "Player")
        {
            npcZoneTriggered.Raise();
            onTriggerEnter.Invoke(); //invoke unityevent
        }
    }

}