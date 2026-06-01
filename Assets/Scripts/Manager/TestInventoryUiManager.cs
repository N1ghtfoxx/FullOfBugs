using UnityEngine;
using UnityEngine.InputSystem;

public class TestInventoryUiManager : MonoBehaviour
{
    public InventorySlot[] inventorySlots;
    public GameObject inventory;

    private void ToggleInventory()
    {
        if (!inventory.activeSelf) // if inventory is currently closed, update the UI before opening
        {
            UpdateInventoryUI();
        }
        inventory.SetActive(!inventory.activeSelf);
    }

    public void OnInventoryToggle(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            ToggleInventory();
        }
    }

    private void UpdateInventoryUI()
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (i < InventoryManager.Instance.inventory.Count)
            {
                inventorySlots[i].UpdateItemSlot(InventoryManager.Instance.inventory[i]);
            }
            else
            {
                inventorySlots[i].UpdateItemSlot(null);
            }
        }
    }
}