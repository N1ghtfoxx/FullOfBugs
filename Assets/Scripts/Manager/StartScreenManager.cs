using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class StartScreenManager : MonoBehaviour
{
    public Slot[] slots;
    private int selectedSlotIndex = -1;
    public Button loadButton;
    public Button deleteButton;

    private void Start()
    {
        loadButton.onClick.AddListener(OnLoadButtonClicked);
        deleteButton.onClick.AddListener(OnDeleteButtonClicked);
        loadButton.gameObject.SetActive(false);
        deleteButton.gameObject.SetActive(false);
    }

    public void OnSlotSelected(int slotIndex)
    {
        selectedSlotIndex = slotIndex;
        if(SaveLoadManager.Instance.SaveExists(slotIndex))
         {
            loadButton.gameObject.SetActive(true);
            deleteButton.gameObject.SetActive(true);
        }
        else
        {
            loadButton.gameObject.SetActive(false);
            deleteButton.gameObject.SetActive(false);
        }
    }

    public void OnLoadButtonClicked()
    {
        if(selectedSlotIndex != -1)
        {
            // Load the game for the selected slot and switch to the main game scene
            SaveData data = SaveLoadManager.Instance.LoadGame(selectedSlotIndex);
            SaveLoadManager.Instance.currentSaveData = data; // Store the loaded data in the manager so it can be accessed by other scripts
            SaveLoadManager.Instance.selectedSlotIndex = selectedSlotIndex; // Store the selected slot index in the manager so it can be accessed by other scripts
            Debug.Log("Game loaded from slot " + selectedSlotIndex);
            SceneLoadingManager.Instance.LoadScene("MainScene");          
        }
    }

    public void OnDeleteButtonClicked()
    {
        if(selectedSlotIndex != -1)
        {
            // Delete the save file for the selected slot 
            SaveLoadManager.Instance.DeleteSave(selectedSlotIndex);
            Debug.Log("Save deleted for slot " + selectedSlotIndex);
        }

        // Refresh the UI for all slots to reflect the deleted save
        foreach(Slot slot in slots)
        {
            slot.RefreshUI();
        }
    }
}
