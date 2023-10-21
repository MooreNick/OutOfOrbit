using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    public GameEvent assignAQuest;

    private List<string> canStartQuestIds = new List<string>();

    public void onQuestBecomeReady(Component sender, object data)
    {
        if(data is string)
        {
            string newCanStartQuestId = data.ToString();
            if (!canStartQuestIds.Contains(newCanStartQuestId)) //if quest not already in list
            {
                canStartQuestIds.Add(newCanStartQuestId);
            }
        }
    }

    public void onNpcZoneTriggered(Component sender, object data)
    {
        TryAssignQuest();
    }

    private void TryAssignQuest()
    {
        if (canStartQuestIds.Count > 0) //a quest exists so can be assigned
        {

        }
        else //no quests to assign
        {

        }
    }
}
