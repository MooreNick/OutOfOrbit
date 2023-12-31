using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    [SerializeField]
    private GameEvent npcLoaded;
    public GameEvent assignAQuest;
    public GameEvent questFinished;
    public GameEvent questStepTurnedIn;
    public GameEvent npcDoNothing;

    private List<string> canStartQuestIds = new List<string>();
    private List<string> questStepCanBeTurnedInIds = new List<string>();
    private List<string> canFinishQuestIds = new List<string>();
    private int questsActive = 0;

    private void Start()
    {
        npcLoaded.Raise(); //notify quest manager to send quest ids for init lists
    }

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

    public void OnSendQuestIds(Component sender, object data)
    {
        QuestManager qMan = sender as QuestManager;

        if(qMan != null)
        {
            canStartQuestIds = qMan.GetQuestIdsWithState(QuestState.CAN_START);
            questStepCanBeTurnedInIds = qMan.GetQuestIdsWithState(QuestState.STEP_DONE);
            canFinishQuestIds = qMan.GetQuestIdsWithState(QuestState.CAN_FINISH);
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

    public void OnQuestStepCompleted(Component sender, object data)
    {
        if(data is string)
        {
            string questId = (string)data;
            questStepCanBeTurnedInIds.Add(questId);
        }
    }

    public void onNpcZoneTriggered(Component sender, object data)
    { 
        if (questStepCanBeTurnedInIds.Count > 0)
        {
            //turn in quest step
            string questIdTurnedIn = questStepCanBeTurnedInIds[0];
            questStepCanBeTurnedInIds.Remove(questIdTurnedIn);
            questStepTurnedIn.Raise(questIdTurnedIn);
            
            //check if quest can be finished
            if (canFinishQuestIds.Contains(questIdTurnedIn))
            {
                canFinishQuestIds.Remove(questIdTurnedIn);
                questFinished.Raise(questIdTurnedIn); //notify listeners quest has finished
                --questsActive;
            }
            
        }
        else if(canStartQuestIds.Count > 0) 
        {
            TryAssignQuest();
        }
        else //nothing to do
        {
            npcDoNothing.Raise();
        }
    }

    private void TryAssignQuest()
    {
        if (questsActive < 2) //a quest can be started and limit not exceeded
        {
            string questId = canStartQuestIds[0]; //get id of quest to assign
            canStartQuestIds.RemoveAt(0); //remove id so quest doesnt get assigned twice
            assignAQuest.Raise(questId); //tell listeners (quest manager) to start the quest
            ++questsActive;
        }
        else
        {
            Debug.Log("2 quests already active. QuestGiver.TryAssignQuest()");
        }
    }
}
