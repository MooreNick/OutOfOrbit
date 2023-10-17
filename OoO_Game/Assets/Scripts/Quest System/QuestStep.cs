using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class QuestStep : MonoBehaviour
{
    public GameEvent questStepCompleted;

    private bool isFinished = false;

    private string questId;

    public void InitializeQuestStep(string questId)
    {
        this.questId = questId;
    }

    protected void FinishQuestStep()
    {
        if (!isFinished)
        {
            isFinished = true;

            questStepCompleted.Raise(questId);

            Destroy(this.gameObject);
        }
    }
}
