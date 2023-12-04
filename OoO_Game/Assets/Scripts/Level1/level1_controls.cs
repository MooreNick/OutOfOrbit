using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class level1_controls : MonoBehaviour
{
    public GameObject player;
    public GameObject triggerLocation;

    float startingPlayerX;
    float triggerLocationX;

    float updatePlayerX;

    bool loadBase = false;

    // To reset the position when loading the starting base
    Vector3 resetPosition = new Vector3(2.69f, -.59f, 0f);

    void Awake()
    {
        startingPlayerX = player.gameObject.transform.position.x;
        triggerLocationX = triggerLocation.gameObject.transform.position.x;

    }

    // Update is called once per frame
    void Update()
    {
        updatePlayerX = player.gameObject.transform.position.x;

        if (updatePlayerX > triggerLocationX)
            loadBase = true;

        if (loadBase)
        {
            SceneManager.LoadScene("StartingBase");
            player.gameObject.transform.position = resetPosition;
        }
    }
}
