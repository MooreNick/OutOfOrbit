using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class QuestStep : MonoBehaviour
{
    public GameEvent questStepCompleted;
    public GameEvent questStepStarted;

    public string description;
    public List<string> stepDialogueLines;

    private bool isFinished = false;

    private string questId;

    public void InitializeQuestStep(string questId)
    {
        this.questId = questId;
        questStepStarted.Raise(stepDialogueLines); // for dialogue panel
        questStepStarted.Raise(this, questId); // for quest panel
    }

    protected void FinishQuestStep()
    {
        if (!isFinished)
        {
            isFinished = true;

            questStepCompleted.Raise(this, questId);

            Destroy(this.gameObject);
        }
    }
}
