using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MichaelDialoguePanel : MonoBehaviour
{
    private TextMeshProUGUI dialogueTM;
    public float secsBetweenCharsTyped;
    public string michaelGreeting;

    private void Start()
    {
        dialogueTM = GetComponentInChildren<TextMeshProUGUI>();
        gameObject.SetActive(false); //not showing on game start
    }

    public void startDialogue()
    {
        StartCoroutine(typeDialogue(michaelGreeting));
    }

    IEnumerator typeDialogue(string lineOfDialogue)
    {
        if(dialogueTM != null)
        {
            foreach(char c in lineOfDialogue)
            {
                dialogueTM.text += c;
                yield return new WaitForSeconds(secsBetweenCharsTyped);
            }
        }
    }

    public void toggleActive()
    {
        bool currentState = gameObject.activeSelf;
        gameObject.SetActive(!currentState);
    } 
}
