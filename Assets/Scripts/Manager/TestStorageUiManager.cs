using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class TestStorageUiManager : MonoBehaviour
{
    public static TestStorageUiManager Instance { get; private set; }
    public InventorySlot[] storageSlots;
    public GameObject storage;
    
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
        Debug.Log("Chest count: " + InventoryManager.Instance.chest.Count);
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
