using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestElement : MonoBehaviour
{
    public Quest quest;

    public void TriggerQuest()
    {
        FindObjectOfType<QuestManager>().StartQuest(quest);
    }
}
