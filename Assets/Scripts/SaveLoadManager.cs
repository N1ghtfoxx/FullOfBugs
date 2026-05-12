using UnityEngine;
using System.IO;
using System;

// Handles saving, oading, and deleting save files
// Uses a Singleton so there is only ever one instance of this manager in the game
public class SaveLoadManager : MonoBehaviour
{
    // The one and only instance of this manager, accessible from anywhere
    public static SaveLoadManager Instance { get; private set; }

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
            Debug.Log("Game loaded from" + filePath);
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
}