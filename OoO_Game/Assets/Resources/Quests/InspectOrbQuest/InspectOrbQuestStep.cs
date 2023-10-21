using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspectOrbQuestStep : QuestStep
{
    //gets called when player enters orbZoneTrigger
    public void hasInspected(Component sender, object data)
    {
        sender.gameObject.SetActive(false); //deactivate zone gameobject
        FinishQuestStep();
    }
}
