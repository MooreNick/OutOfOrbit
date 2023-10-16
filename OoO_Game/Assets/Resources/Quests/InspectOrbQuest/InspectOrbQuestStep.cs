using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspectOrbQuestStep : QuestStep
{
    private bool inspectedOrb = false;

    //gets called when player enters orbZoneTrigger
    public void hasInspected(Component sender, object data)
    {
        inspectedOrb = true;
        sender.gameObject.SetActive(false); //deactivate zone gameobject
        FinishQuestStep();
    }
}
