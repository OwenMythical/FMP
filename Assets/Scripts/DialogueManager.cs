using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI TextDisplay;
    public Image Panel;
    public bool DialogueRunning = false;
    public bool DialogueRunning2 = false;

    IEnumerator Dialogue(string Text)
    {
        Debug.Log(Text);
        if (DialogueRunning == false)
        {
            DialogueRunning = true;
            string DisplayedText = "";
            int i = 0;
            TextDisplay.text = "";
            TextDisplay.enabled = true;
            Panel.enabled = true;
            DialogueRunning2 = true;
            while (DialogueRunning2 == true && DisplayedText != Text)
            {
                DisplayedText += Text[i];
                Debug.Log(DisplayedText);
                yield return new WaitForSeconds(0.05f);
                TextDisplay.text = DisplayedText;
                i += 1;
            }
            yield return new WaitForSeconds(1f);
            TextDisplay.text = "";
            Panel.enabled = false;
            TextDisplay.enabled = false;
            DialogueRunning = false;
        }
    }

    public void DialogueStart(string Text)
    {
        StartCoroutine(Dialogue(Text));
    }

    private void Start()
    {
        TextDisplay.text = "";
        Panel.enabled = false;
        TextDisplay.enabled = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (DialogueRunning2 == true)
            {
                DialogueRunning2 = false;
                TextDisplay.text = "";
                Panel.enabled = false;
                TextDisplay.enabled = false;
            }
        }
    }
}
