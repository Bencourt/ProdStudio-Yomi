using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public Quest activeQuest;
    public DialogManager dialogManager;
    public GameObject Player;
    public int questProgression;

    private Queue<Quest> questQueue;

    public Text activeQuestText;

    private GameObject[] inventory;

    // Start is called before the first frame update
    void Start()
    {
        Quest startingQuest = new Quest(0, "Head into town and speak to Chef John", GameObject.Find("Chef John"));
        inventory = Player.GetComponent<Inventory>().Slot;
        questProgression = 0;
        questQueue = new Queue<Quest>();
        StartQuest(startingQuest);
        Debug.Log(activeQuest.NPC.GetComponent<DialogElement>().dialogElements[activeQuest.NPC.GetComponent<DialogElement>().progression].name);
    }

    // Update is called once per frame
    void Update()
    {
        if (activeQuest != null)
        {
            activeQuestText.text = activeQuest.description;

            switch (activeQuest.type)
            {
                case questType.item:
                    if (inventory.Length > 0)
                    {
                        foreach (GameObject slot in inventory)
                        {
                            if (activeQuest.itemID == slot.GetComponent<Slot>().ID)
                            {
                                Debug.Log("Item Quest Completed");
                                CompleteQuest();
                            }
                        }
                    }
                    break;

                case questType.dialog:
                    if(dialogManager.nameText.text == activeQuest.NPC.GetComponent<DialogElement>().dialogElements[activeQuest.NPC.GetComponent<DialogElement>().progression].name)
                    {
                        Debug.Log("Dialog Quest Completed");
                        CompleteQuest();
                    }
                    break;
            }
        }
        else
        {
            activeQuestText.text = "No Active Quest";
        }
    }

    public void StartQuest(Quest quest)
    {
        if (quest.questOrder == questProgression)
        {
            Debug.Log("New Quest Started: " + quest.description);
            activeQuest = quest;
        }
        if (questQueue.Count > 0)
        {
            if (quest.questOrder == questProgression + 1 && questQueue.Peek() != quest)
            {
                Debug.Log("Added next quest to Queue");
                questQueue.Enqueue(quest);
            }
        }
        else
        {
            if (quest.questOrder == questProgression + 1)
            {
                Debug.Log("Added next quest to Queue");
                questQueue.Enqueue(quest);
            }
        }
    }

    public void CompleteQuest()
    {
        activeQuest.NPC.GetComponent<DialogElement>().progression++;
        activeQuest = null;
        questProgression++;
        if(questQueue.Count > 0)
        {
            StartQuest(questQueue.Dequeue());
        }
    }
}
