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
            Debug.Log("Storage interact button pressed while player is in range");
            TestStorageUiManager.Instance.ToggleStorage();
        }
    }
}