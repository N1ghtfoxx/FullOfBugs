using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class TestStorageUiManager : Singleton<TestStorageUiManager>
{
    public InventorySlot[] storageSlots;
    public InventorySlot[] storageViewInventorySlots;
    public GameObject storage;
    
    void Start()
    {
        storage.SetActive(false);
    }

    public void ToggleStorage()
    {
        if (!storage.activeSelf) // if storage is currently closed, update the UI before opening
        {
            UpdateStorageUI();
        }
        bool newState = !storage.activeSelf;
        storage.SetActive(newState);
    }

    public void UpdateStorageUI()
    {
        Debug.Log("Chest count: " + InventoryManager.instance.chest.Count);
        for (int i = 0; i < storageSlots.Length; i++)
        {
            if (i < InventoryManager.instance.chest.Count)
            {
                storageSlots[i].UpdateItemSlot(InventoryManager.instance.chest[i]);
            }
            else
            {
                storageSlots[i].UpdateItemSlot(null);
            }
        }

        //for (int i = 0; i < storageViewInventorySlots.Length; i++)
        //{
        //    if (i < InventoryManager.Instance.inventory.Count)
        //    {
        //        storageViewInventorySlots[i].UpdateItemSlot(InventoryManager.Instance.inventory[i]);
        //    }
        //    else
        //    {
        //        storageViewInventorySlots[i].UpdateItemSlot(null);
        //    }
        //}
    }
}
