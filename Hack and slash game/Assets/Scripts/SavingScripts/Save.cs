using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class Save 
{
    public List<int> livingEnemyPositions = new List<int>();
    public List<int> livingNPCPositions = new List<int>();
    public List<int> availableItemPositions = new List<int>();

    public int playerPosition;
    public int sidekickPosition;


}

