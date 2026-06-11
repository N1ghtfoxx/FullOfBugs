using UnityEngine;

public class Storage : MonoBehaviour, IInteractable
{
    public bool instantInteract { get; set; } = false;
    public void Interact()
    {
        TestInventoryUiManager.instance.ToggleInventoryPlus();
        TestStorageUiManager.instance.ToggleStorage();
    }
}