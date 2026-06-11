using UnityEngine;

public class DialogueTrigger : MonoBehaviour, IInteractable
{
    public bool instantInteract { get; set; } = true;

    [SerializeField] private TextAsset inkFile;
    public void Interact()
    {
        DialogueManager.instance.StartDialogue(inkFile);
    }

}
