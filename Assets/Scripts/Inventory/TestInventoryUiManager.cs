using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class TestInventoryUiManager : Singleton<TestInventoryUiManager>
{
    public InventorySlot[] inventorySlots;
    public GameObject inventory;
    public InventorySlot[] inventoryPlusSlots;
    public GameObject inventoryPlus;
    private ItemData _currentDetailItem;
    [SerializeField] private Image _detailItemImage;
    [SerializeField] private TMP_Text _detailDescriptionText;
    [SerializeField] private GameObject _inventoryConsumeButton;
        
    void Start()
    {
        inventory.SetActive(false);
        inventoryPlus.SetActive(false);
    }

    public void ToggleInventory()
    {
        inventory.SetActive(!inventory.activeSelf);
        if (inventory.activeSelf) // if inventory is currently closed, update the UI before opening
        {
            UpdateInventoryUI();
            ClearItemDetail(); // Clear item details when opening the inventory
        }
        PauseManager.instance.SetPause();
    }

    public void ToggleInventoryPlus()
    {
        inventoryPlus.SetActive(!inventoryPlus.activeSelf);
        if(inventoryPlus.activeSelf)
        {
            UpdateInventoryPlusUI();
        }
        PauseManager.instance.SetPause();
    }

    public void UpdateInventoryPlusUI()
    {
        for (int i = 0; i < inventoryPlusSlots.Length; i++)
        {
            if (i < InventoryManager.instance.inventory.Count)
            {
                inventoryPlusSlots[i].UpdateItemSlot(InventoryManager.instance.inventory[i]);
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
        PauseManager.instance.SetPause();
    }

    public void UpdateInventoryUI()
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (i < InventoryManager.instance.inventory.Count)
            {
                inventorySlots[i].UpdateItemSlot(InventoryManager.instance.inventory[i]);
            }
            else
            {
                inventorySlots[i].UpdateItemSlot(null);
            }
        }
    }

    public void ShowItemDetails(ItemData item)
    {
        _currentDetailItem = item;
        _detailItemImage.sprite = item.icon;
        _detailItemImage.color = Color.white; // Ensure the image is visible
        _detailDescriptionText.text = ItemDescriptions.instance.GetDescription(item.name);
        _inventoryConsumeButton.SetActive(Consumables.instance.IsConsumable(item.name));
    }

    public void ClearItemDetail()
    {
        _detailItemImage.sprite = null;
        _detailItemImage.color = new Color(1, 1, 1, 0); // Make the image transparent
        _detailDescriptionText.text = "";
        _inventoryConsumeButton.SetActive(false);
    }

    public void ConsumeCurrentItem()
    {
        if (_currentDetailItem == null) return;

        string itemName = _currentDetailItem.name;

        InventoryManager.instance.RemoveItemFromInventory(itemName);
        Consumables.instance.UseConsumable(itemName);
        UpdateInventoryUI();

        ItemData remainingItem = InventoryManager.instance.inventory.Find(i => i.name == itemName);
        if(remainingItem != null)
        {
            ShowItemDetails(remainingItem);
        }
        else
        {
            ClearItemDetail();
        }
    }
}