using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordHitbox : MonoBehaviour
{
    public GameObject player;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("attempting to attack: " + other);
        player.GetComponent<PlayerCombat>().Attack(other.gameObject);
    }

}
