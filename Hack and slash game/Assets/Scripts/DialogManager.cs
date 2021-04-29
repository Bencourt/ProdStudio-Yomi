using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class DialogManager : MonoBehaviour
{
    private Queue<string> sentences;
    public Text nameText;
    public Text DialogText;
    public bool dialogOpen;
    public GameObject dialogBox;
    public GameObject NPC;
    // Start is called before the first frame update
    void Start()
    {
        dialogOpen = false;
        sentences = new Queue<string>();
        NPC = null;
    }

    private void Update()
    {
        if (dialogOpen == true)
        {
            if(Input.GetKeyDown(KeyCode.Mouse1))
            {
                DisplayNextSentence();
            }
        }
    }
    public void startDialog(Dialog d, DialogElement element) 
    {
        if (dialogOpen == false)
        {
            NPC = element.gameObject;
            dialogOpen = true;
            dialogBox.SetActive(true);
            nameText.text = d.name;
            sentences.Clear();

            foreach (string s in d.sentences)
            {
                sentences.Enqueue(s);
            }
            Debug.Log(nameText.text);
            DisplayNextSentence();
        }
    }
    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialog();
            return;
        }
        string sentence = sentences.Dequeue();
        DialogText.text = sentence;
    }

    public void EndDialog()
    {
        dialogBox.SetActive(false);
        dialogOpen = false;
        if (NPC != null)
        {
            if(NPC.GetComponents<QuestElement>().Length > 0)
            {
                foreach(QuestElement q in NPC.GetComponents<QuestElement>())
                {
                    q.TriggerQuest();
                }
            }
            else
            {
                NPC = null;
            }
        }
    }

}
