using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LeaveBaseZone : MonoBehaviour
{
    public Collider2D player;
    public string sceneName;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision == player)
        {
            SceneManager.LoadScene(sceneName);
        }
    }

    public void changeSceneToLoad(string newSceneName)
    {
        sceneName = newSceneName;
    }

}
