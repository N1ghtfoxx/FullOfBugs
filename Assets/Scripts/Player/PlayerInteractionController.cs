using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractionController : MonoBehaviour
{
    public GameObject progressBar;
    [SerializeField] GameObject _interactIndicator;
    private List<IInteractable> _interactablesInRange = new List<IInteractable>();
    private DialogueNpc dialogueNpc;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (PauseManager.instance.isPaused) return;
        IInteractable interactable = other.GetComponent<IInteractable>();
        if (interactable == null) return;
        if(interactable.instantInteract)
        {
            interactable.Interact();
        }
        else
        {
            if(_interactablesInRange.Contains(interactable)) return;
            _interactablesInRange.Add(interactable);
            _interactIndicator.SetActive(true);
            Debug.Log("Player entered trigger with " + other.name);
            if(interactable is DialogueNpc npc)
            {
                dialogueNpc = npc;
                dialogueNpc.ShowVisualIndicator();
            }
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        IInteractable interactable = other.GetComponent<IInteractable>();
        if (interactable == null) return;
        _interactablesInRange.Remove(interactable);
        if (_interactablesInRange.Count == 0)
        {
            _interactIndicator.SetActive(false);
            if(dialogueNpc != null)
            {
                dialogueNpc.HideVisualIndicator();
                dialogueNpc = null;
            }
        }
    }

    public void OnInteract(InputAction.CallbackContext ctx)
    {
        if(ctx.performed && _interactablesInRange.Count > 0)
        {
            _interactablesInRange[_interactablesInRange.Count - 1].Interact();
        }
    }

    public void OnSwitchInteract(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && _interactablesInRange.Count > 1)
        {
            IInteractable lastInteractable = _interactablesInRange[_interactablesInRange.Count - 1];
            _interactablesInRange.RemoveAt(_interactablesInRange.Count - 1);
            _interactablesInRange.Insert(0, lastInteractable);
            _interactablesInRange[_interactablesInRange.Count - 1].Selected();
        }
    }
}