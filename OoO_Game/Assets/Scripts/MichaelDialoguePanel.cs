using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class MichaelDialoguePanel : MonoBehaviour
{
    public GameObject michaelTriggerZone;
    public UnityEvent onLeaveCutscene;

    private TextMeshProUGUI dialogueTM;
    public float secsBetweenCharsTyped;

    public List<string> firstEncounterLines;
    private List<string> linesToSay = new List<string>();

    private bool skippedDialogue = false;

    //called when script is loaded (at game start)
    private void Awake()
    {
        gameObject.SetActive(false); //not showing on game start


        dialogueTM = GetComponentInChildren<TextMeshProUGUI>();

       // load up the starting lines into linesToSay
       foreach(string line in firstEncounterLines)
        {
            linesToSay.Add(line);
        } 
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
        onLeaveCutscene.Invoke(); // do actions to make cutscene end
        michaelTriggerZone.SetActive(false);
        gameObject.SetActive(false); //make michael dialogue panel not show/run
    } 
    
}
