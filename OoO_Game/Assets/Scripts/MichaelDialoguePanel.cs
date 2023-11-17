using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class MichaelDialoguePanel : MonoBehaviour
{
    public GameObject michaelTriggerZone;

    [SerializeField]
    private GameEvent leaveCutscene;

    private TextMeshProUGUI dialogueTM;
    public float secsBetweenCharsTyped;

    private List<string> linesToSay = new List<string>();
    private List<string> linesPrevSaid = new List<string>();
    private List<string> linesDoNothing = new List<string>();

    private bool skippedDialogue = false;

    //called when script is loaded (at game start)
    private void Awake()
    {
        gameObject.SetActive(false);

        dialogueTM = GetComponentInChildren<TextMeshProUGUI>();

        linesDoNothing.Add("Do you need something?");
    }

    private void Start()
    {
    }
    
    //runs every frame while dialogue panel is active
    private void Update()
    {
        //check for mouse left click for skip dialogue in typeDialogue
        skippedDialogue = (Input.GetMouseButton(0)) ? true : false;
    } 

    public void OnQuestStepStarted(Component sender, object data)
    {
        if(data is List<string>)
        {
            linesPrevSaid = linesToSay; //save previously said lines
            updateDialogue((List<string>)data); //update linesToSay
            startDialogue();
        }
        else
        {
            Debug.Log("data was not string inside OnQuestStepStarted");
        }
    }

    public void OnNpcNothingToDo(Component sender, object data)
    {
        linesToSay = linesDoNothing;
        startDialogue();
    }

    public void OnQuestFinalized(Component sender, object data)
    {
        Debug.Log("inside questfinalized");
        if(data is List<string>)
        {
            List<string> finalLines = (List<string>)data;
            updateDialogue(finalLines);
            startDialogue(); //ending quest dialogue
        }
    }

    private void updateDialogue(List<string> newLines)
    {
        linesToSay = newLines;
    }

    public void startDialogue()
    {
        StartCoroutine(typeDialogue());
    }

    IEnumerator typeDialogue()
    {
        if(dialogueTM != null)
        {
            foreach (string lineOfDialogue in linesToSay)
            {
                skippedDialogue = false;
                dialogueTM.text = "";
                for (int i = 0; i < lineOfDialogue.Length; ++i)
                {
                    if (!skippedDialogue)
                    {
                        dialogueTM.text += lineOfDialogue[i];
                        yield return new WaitForSeconds(secsBetweenCharsTyped);
                    }
                    else // finish the dialogue instantly instead of letter by letter
                    {
                        dialogueTM.text += lineOfDialogue.Substring(i);
                        skippedDialogue = false; //reset skipped
                        break;
                    }
                }
                //wait for user to left click before continuing to next dialgoue
                while (!Input.GetMouseButtonDown(0))
                {
                    yield return null; //wait for next frame
                }
            }
        }
        dialogueTM.text = "";

        leaveCutscene.Raise();

        gameObject.SetActive(false); //make michael dialogue panel not show/run
    } 
    
}
