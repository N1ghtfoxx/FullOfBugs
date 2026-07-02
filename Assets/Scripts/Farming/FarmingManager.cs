using UnityEngine;
using System.Collections.Generic;

public class FarmingManager : Singleton<FarmingManager>
{
    public List<FarmField> fields = new List<FarmField>();
    public bool isWaitingForSeed { get; private set; } = false;
    private FarmField _activeField;
    [SerializeField] private ItemData[] _seedTypes;
    [SerializeField] private ItemData[] _cropResults; 


    public void StartSeedSelection(FarmField field)
    {
        _activeField = field;
        isWaitingForSeed = true;
        TestInventoryUiManager.instance.ToggleInventory();
    }

    public void SelectSeed(ItemData seed)
    {
        if (_activeField == null) return;

        int index = System.Array.IndexOf(_seedTypes, seed);

        if(index != -1)
        {
            _activeField.Plant(_cropResults[index]);
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
        int index = System.Array.IndexOf(_seedTypes, seed);
        return index != -1 ? _cropResults[index] : null;
    }
}
