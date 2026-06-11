using Ink.Runtime;
using TMPro;
using UnityEngine;

public class DialogueManager : Singleton<DialogueManager>
{
    [Header("Dialogue Settings")]
    [SerializeField] private TextMeshProUGUI displayText;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private Story story;


    private void Start()
    {
        dialoguePanel.SetActive(false);
    }

    public void StartDialogue(TextAsset inkFile)
    {
        if (inkFile != null)
        {
            story = new Story(inkFile.text);
            dialoguePanel.SetActive(true);
            ContinueDialogue();
        }
    }

    private void ContinueDialogue()
    {
        if (story != null && story.canContinue)
        {
            displayText.text = story.Continue();
        }
        else
        {
            EndDialogue();
        }
    }

    private void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        story = null;
    }
}
