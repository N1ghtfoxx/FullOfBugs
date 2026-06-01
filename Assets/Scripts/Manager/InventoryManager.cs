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

    public void AddItem(ItemData newItem, List<ItemData> targetList, int maxSlots)
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

    public void RemoveItem(string itemName, List<ItemData> targetList)
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

    // The following two methods are just for testing - they add and remove a test item to verify everything is working correctly
    [ContextMenu("Add Test Item")]
    private void AddTestItem()
    {
        ItemData testItem = new ItemData
        {
            name = "Test Item",
            quantity = 1
        };
        AddItem(testItem, inventory, maxInventorySlots);
    }

    [ContextMenu("Remove Test Item")]
    private void RemoveTestItem()
    {
        RemoveItem("Test Item", inventory);
    }

    [ContextMenu("Save Inventory")]
    private void SaveInventory()
    {
        SaveLoadManager.Instance.SaveInventory();
    }
}