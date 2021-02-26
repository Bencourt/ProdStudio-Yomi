using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string type;
    public int ID;
    public string desc;
    public Sprite icon;
    public bool pickedUp;

    [HideInInspector]
    public bool used;
    [HideInInspector]
    public bool equipped;

    [HideInInspector]
    public GameObject equipment;

    [HideInInspector]
    public GameObject equipmentManager;

    public bool playersEquipment;


    public void Start()
    {
        equipmentManager = GameObject.FindWithTag("EquipmentManager");

        if(!playersEquipment)
        {
            int allEquipment = equipmentManager.transform.childCount;
            for(int i = 0; i < allEquipment; i++)
            {
                if(equipmentManager.transform.GetChild(i).gameObject.GetComponent<Item>().ID == ID)
                {
                    equipment = equipmentManager.transform.GetChild(i).gameObject;
                }
            }
        }
    }

    public void Update()
    {
        if(used)
        {
            int amntToHeal = 0;
            switch (ID)
            {
                case 0:
                    amntToHeal = 10;
                    break;
                default:
                    amntToHeal = 0;
                    break;
            }
        }
        if(equipped)
        {

        }
    }

    public void itemUseage()
    {
        // weapon 
        if(type == "Weapon")
        {
            equipment.SetActive(true);
            equipped = true;
        }
        // health potion
        if(type == "HP")
        {
            
            used = true;
        }

        // hi gamers stream bring me the horizon
    }
}
