using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public GameEvent playerLevelChanged;

    private int playerLevel;

    private void Start()
    {
        playerLevel = 0;
        playerLevelChanged.Raise(playerLevel);
    }

    private void changeLevel(int newLevel)
    {
        playerLevel = newLevel;
        playerLevelChanged.Raise(playerLevel);
    }

    public void onQuestCompleted(Component sender, object data)
    {
        changeLevel(playerLevel + 1);
    }
}
