using UnityEngine;
using UnityEngine.Events;

public class SimpleTrigger : MonoBehaviour
{

    public GameEvent npcZoneTriggered;
    public UnityEvent onTriggerEnter;

    [SerializeField]
    private GameObject michaelDialoguePanel;

    private void Start()
    {
        GameObject canvas = GameObject.FindGameObjectWithTag("Canvas");

        if(canvas != null)
        {
            michaelDialoguePanel = canvas.transform.Find("Michael Dialogue Panel").gameObject;
        }
        else
        {
            Debug.Log("npc zone couldnt find canvas inside simple trigger script.");
        }
    }


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