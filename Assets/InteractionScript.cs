using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InteractionScript : MonoBehaviour
{
    public bool EventInteraction;
    public bool DialogueInteraction;
    public string Text;
    DialogueManager DM;
    GameObject Player;
    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        GameObject DMO = GameObject.FindGameObjectWithTag("Canvas");
        DM = (DialogueManager)DMO.GetComponent("DialogueManager");
    }

    public void Interact()
    {
        if (DialogueInteraction == true)
        {
            StartCoroutine(DM.DialogueStart(Text));
        }
        Debug.Log("Interacted");
    }
}
