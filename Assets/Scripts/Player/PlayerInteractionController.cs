using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractionController : MonoBehaviour
{
    private IInteractable _currentInteractable;

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
            _currentInteractable = interactable;
            Debug.Log("Player entered trigger with " + other.name);
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        IInteractable interactable = other.GetComponent<IInteractable>();
        if (interactable == null) return;
        if(interactable == _currentInteractable)
        {
            _currentInteractable = null;
            Debug.Log("Player left trigger with " + other.name);
        }
    }

    public void OnInteract(InputAction.CallbackContext ctx)
    {
        if(ctx.performed && _currentInteractable != null)
        {
            Debug.Log("Interact button pressed while player is in range");
            _currentInteractable.Interact();
        }
    }
}