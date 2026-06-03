using UnityEngine;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }
    public List<ItemData> inventory = new List<ItemData>();
    public List<ItemData> chest = new List<ItemData>();
    public int maxInventorySlots = 7;
    public int maxChestSlots = 20;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddItemToInventory(ItemData newItem, List<ItemData> targetList, int maxSlots)
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
            existingItem.quantity++;

        } else {
            
            if(targetList.Count < maxSlots)
            {
                targetList.Add(newItem);

            } else {

                Debug.Log("Inventory is full! Cannot add " + newItem.name);
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
            existingItem.quantity++;

        } else {
            
            if(targetList.Count < maxChestSlots)
            {
                targetList.Add(newItem);

            } else {

                Debug.Log("Storage is full! Cannot add " + newItem.name);
            }
        }
    }

    public void RemoveItemFromInventory(string itemName, List<ItemData> targetList)
    {
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

            Debug.Log("Item " + itemName + " not found in inventory!");

        }
    }

    public void RemoveItemFromStorage(string itemName, List<ItemData> targetList)
    {
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

            Debug.Log("Item " + itemName + " not found in storage!");

        }
    }

    // The following two methods are just for testing - they add and remove a test item to verify everything is working correctly
    [ContextMenu("Add Test Item to Inventory")]
    private void AddTestItemToInventory()
    {
        ItemData testItem = new ItemData
        {
            name = "Test Item",
            quantity = 1
        };
        AddItemToInventory(testItem, inventory, maxInventorySlots);
    }

    [ContextMenu("Remove Test Item from Inventory")]
    private void RemoveTestItemFromInventory()
    {
        RemoveItemFromInventory("Test Item", inventory);
    }

    [ContextMenu("Save Inventory")]
    private void SaveInventory()
    {
        SaveLoadManager.Instance.SaveInventory();
    }

    [ContextMenu("Add Test Item to Storage")]
    private void AddTestItemToStorage()
    {
        ItemData testItem = new ItemData
        {
            name = "Test Item",
            quantity = 1
        };
        AddItemToStorage(testItem, chest, maxChestSlots);
    }

    [ContextMenu("Remove Test Item from Storage")]
    private void RemoveTestItemFromStorage()
    {
        RemoveItemFromStorage("Test Item", chest);
    }

    [ContextMenu("Save Storage")]
    private void SaveStorage()
    {
        SaveLoadManager.Instance.SaveInventory();
    }
}