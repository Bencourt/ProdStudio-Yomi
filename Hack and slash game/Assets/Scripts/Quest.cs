using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum questType
{
    item = 0,
    dialog
}

[System.Serializable]
public class Quest
{
    public questType type;
    public int questOrder;
    public string description;
    public int itemID;
    public GameObject NPC;
    public Quest(int questOrder, string desc, GameObject NPC)
    {
        type = questType.dialog;
        this.questOrder = questOrder;
        this.description = desc;
        this.NPC = NPC;
    }

    public Quest(int questOrder, string desc, int itemID)
    {
        this.questOrder = questOrder;
        type = questType.item;
        this.description = desc;
        this.itemID = itemID;
    }
}
