using UnityEngine;

public class CraftingTable : MonoBehaviour, IInteractable
{
    public bool instantInteract { get; set; } = false;
    public void Interact()
    {
        TestInventoryUiManager.instance.ToggleInventoryPlus();
        CraftingManager.instance.ToggleCraftingMenu();
    }
    public void Selected()
    {
        FailFeedbackManager.instance.ShowFailFeedbackInGame(GetComponent<SpriteRenderer>().sprite, gameObject);
    }

}
