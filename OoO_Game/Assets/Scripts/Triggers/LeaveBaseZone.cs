using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LeaveBaseZone : MonoBehaviour
{
    [SerializeField]
    private string toScene;
    [SerializeField]
    private string connectingPortal;
    [SerializeField]
    private float spawnOffsetY;
    [SerializeField]
    private float spawnOffsetX;
    [SerializeField]
    private float zRotateOffset;

    private void Start()
    {
        //if prev portal connects to this portal then spawn player near this portal
        if(PlayerPrefs.GetString("LastUsedPortal") == connectingPortal)
        {
            //player position
            PlayerInfo.playerInstance.transform.position =
                new Vector3(transform.position.x + spawnOffsetX, transform.position.y + spawnOffsetY);

            //player rotation
            PlayerInfo.playerInstance.transform.eulerAngles = 
                new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z + zRotateOffset);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            //save portal name so can correctly place player in next scene
            PlayerPrefs.SetString("LastUsedPortal", gameObject.name);

            SceneManager.LoadScene(toScene);
        }
    }

    public void changeSceneToLoad(string newSceneName)
    {
        toScene = newSceneName;
    }

}
