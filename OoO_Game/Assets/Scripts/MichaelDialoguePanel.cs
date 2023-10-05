using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MichaelDialoguePanel : MonoBehaviour
{
    private TextMeshProUGUI dialogueTM;
    public float secsBetweenCharsTyped;
    public string michaelGreeting;

    private bool skippedDialogue = false;

    private void Start()
    {
        dialogueTM = GetComponentInChildren<TextMeshProUGUI>();
        gameObject.SetActive(false); //not showing on game start
    }
    
    //runs every frame while dialogue panel is active
    private void Update()
    {
        //check for mouse left click for skip dialogue in typeDialogue
        skippedDialogue = (Input.GetMouseButton(0)) ? true : false;
    }

    public void startDialogue()
    {
        StartCoroutine(typeDialogue(michaelGreeting));
    }

    IEnumerator typeDialogue(string lineOfDialogue)
    {
        if(dialogueTM != null)
        {
            dialogueTM.text = "";
            for(int i = 0; i < lineOfDialogue.Length; ++i)
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
        }
    }

    public void toggleActive()
    {
        bool currentState = gameObject.activeSelf;
        gameObject.SetActive(!currentState);
    } 
}
