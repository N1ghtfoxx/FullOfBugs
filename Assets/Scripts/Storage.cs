using UnityEngine;

public class Storage : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        TestStorageUiManager.Instance.ToggleStorage();
    }
}