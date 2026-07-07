using UnityEngine;

public class DialogueTrigger : MonoBehaviour, IInteractable
{
    public bool instantInteract { get; set; } = true;

    [Header("Trigger Settings")]
    [SerializeField] private TextAsset inkFile;
    [SerializeField] private bool triggerOnlyOnce = false;
    [SerializeField] private bool isAutoPlay = false;


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

        //if the dialogue should only trigger once, disable the game object after triggering
        if (triggerOnlyOnce)
            gameObject.SetActive(false); 
    }

    public void Selected()
    {
        // This trigger is instant interact, so no visual indicator is needed.
    }

}
