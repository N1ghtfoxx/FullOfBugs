using Ink.Parsed;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class DialogueNpc : MonoBehaviour, IInteractable
{
    public bool instantInteract { get; set; } = false;

    [Header("Trigger Settings")]
    [SerializeField] private List<TextAsset> randomDialogue;
    [SerializeField] private bool triggerOnlyOnce = false;
    [SerializeField] private bool isAutoPlay = false;

    [Header("Visual Settings")]
    [SerializeField] private GameObject visualIndicator;

    [Header("Quest System Settings")]
    private List<string> questsToGive = new List<string>();


    public void Interact()
    {
        //return if the dialogue is already active
        if (DialogueManager.instance.isDialogueActive)
            return;

        if(questsToGive.Count > 0)
        {
            QuestManager.instance.StartQuest(questsToGive[0]);
        }


        TextAsset inkFile = randomDialogue[Random.Range(0, randomDialogue.Count)];
        // start the dialogue using the ink file assigned to this trigger
        DialogueManager.instance.StartDialogue(inkFile, isAutoPlay);

        // lock the player movement while the dialogue is active
        PauseManager.instance.SetPause();

        //if the dialogue should only trigger once, disable the game object after triggering
        if (triggerOnlyOnce)
            gameObject.SetActive(false);
    }

    public void Selected()
    {
        // Show visual indicator when selected
        ShowVisualIndicator();
    }

    public void ShowVisualIndicator()
    {
        if (visualIndicator != null)
            visualIndicator.SetActive(true);
    }

    public void HideVisualIndicator()
    {
        if (visualIndicator != null)
            visualIndicator.SetActive(false);
    }
}
