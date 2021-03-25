using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerClickHandler
{
    public bool empty;
    public Sprite icon;
    public string type;
    public int ID;
    public GameObject item;
    public string desc;

    public Sprite baseIcon;

    public Transform slotIconGO;

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        UseItem();
    }

    private void Start()
    {
        slotIconGO = transform.GetChild(0);
    }

    public void UpdateSlot()
    {
        slotIconGO.GetComponent<Image>().sprite = icon;
    }

    public void UseItem()
    {
        item.GetComponent<Item>().ItemUseage();
        foreach (Transform child in transform)
        {
            if (child.tag == "Item" || child.tag == "ItemButton")
            {
                GameObject.Destroy(child.gameObject);
            }
            else
            {
                child.GetComponent<Image>().sprite = baseIcon;
            }
        }
        ResetSlot();
    }

    public void ResetSlot()
    {
        empty = true;
        icon = null;
        type = null;
        ID = 0;
        item = null;
        desc = null;
    }
}
