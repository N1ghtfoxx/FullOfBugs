using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public int slotIndex;
    public Button slotButton;
    public Image slotImage;
    public Image slotBackgroundImage;
    public Sprite emptySlotSprite;
    public Sprite filledSlotSprite;
    // public TMP_Text emptySlotText;
    public TMP_Text playtimeText;
    // public TMP_Text progressText;
    private StartScreenManager startScreenManager;

    private void Start()
    {
        startScreenManager = FindFirstObjectByType<StartScreenManager>();
        RefreshUI();

        // Add a click listener to the button to load the game when clicked
        slotButton.onClick.AddListener(OnSlotClicked);
    }

    private void OnSlotClicked()
    {
        if(SaveLoadManager.Instance.SaveExists(slotIndex))
        {
            // If a save file exists, select this slot in the StartScreenManager
            startScreenManager.OnSlotSelected(slotIndex);
        }
        else
        {
            // No save file - create a new save file with default data and select it (testing purposes)
            Debug.Log("Starting new game in slot " + slotIndex);
            SaveData newData = new SaveData();
            newData.level = 1;
            newData.playtime = 0f;
            newData.posX = 0f;
            newData.posY = 0f;
            newData.inventory = new System.Collections.Generic.List<ItemData>();
            newData.chest = new System.Collections.Generic.List<ItemData>();
            SaveLoadManager.Instance.SaveGame(newData, slotIndex);
            RefreshUI();
            startScreenManager.OnSlotSelected(slotIndex);
        }
    }

    /// TODO: Add a method to update the UI for this slot when the save data changes (e.g. after loading a game or deleting a save)
    public void RefreshUI()
    {
        if(SaveLoadManager.Instance.SaveExists(slotIndex))
        {
            SaveData data = SaveLoadManager.Instance.LoadGame(slotIndex);
            //emptySlotText.gameObject.SetActive(false);
            playtimeText.gameObject.SetActive(true);
            playtimeText.text = "Playtime: " + Mathf.FloorToInt(data.playtime) + "s";
            //progressText.gameObject.SetActive(true);
            //progressText.text = "Level: " + data.level;
            slotImage.gameObject.SetActive(true);
            slotBackgroundImage.sprite = filledSlotSprite;
        }
        else
        {
            //emptySlotText.gameObject.SetActive(true);
            playtimeText.gameObject.SetActive(false);
            //progressText.gameObject.SetActive(false);
            slotImage.gameObject.SetActive(false);
            slotBackgroundImage.sprite = emptySlotSprite;
        }
    }
}
