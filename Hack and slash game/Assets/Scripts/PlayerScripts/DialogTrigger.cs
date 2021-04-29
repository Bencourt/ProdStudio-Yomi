using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform dialogTrigger;
    public float dialogRange = 2.0f;
    public LayerMask NPCLayer;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Collider[] hitNPCs = Physics.OverlapSphere(dialogTrigger.position, dialogRange, NPCLayer);
            if (hitNPCs.Length > 0)
            {
                Collider closestNPC = null;
                float closestDistSqr = Mathf.Infinity;
                Vector3 currentPos = transform.position;

                foreach (Collider npc in hitNPCs)
                {
                    Vector3 directionToTarget = npc.transform.position - currentPos;
                    float distSqrToTarget = directionToTarget.sqrMagnitude;
                    if (distSqrToTarget < closestDistSqr)
                    {
                        closestDistSqr = distSqrToTarget;
                        closestNPC = npc;
                    }
                }

                closestNPC.gameObject.GetComponent<DialogElement>().TriggerDialog();
                if(closestNPC.gameObject.GetComponent<QuestElement>() != null)
                {
                    closestNPC.gameObject.GetComponent<QuestElement>().TriggerQuest();
                }

            }
        }
    }
}
