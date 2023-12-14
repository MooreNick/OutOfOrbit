using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillEnemiesQuestStep : QuestStep
{
    private int numberOfEnemiesKilled = 0;

    public void OnEnemyKilled(Component sender, object data)
    {
        ++numberOfEnemiesKilled;
        if(numberOfEnemiesKilled >= 5)
        {
            FinishQuestStep();
        }
    }
}
