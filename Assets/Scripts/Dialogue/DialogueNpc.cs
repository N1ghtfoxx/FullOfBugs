using System.Runtime.CompilerServices;
using UnityEngine;

public class DialogueNpc : MonoBehaviour, IInteractable
{
    public bool instantInteract { get; set; } = false;

    [Header("Trigger Settings")]
    [SerializeField] private TextAsset inkFile;
    [SerializeField] private bool triggerOnlyOnce = false;
    [SerializeField] private bool isAutoPlay = false;

    [Header("Visual Settings")]
    [SerializeField] private GameObject visualIndicator;

    [Header("Quest System Settings")]
    // Add quest ID / reference here if needed to initiate quests when interacting with this NPC
    private bool hasQuest; // placeholder to avoid the header error.


    public void Interact()
    {
        //return if the dialogue is already active
        if (DialogueManager.instance.isDialogueActive)
            return;

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
