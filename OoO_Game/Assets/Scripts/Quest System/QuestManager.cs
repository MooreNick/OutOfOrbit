using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public GameEvent questBecomeReady;
    public GameEvent questCanFinish;

    private Dictionary<string, Quest> questMap;

    private int currentPlayerLevel = 0;

    private void Awake()
    {
        questMap = CreateQuestMap(); 
    }

    public void PlayerLevelChanged(Component sender, object data)
    {
        if (data is int) currentPlayerLevel = (int)data;
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


    public void onQuestStepCompleted(Component sender, object data)
    {
        if(data is string)
        {
            AdvanceQuest(data.ToString());
        }
    }

    private void AdvanceQuest(string id)
    {
        Quest quest = GetQuestById(id);

        if (quest.CurrentStepExists())
        {
            quest.InstantiateCurrentQuestStep(this.transform);
        }
        else
        {
            ChangeQuestState(quest.info.id, QuestState.CAN_FINISH);
        }
    }

    private void FinishQuest(string id)
    {
        //todo: finish the quest
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
}
