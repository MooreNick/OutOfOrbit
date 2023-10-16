using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    private Dictionary<string, Quest> questMap;

    private void Awake()
    {
        questMap = CreateQuestMap();

        Quest quest = GetQuestById("InspectOrbQuest");
        Debug.Log(quest.info.displayName);
    }

    private void StartQuest(string id)
    {
        //TODO: start the quest
    }

    private void AdvanceQuest(string id)
    {
        //TODO: advance the quest
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
            newQuestMap.Add(questInfo.id, new Quest(questInfo));
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
