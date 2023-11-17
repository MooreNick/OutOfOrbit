using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public static PlayerInfo playerInstance;

    public GameEvent playerLevelChanged;

    private int playerLevel;

    private void Awake()
    {
        if(playerInstance == null)
        {
            playerInstance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

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

    public void onQuestFinalized(Component sender, object data)
    {
        changeLevel(playerLevel + 1);
    }
}
