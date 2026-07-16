using UnityEngine;
using System.Collections.Generic;

public class InventoryManager : Singleton<InventoryManager>
{
    public List<ItemData> inventory = new List<ItemData>();
    public List<ItemData> chest = new List<ItemData>();
    public int maxInventorySlots = 7;
    public int maxChestSlots = 20;

    [SerializeField] private Sprite _testSprite; // Assign a test sprite in the inspector

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }

    public bool AddItemToInventory(ItemData newItem, List<ItemData> targetList = null, int maxSlots = 7)
    {
        targetList = targetList == null ? inventory : targetList;
        ItemData existingItem = null;

        foreach(ItemData item in targetList)
        {
            if(item.name == newItem.name)
            {
                existingItem = item;
                break;
            }
        }

        if(existingItem != null)
        {
            existingItem.quantity += newItem.quantity;
            return true;
        } else {
            
            if(targetList.Count < maxSlots)
            {
                ItemData copy = new ItemData();
                copy.name = newItem.name;
                copy.quantity = newItem.quantity;
                copy.description = newItem.description;
                copy.type = newItem.type;
                copy.icon = newItem.icon;
                copy.ingrediant = newItem.ingrediant;
                targetList.Add(copy);
                return true;
            } else {
                Debug.Log("Inventory is full! Cannot add " + newItem.name);
                UiManager.instance.SetNotification("Inventory is full! Cannot add " + newItem.name);
                LootNotificationManager.instance.ShowMessage("Inventory is full! Cannot add " + newItem.name);
                return false;
            }
        }
    }

    public void AddItemToStorage(ItemData newItem, List<ItemData> targetList, int maxChestSlots)
    {
        ItemData existingItem = null;

        foreach(ItemData item in targetList)
        {
            if(item.name == newItem.name)
            {
                existingItem = item;
                break;
            }
        }

        if(existingItem != null)
        {
            existingItem.quantity += newItem.quantity;

        } else {
            
            if(targetList.Count < maxChestSlots)
            {
                ItemData copy = new ItemData();
                copy.name = newItem.name;
                copy.quantity = newItem.quantity;
                copy.description = newItem.description;
                copy.type = newItem.type;
                copy.icon = newItem.icon;
                copy.ingrediant = newItem.ingrediant;
                targetList.Add(copy);
            } else {

                Debug.Log("Storage is full! Cannot add " + newItem.name);
            }
        }
    }

    public bool RemoveItemFromInventory(string itemName, List<ItemData> targetList = null)
    {
        targetList = targetList == null ? inventory : targetList;

        ItemData existingItem = null;

        foreach(ItemData item in targetList)
        {
            if(item.name == itemName)
            {
                existingItem = item;
                break;
            }
        }

        if(existingItem != null)
        {
            existingItem.quantity--;
            if(existingItem.quantity <= 0)
            {
                targetList.Remove(existingItem);

            } 

        } else {

            Debug.Log("CollectableItem " + itemName + " not found in inventory!");

        }
        return existingItem != null;
    }

    public void RemoveItemFromStorage(string itemName, List<ItemData> targetList)
    {
        Debug.Log("RemoveFromStorage aufgerufen für " + itemName);
        ItemData existingItem = null;

        foreach(ItemData item in targetList)
        {
            if(item.name == itemName)
            {
                existingItem = item;
                break;
            }
        }

        if(existingItem != null)
        {
            Debug.Log("CollectableItem gefunden, quantity: " + existingItem.quantity);
            existingItem.quantity--;

            if(existingItem.quantity <= 0)
            {
                targetList.Remove(existingItem);

            } 

        } else {

            Debug.Log("CollectableItem " + itemName + " not found in storage!");

        }
    }

    // The following two methods are just for testing - they add and remove a test item to verify everything is working correctly
    [ContextMenu("Add Test CollectableItem to Inventory")]
    private void AddTestItemToInventory()
    {
        ItemData testItem = new ItemData
        {
            name = "Test CollectableItem",
            quantity = 1,
            icon = _testSprite
        };
        AddItemToInventory(testItem, inventory, maxInventorySlots);
        TestInventoryUiManager.instance.UpdateInventoryUI();
    }

    [ContextMenu("Remove Test CollectableItem from Inventory")]
    private void RemoveTestItemFromInventory()
    {
        RemoveItemFromInventory("Test CollectableItem", inventory);
        TestInventoryUiManager.instance.UpdateInventoryUI();
    }

    [ContextMenu("Save Inventory")]
    private void SaveInventory()
    {
        SaveLoadManager.Instance.SaveInventory();
    }

    [ContextMenu("Add Test CollectableItem to Storage")]
    private void AddTestItemToStorage()
    {
        ItemData testItem = new ItemData
        {
            name = "Test CollectableItem",
            quantity = 1,
            icon = _testSprite
        };
        AddItemToStorage(testItem, chest, maxChestSlots);
        TestStorageUiManager.instance.UpdateStorageUI();
    }

    [ContextMenu("Remove Test CollectableItem from Storage")]
    private void RemoveTestItemFromStorage()
    {
        RemoveItemFromStorage("Test CollectableItem", chest);
        TestStorageUiManager.instance.UpdateStorageUI();
    }

    [ContextMenu("Save Storage")]
    private void SaveStorage()
    {
        SaveLoadManager.Instance.SaveInventory();
    }
}