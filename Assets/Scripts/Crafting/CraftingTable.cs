using UnityEngine;

public class CraftingTable : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        CraftingManager.instance.ToggleCraftingMenu();
    }
}
