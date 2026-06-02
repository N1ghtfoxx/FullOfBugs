using UnityEngine;
using UnityEngine.InputSystem;

public class Storage : MonoBehaviour
{
    private bool _playerInRange;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            _playerInRange = true;
            Debug.Log("Player entered storage area");
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            _playerInRange = false;
            Debug.Log("Player left storage area");
        }
    }

    public void OnStorageInteract(InputAction.CallbackContext ctx)
    {
        if(ctx.performed && _playerInRange)
        {
            Debug.Log("Interacted with storage");
            // Here you would add code to open the storage UI and allow the player to transfer items between their inventory and the chest
        }
    }
}