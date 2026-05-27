using UnityEngine;
using System.IO;
using System;

// Handles saving, oading, and deleting save files
// Uses a Singleton so there is only ever one instance of this manager in the game
public class SaveLoadManager : MonoBehaviour
{
    // The one and only instance of this manager, accessible from anywhere
    public static SaveLoadManager Instance { get; private set; }
    public SaveData currentSaveData; // The currently loaded save data, which can be accessed and modified by other scripts
    public int selectedSlotIndex; // The index of the currently selected save slot, which can be accessed by other scripts (e.g. to save to the correct slot when saving from the main game scene)

    private void Awake()
    {
        // if an instance already exists, destroy this duplicate and stop here
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        // No instance yet - make this one the official instance
        Instance = this;
        // Keep this object alive when switching scenes
        DontDestroyOnLoad(gameObject);
    }

    // Builds the full file path for a given save slot
    // e.g. slot 1 -> ".../Slot1.json"
    private string GetFilePath(int slotIndex)
    {
        string filePath = Application.persistentDataPath + "/Slot" + slotIndex + ".json";
        return filePath;
    }

    // Converts the save data to JSON and writes it to the correct slot file
    public void SaveGame(SaveData data, int slotIndex)
    {
        string json = JsonUtility.ToJson(data);
        string filePath = GetFilePath(slotIndex);
        System.IO.File.WriteAllText(filePath, json);
        Debug.Log("Game saved to " + filePath);
    }

    // Reads the save file for the given slot and returns the data
    // Returns null if the file does not exist
    public SaveData LoadGame( int slotIndex)
    {
        string filePath = GetFilePath(slotIndex);
        if (System.IO.File.Exists(filePath))
        {
            // Read the raw JSON text from disk and convert it back into a SaveData
            string json = System.IO.File.ReadAllText(filePath);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            // Debug.Log("Game loaded from" + filePath); //
            return data;
        }
        else
        {
            Debug.LogError("File not found: " + filePath);
            return null;
        }
    }

    // Returns true if a save file exists for the given slot, false if not
    public bool SaveExists(int slotIndex)
    {
        string filePath = GetFilePath(slotIndex);
        return System.IO.File.Exists(filePath);
    }

    // Deletes the save file for the given slot if it exists
    public void DeleteSave(int slotIndex)
    {
        string filePath = GetFilePath(slotIndex);
        if (System.IO.File.Exists(filePath))
        {
            System.IO.File.Delete(filePath);
            Debug.Log("Save deleted: " + filePath);
        }
        else
        {
            Debug.LogError("File not found: " + filePath);
        }
    }

    public void SaveInventory()
    {
        if(currentSaveData != null)
        {
            currentSaveData.inventory = InventoryManager.Instance.inventory;
            currentSaveData.chest = InventoryManager.Instance.chest;
        }
    }

    public void LoadInventory()
    {
        if(currentSaveData != null)
        {
            InventoryManager.Instance.inventory = currentSaveData.inventory;
            InventoryManager.Instance.chest = currentSaveData.chest;
        }
    }

    // The following two methods are just for testing 
    // - they create a sample save file and load it back to verify everything is working correctly
    [ContextMenu("Test Save")]
    private void TestSave()
    {
        SaveData testData = new SaveData();
        testData.level = 5;
        testData.playtime = 123.45f;
        testData.posX = 10.0f;
        testData.posY = 20.0f;

        ItemData testItem1 = new ItemData();
        testItem1.name = "Strawberry";
        testItem1.quantity = 3;
        testItem1.description = "A juicy red fruit.";
        testItem1.type = "Food";

        testData.inventory = new System.Collections.Generic.List<ItemData>();
        testData.inventory.Add(testItem1);
        testData.chest = new System.Collections.Generic.List<ItemData>();
        testData.chest.Add(testItem1);

        SaveGame(testData, 1);
    }

    [ContextMenu("Test Load")]
    private void TestLoad()    {
        SaveData loadedData = LoadGame(1);
        if (loadedData != null)
        {
            Debug.Log("Level: " + loadedData.level);
            Debug.Log("Playtime: " + loadedData.playtime);
            Debug.Log("Position: (" + loadedData.posX + ", " + loadedData.posY + ")");
            if (loadedData.inventory != null && loadedData.inventory.Count > 0)
            {
                Debug.Log("Inventory items: " + loadedData.inventory[0].name);
            }
            if (loadedData.chest != null && loadedData.chest.Count > 0)
            {
                Debug.Log("Chest items: " + loadedData.chest[0].name);
            }
        }
    }
}