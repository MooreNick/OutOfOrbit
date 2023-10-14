using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestPanelController : MonoBehaviour
{
    private TextMeshProUGUI noneActive;
    private GameObject questPanelOne;
    private GameObject questPanelTwo;
    private TextMeshProUGUI questTextOne;
    private TextMeshProUGUI questTextTwo;

    //checkboxes show whether quest is completed
    private GameObject checkmarkOne;
    private GameObject checkmarkTwo;


    //called when the script loads
    private void Awake()
    {
        //init activeQuestsPanel
        Transform activeQuestsPanel = transform.Find("Active Quests");

        //initialize the quest panel gameobjects
        questPanelOne = activeQuestsPanel.Find("Quest 1 Panel").gameObject;
        questPanelTwo = activeQuestsPanel.Find("Quest 2 Panel").gameObject;

        //init the quest texts
        questTextOne = questPanelOne.transform.Find("quest text").GetComponent<TextMeshProUGUI>();
        questTextTwo = questPanelTwo.transform.Find("quest text").GetComponent<TextMeshProUGUI>();

        //init checkmarks
        Transform parentOfCheckmarkOne = questPanelOne.transform.Find("checkbox");
        Transform parentOfCheckmarkTwo = questPanelTwo.transform.Find("checkbox");
        checkmarkOne = parentOfCheckmarkOne.Find("checkmark").gameObject;
        checkmarkTwo = parentOfCheckmarkTwo.Find("checkmark").gameObject;
    }


    void Start()
    {
        //set quest panels inactive since no quests at start of game
        questPanelOne.SetActive(false);
        questPanelTwo.SetActive(false);

        //set quests as uncompleted
        checkmarkOne.SetActive(false);
        checkmarkTwo.SetActive(false);
    }

    void Update()
    {
        
    }
}
