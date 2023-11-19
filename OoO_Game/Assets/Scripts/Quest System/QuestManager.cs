using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public GameEvent questBecomeReady;
    public GameEvent questCanFinish;
    public GameEvent questFinalized;
    [SerializeField]
    private GameEvent sendQuestIds;

    private Dictionary<string, Quest> questMap;

    private int currentPlayerLevel = 0;

    private void Awake()
    {
        questMap = CreateQuestMap(); 
    }

    public void OnNpcLoaded(Component sender, object data)
    {
        sendQuestIds.Raise(this); 
    }

    public void onPlayerLevelChanged(Component sender, object data)
    {
        if (data is int) currentPlayerLevel = (int)data; //update the current player level
        Debug.Log("player level changed to " + currentPlayerLevel);
        //check if any quests meet can now be started at the new level and set them as CAN_START
        foreach(Quest quest in questMap.Values)
        {
            if (quest.info.playerLevelRequired <= currentPlayerLevel && quest.state == QuestState.REQUIREMENTS_NOT_MET)
            {
                ChangeQuestState(quest.info.id, QuestState.CAN_START);
                questBecomeReady.Raise(quest.info.id); //sends id of newly ready quest to listeners
            }
        }
    }

    private void ChangeQuestState(string id, QuestState newState)
    {
        Quest questToChange = GetQuestById(id);
        questToChange.state = newState;

        if(newState == QuestState.CAN_FINISH)
        {
            questCanFinish.Raise(questToChange.info.id);
        }
    }

    private void StartQuest(string id)
    {
        Quest quest = GetQuestById(id);
        quest.InstantiateCurrentQuestStep(this.transform);
        ChangeQuestState(quest.info.id, QuestState.IN_PROGRESS); 
    }

    public void OnAssignAQuest(Component sender, object data)
    {
        if(data is string)
        {
            string questId = data.ToString();
            StartQuest(questId);
        }
    }

    public void OnQuestFinished(Component sender, object data)
    {
        if(data is string)
        {
            string questId = (string)data;
            FinishQuest(questId);
        }
    }


    public void onQuestStepTurnedIn(Component sender, object data)
    {
        if(data is string)
        {
            AdvanceQuest(data.ToString());
        }
    }

    public void OnQuestStepCompleted(Component sender, object data)
    {
        if(data is string)
        {
            string questId = (string)data;

            ChangeQuestState(questId, QuestState.STEP_DONE);
        }
    }

    private void AdvanceQuest(string id)
    {
        Quest quest = GetQuestById(id);

        quest.MoveToNextStep();

        if (quest.CurrentStepExists())
        {
            ChangeQuestState(id, QuestState.IN_PROGRESS); // STEP_DONE -> IN_PROGRESS

            quest.InstantiateCurrentQuestStep(this.transform);
        }
        else //all quest steps completed
        {
            ChangeQuestState(quest.info.id, QuestState.CAN_FINISH);
        }
    }

    private void FinishQuest(string id)
    {
        ChangeQuestState(id, QuestState.FINISHED);

        //for giving quest finished lines to dialogue panel and player level
        Quest quest = GetQuestById(id);
        questFinalized.Raise(quest.info.finishedLines);

        //TODO: give player quest rewards
    }

    private Dictionary<string, Quest> CreateQuestMap()
    {
        //Loads all QuestStatic scriptable objects under Assets/Resources/Quests folder
        QuestStatic[] allQuests = Resources.LoadAll<QuestStatic>("Quests");

        Dictionary<string, Quest> newQuestMap = new Dictionary<string, Quest>();
        foreach(QuestStatic questInfo in allQuests)
        {
            if (newQuestMap.ContainsKey(questInfo.id)) //check for duplicate quest id
            {
                Debug.LogWarning("WARNING: theres two quests with the same quest id");
            }
            newQuestMap.Add(questInfo.id, new Quest(questInfo)); //calls Quest constructor 
        }
        return newQuestMap;
    }

    private Quest GetQuestById(string id)
    {
        Quest questRequested = questMap[id];
        if(questRequested == null)
        {
            Debug.LogError("Quest with id not found: " + id);
        }
        return questRequested;
    }

    public List<string> GetQuestIdsWithState(QuestState stateToFind) 
    {
        List<string> questIds = new List<string>();

        foreach(var questPair in questMap)
        {
            if(questPair.Value.state == stateToFind)
            {
                questIds.Add(questPair.Key);
            }
        }

        return questIds;
    }
}
