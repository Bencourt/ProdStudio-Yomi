using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObserverScript : MonoBehaviour
{
    public GameObject player;

    public GameObject enemyGroup;
    public GameObject itemGroup;
    public GameObject npcGroup;

    public List<GameObject> enemies = new List<GameObject>();
    public List<GameObject> groundItems = new List<GameObject>();
    public List<GameObject> npcs = new List<GameObject>();
    public GameObject sidekick;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void RefreshLists()
    {

    }
}
