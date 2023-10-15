using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspectOrbQuestStep : QuestStep
{
    public GameObject orbZoneTrigger;

    private bool inspectedOrb = false;

    //setup needed stuff for this quest step
    protected override void StepSpecificInitialization()
    {
        orbZoneTrigger.SetActive(true);
    }

    //gets called when player enters orbZoneTrigger
    public void hasInspected()
    {
        inspectedOrb = true;
        orbZoneTrigger.SetActive(false); //deactivate zone gameobject
        FinishQuestStep();
    }
}
