using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class tutorial_text_manager : MonoBehaviour
{
    // Each prompt
    public TextMeshProUGUI movePrompt;
    public TextMeshProUGUI shootPrompt;
    public TextMeshProUGUI destroyPrompt;
    public TextMeshProUGUI proceedPrompt;

    // Player for x location
    // Enemy for death status
    public GameObject player; 
    public GameObject enemy;

    // For x location to load new level
    public GameObject triggerLocation;

    //  Tasks completed
    bool movedRight = false;
    bool shot = false;
    bool killedEnemy = false;
    bool proceeded = false;

    // Tutorial skip
    bool escExit = false;

    // init value
    float startingPlayerX = 0;
    // to change
    float updatePlayerX = 0;
    // init
    float triggerLocationX;

    // To reset the position when loading the starting base
    Vector3 resetPosition = new Vector3(2.69f, -.59f, 0f);

    void Awake()
    {
        // set prompts
        movePrompt.enabled = true;
        shootPrompt.enabled = false;
        destroyPrompt.enabled = false;
        proceedPrompt.enabled = false;  

        // get inits
        startingPlayerX = player.gameObject.transform.position.x;
        triggerLocationX = triggerLocation.gameObject.transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        // update player location
        updatePlayerX = player.gameObject.transform.position.x;

        // if moved
        if (Mathf.Abs(updatePlayerX - startingPlayerX) >= 2.0)
        {
            movedRight = true;
        }
        // switch prompts
        if (movedRight)
        {
            movePrompt.enabled = false;
            shootPrompt.enabled = true;
        }
        // check for shooting
        if (Input.GetKeyDown(KeyCode.Space))
        {
            shot = true;
        }
        // switch prompts
        if (shot)
        {
            shootPrompt.enabled = false;
            destroyPrompt.enabled = true;
        }
        // check if enemy destroyed
        if (enemy.gameObject == null)
        {
            killedEnemy = true;
        }
        // switch prompts
        if (killedEnemy)
        {
            destroyPrompt.enabled = false;
            proceedPrompt.enabled = true;
        }
        // moved to exit
        if (updatePlayerX > triggerLocationX)
        {
            proceeded = true;
        }
        // load new scene
        if (proceeded)
        {
            SceneManager.LoadScene("StartingBase");
            player.gameObject.transform.position = resetPosition;
        }
        // pressed escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            escExit = true;
        }
        // load new scene
        if (escExit)
        {
            SceneManager.LoadScene("StartingBase");
            player.gameObject.transform.position = resetPosition;
        }
    }

}
