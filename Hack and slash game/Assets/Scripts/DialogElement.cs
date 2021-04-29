using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogElement : MonoBehaviour
{
    public Dialog[] dialogElements;
    public int progression;

    public void Start()
    {
        progression = 0;
    }

    public void TriggerDialog()
    {
        FindObjectOfType<DialogManager>().startDialog(dialogElements[progression], this);
    }
}
