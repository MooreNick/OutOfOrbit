using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    public GameEvent assignAQuest;
    public GameEvent questFinished;

    private List<string> canStartQuestIds = new List<string>();
    private List<string> canFinishQuestIds = new List<string>();
    private int questsActive = 0;

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

    public void OnQuestCanFinish(Component sender, object data)
    {
        if (data is string) {
            string questId = data.ToString();
            if (!canFinishQuestIds.Contains(questId))
            {
                canFinishQuestIds.Add(questId);
            }
        }
    }

    public void onNpcZoneTriggered(Component sender, object data)
    {
        if (canFinishQuestIds.Count > 0) //if quest can be finished then finish it
        {
            string questId = canFinishQuestIds[0];
            canFinishQuestIds.RemoveAt(0);
            questFinished.Raise(questId);
            --questsActive;
        }
        else
        {
            TryAssignQuest();
        }
    }

    private void TryAssignQuest()
    {
        if (canStartQuestIds.Count > 0 && questsActive < 2) //a quest can be started and limit not exceeded
        {
            string questId = canStartQuestIds[0]; //get id of quest to assign
            canStartQuestIds.RemoveAt(0); //remove id so quest doesnt get assigned twice
            assignAQuest.Raise(questId); //tell listeners (quest manager) to start the quest
            ++questsActive;
        }
    }
}
