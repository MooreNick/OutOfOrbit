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

        noneActive = transform.Find("None Active Text").GetComponent<TextMeshProUGUI>();

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
        if(data is string && sender is QuestStep) //make sure data is the questid
        {
            QuestStep questStep = (QuestStep)sender;

            string questId = (string)data;

            string stepDescription = questStep.description; 

            if (unpopulated) //no quests active
            {
                noneActive.gameObject.SetActive(false);
                questOneId = questId;
                questTextOne.text = stepDescription;
                questPanelOne.SetActive(true);
                checkmarkOne.SetActive(false);
                questOnePopulated = true;
            }
            else if (!questOnePopulated) //two active, one not
            {
                questTextOne.text = stepDescription;
                questOneId = questId;
                questPanelOne.SetActive(true);
                checkmarkOne.SetActive(false);
                questOnePopulated = true;

            }
            else if (!questTwoPopulated) //one active, two not
            {
                questTextTwo.text = stepDescription;
                questTwoId = questId;
                questPanelTwo.SetActive(true);
                checkmarkTwo.SetActive(false);
                questTwoPopulated = true;
            }
            else
            {
                Debug.LogWarning("Started a quest step but both quest panels are populated.");
            }
        }
    }

    public void OnQuestStepTurnedIn(Component sender, object data)
    {
        if(data is string) //make sure is questid string
        {
            string questId = (string)data;
            if(questId == questOneId)
            {
                //clean up quest one panel
                questTextOne.text = "";
                checkmarkOne.SetActive(false);
                questPanelOne.SetActive(false);

                //reset some data for quest one
                questOneId = null;
                questOnePopulated = false;
            }
            else if(questId == questTwoId)
            {
                //clean up quest two panel
                questTextTwo.text = "";
                checkmarkTwo.SetActive(false);
                questPanelTwo.SetActive(false);

                //reset some data for quest one
                questTwoId = null;
                questTwoPopulated = false;
            }
            else
            {
                Debug.LogWarning("Quest turned in didnt match the active quest IDs.");
            }
   
            //if no quests active then activate none active text
            if (unpopulated)
            {
                noneActive.gameObject.SetActive(true);
            }
        }
    }

    public void OnQuestStepCompleted(Component sender, object data)
    {
        Debug.Log("inside questpanelcontroller.onqueststepcompleted");
        if (checkmarkOne == null) Debug.Log("checkmarkone was null");
        if(data is string)
        {
            string questId = (string)data;
            //show checkmark to signify can be turned in
            if(questId == questOneId)
            {
                checkmarkOne.SetActive(true);
            }
            else if(questId == questTwoId)
            {
                checkmarkTwo.SetActive(true);
            }
        }
    }


}
