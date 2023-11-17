using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LeaveBaseZone : MonoBehaviour
{
    public string sceneName;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            SceneManager.LoadScene(sceneName);
        }
    }

    public void changeSceneToLoad(string newSceneName)
    {
        sceneName = newSceneName;
    }

}
