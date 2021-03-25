﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public bool inventoryEnabled;
    public GameObject inventory;
    public GameObject slotHolder;

    public GameObject itemButton;

    private int allSlots;
    private int enabledSlots;
    private GameObject[] slot;

    void Start()
    {
        allSlots = 16;
        slot = new GameObject[allSlots];
        for(int i = 0; i < allSlots; i++)
        {
            slot[i] = slotHolder.transform.GetChild(i).gameObject;

            if (slot[i].GetComponent<Slot>().item == null)
                slot[i].GetComponent<Slot>().empty = true;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            inventoryEnabled = !inventoryEnabled;

        if(inventoryEnabled)
        {
            inventory.SetActive(true);
        }
        else
        {
            inventory.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Item")
        {
            GameObject itemPickedUp = other.gameObject;
            Item item = itemPickedUp.GetComponent<Item>();
            AddItem(itemPickedUp, item.ID, item.type, item.desc, item.icon);
        }
    }

    void AddItem(GameObject itemObject, int itemID, string itemType, string itemDesc, Sprite itemIcon)
    {
        for (int i = 0; i < allSlots; i++)
        {
            if(slot[i].GetComponent<Slot>().empty)
            {
                itemObject.GetComponent<Item>().pickedUp = true;

                slot[i].GetComponent<Slot>().icon = itemIcon;
                slot[i].GetComponent<Slot>().type = itemType;
                slot[i].GetComponent<Slot>().ID = itemID;
                slot[i].GetComponent<Slot>().desc = itemDesc;
                slot[i].GetComponent<Slot>().item = itemObject;

                itemObject.transform.parent = slot[i].transform;
                itemObject.SetActive(false);

                slot[i].GetComponent<Slot>().UpdateSlot();
                slot[i].GetComponent<Slot>().empty = false;

                GameObject tempButton = Instantiate(itemButton, slot[i].transform, false);
                tempButton.tag = "ItemButton";

                break;
            }
        }
    }
}
