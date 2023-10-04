using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MichaelDialoguePanel : MonoBehaviour
{
    private void Start()
    {
        gameObject.SetActive(false); //not showing on game start
    }

    public void toggleActive()
    {
        bool currentState = gameObject.activeSelf;
        gameObject.SetActive(!currentState);
    } 
}
