using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TestInventoryUiManager : Singleton<TestInventoryUiManager>
{
    public InventorySlot[] inventorySlots;
    public GameObject inventory;
    public GameObject inventoryPlus;
        
    void Start()
    {
        inventory.SetActive(false);
        inventoryPlus.SetActive(false);
    }

    private void ToggleInventory()
    {
        if (!inventory.activeSelf) // if inventory is currently closed, update the UI before opening
        {
            UpdateInventoryUI();
        }
        inventory.SetActive(!inventory.activeSelf);
    }

    public void ToggleInventoryPlus()
    {
        inventoryPlus.SetActive(!inventoryPlus.activeSelf);
    }

    public void SetInventoryActive(bool newState)
    {
        if (newState)
        {
            UpdateInventoryUI();
        }
        inventory.SetActive(newState);
    }

    public void OnInventoryToggle(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            ToggleInventory();
        }
    }

    public void UpdateInventoryUI()
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