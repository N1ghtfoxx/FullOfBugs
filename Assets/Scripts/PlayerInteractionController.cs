using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractionController : MonoBehaviour
{
    private IInteractable _currentInteractable;

    public void OnTriggerEnter2D(Collider2D other)
    {
        _currentInteractable = other.GetComponent<IInteractable>();
        Debug.Log("Player entered trigger with " + other.name);
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        _currentInteractable = null;
        Debug.Log("Player left trigger with " + other.name);
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