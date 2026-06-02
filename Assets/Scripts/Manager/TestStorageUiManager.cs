using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class TestStorageUiManager : MonoBehaviour
{
    public InventorySlot[] storageSlots;
    public GameObject storage;

   public void ToggleStorage()
    {
        if (!storage.activeSelf) // if storage is currently closed, update the UI before opening
        {
            UpdateStorageUI();
        }
        storage.SetActive(!storage.activeSelf);
    }

    public void UpdateStorageUI()
    {
        for (int i = 0; i < storageSlots.Length; i++)
        {
            if (i < InventoryManager.Instance.chest.Count)
            {
                storageSlots[i].UpdateItemSlot(InventoryManager.Instance.chest[i]);
            }
            else
            {
                storageSlots[i].UpdateItemSlot(null);
            }
        }
    }
}
