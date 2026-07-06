using UnityEngine;
using System.Collections.Generic;

public class FarmingManager : Singleton<FarmingManager>
{
    public List<FarmField> fields = new List<FarmField>();
    public bool isWaitingForSeed { get; private set; } = false;
    private FarmField _activeField;
    [SerializeField] private ItemData[] _seedTypes;
    [SerializeField] private ItemData[] _cropResults; 
    [SerializeField] private Sprite[] _seedSprites; // Sprites for the seed stages
    [SerializeField] private Sprite[] _sproutSprites; // Sprites for the sprout stages
    [SerializeField] private Sprite[] _grownPlantSprites; // Sprites for the grown plant stages


    public void StartSeedSelection(FarmField field)
    {
        _activeField = field;
        isWaitingForSeed = true;
        TestInventoryUiManager.instance.ToggleInventory();
    }

    public void SelectSeed(ItemData seed)
    {
        if (_activeField == null) return;

        int index = System.Array.FindIndex(_seedTypes, s => s.name == seed.name);

        if(index != -1)
        {
            _activeField.Plant(_cropResults[index]);
            InventoryManager.instance.RemoveItemFromInventory(seed.name, InventoryManager.instance.inventory);
            TestInventoryUiManager.instance.UpdateInventoryUI();
            isWaitingForSeed = false;
        _activeField = null;
        TestInventoryUiManager.instance.ToggleInventory();
        }
        else 
        {
            Debug.LogWarning("Selected item is not a seed!");
        } 
    }

    public void CancelSeedSelection()
    {
        isWaitingForSeed = false;
        _activeField = null;
        TestInventoryUiManager.instance.ToggleInventory();
    }

    public ItemData GetCropResult(ItemData seed)
    {
        int index = System.Array.FindIndex(_seedTypes, s => s.name == seed.name);
        ItemData result = index != -1 ? _cropResults[index] : null;
        return result;
    }

    public Sprite GetSeedSprite(ItemData seed)
    {
        int index = System.Array.FindIndex(_cropResults, c => c.name == seed.name);
        return index != -1 ? _seedSprites[index] : null;
    }

    public Sprite GetSproutSprite(ItemData seed)
    {
        int index = System.Array.FindIndex(_cropResults, c => c.name == seed.name);
        return index != -1 ? _sproutSprites[index] : null;
    }

    public Sprite GetGrownPlantSprite(ItemData seed)
    {
        int index = System.Array.FindIndex(_cropResults, c => c.name == seed.name);
        return index != -1 ? _grownPlantSprites[index] : null;
    }
}
