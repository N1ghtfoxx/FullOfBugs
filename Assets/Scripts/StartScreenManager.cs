using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
            Debug.Log("Game loaded from slot " + selectedSlotIndex);
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
    }
}
