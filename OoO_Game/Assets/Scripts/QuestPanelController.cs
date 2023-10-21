using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestPanelController : MonoBehaviour
{
    public List<string> questDescriptions; //index refers to quest number

    private TextMeshProUGUI noneActive;
    private GameObject questPanelOne;
    private GameObject questPanelTwo;
    private TextMeshProUGUI questTextOne;
    private TextMeshProUGUI questTextTwo;

    //checkboxes show whether quest is completed
    private GameObject checkmarkOne;
    private GameObject checkmarkTwo;

    private bool questOnePopulated = false;
    private bool questTwoPopulated = false;

    private string questOneId = null;
    private string questTwoId = null;
    private bool unpopulated //true if no quests active
    {
        get
        {
            return (questOnePopulated == false && questTwoPopulated == false);
        }
    }


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

        questOnePopulated = false;
        questTwoPopulated = false;
    }
    
    public void OnQuestStepStarted(Component sender, object data)
    {
        if(data is string && sender is QuestStep) //make sure data is the quest step description
        {
            QuestStep questStep = (QuestStep)sender;

            string questId = (string)data;

            string stepDescription = questStep.description; 

            if (unpopulated)
            {
                noneActive.gameObject.SetActive(false);
                questOneId = questId;
                questTextOne.text = stepDescription;
                questOnePopulated = true;
            }
            else if (!questOnePopulated)
            {
                questTextOne.text = stepDescription;
                questOneId = questId;
                questPanelOne.SetActive(true);
                questOnePopulated = true;

            }
            else if (!questTwoPopulated)
            {
                questTextTwo.text = stepDescription;
                questTwoId = questId;
                questPanelTwo.SetActive(true);
                questTwoPopulated = true;
            }
            else
            {
                Debug.LogWarning("Started a quest step but both quest panels are populated.");
            }
        }
    }

    public void OnQuestStepCompleted(Component sender, object data)
    {
        if(data is string)
        {
            string questId = (string)data;

        }
    }


}
