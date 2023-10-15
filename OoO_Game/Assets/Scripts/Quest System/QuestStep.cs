using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class QuestStep : MonoBehaviour
{
    public string description;

    protected bool active = false;

    private bool isFinished = false;

    private void Start()
    {
        active = true;
        StepSpecificInitialization();
    }

    //overidden in derived for enabling specific step functionality
    protected abstract void StepSpecificInitialization();

    protected void FinishQuestStep()
    {
        if (!isFinished)
        {
            isFinished = true;

            //TODO: go to next quest step

            Destroy(this.gameObject);
        }
    }

    public bool IsActive()
    {
        return active;
    }

}
