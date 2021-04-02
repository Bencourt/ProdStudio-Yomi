﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthTextController : MonoBehaviour
{
    private Text healthText;
    private float healthValue;

    // Start is called before the first frame update
    void Start()
    {
        healthText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        AssignHealthValue();
    }

    // Adjusts Health to display number/100 and converts to string for Text Object
    void AssignHealthValue()
    {
        healthValue = PlayerHealthBarHandler.GetHealthBarValue() * 100;
        healthText.GetComponent<Text>().text = healthValue.ToString();
    }
}
