using UnityEngine;
using System.Collections.Generic;

public class FarmingManager : Singleton<FarmingManager>
{
    public List<FarmField> fields = new List<FarmField>();
    public bool isWaitingForSeed { get; private set; } = false;
    private FarmField _activeField;

    public void StartSeedSelection(FarmField field)
    {
        _activeField = field;
        isWaitingForSeed = true;
        TestInventoryUiManager.instance.ToggleInventory();
    }

    public void SelectSeed(ItemData seed)
    {
        if (_activeField == null) return;

        _activeField.Plant(seed);
        isWaitingForSeed = false;
        _activeField = null;
        TestInventoryUiManager.instance.ToggleInventory();
    }

    public void CancelSeedSelection()
    {
        isWaitingForSeed = false;
        _activeField = null;
        TestInventoryUiManager.instance.ToggleInventory();
    }
}
