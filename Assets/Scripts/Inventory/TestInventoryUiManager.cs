using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TestInventoryUiManager : Singleton<TestInventoryUiManager>
{
    public InventorySlot[] inventorySlots;
    public GameObject inventory;
    public InventorySlot[] inventoryPlusSlots;
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
        PauseManager.instance.SetPause();
    }

    public void ToggleInventoryPlus()
    {
        if(!inventoryPlus.activeSelf)
        {
            UpdateInventoryPlusUI();
        }
        inventoryPlus.SetActive(!inventoryPlus.activeSelf);
        PauseManager.instance.SetPause();
    }

    public void UpdateInventoryPlusUI()
    {
        for (int i = 0; i < inventoryPlusSlots.Length; i++)
        {
            if (i < InventoryManager.Instance.inventory.Count)
            {
                inventoryPlusSlots[i].UpdateItemSlot(InventoryManager.Instance.inventory[i]);
            }
            else
            {
                inventoryPlusSlots[i].UpdateItemSlot(null);
            }
        }
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