using UnityEngine;

public class FarmField : MonoBehaviour, IInteractable
{
    [SerializeField] private FieldState _state;
    private ItemData _plantedSeed;
    private bool _playerInRange;
    private float _growthTime = 30f; 
    public bool instantInteract { get; set; } = false;

    public void Interact()
    {
        Debug.Log("Interact called on field with state: " + _state);
        switch (_state)
        {
            case FieldState.Blocked:
                // Do nothing or show a message that the field is blocked
                Debug.Log("This field is blocked and cannot be used.");
                break;
            case FieldState.Empty:
                // Open seed selection UI
                FarmingManager.instance.StartSeedSelection(this);
                break;
            case FieldState.Planted:
                // Water the plant
                Water();
                break;
            case FieldState.Watered:
                // Do nothing or show a message that the plant is already watered
                Debug.Log("Plant is growing. Please wait until it's ready to harvest.");
                break;
            case FieldState.ReadyToHarvest:
                // Harvest the plant
                Harvest();
                break;
            default:
                break;
        }
    }

    /*public void OnMouseDown()
    {
        Debug.Log("OnMouseDown called");
        if(!_playerInRange) return;
        switch (_state)
        {
            case FieldState.Planted:
                // Water the plant
                Water();
                break;
            case FieldState.ReadyToHarvest:
                // Harvest the plant
                Harvest();
                break;
        }
    }*/

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _playerInRange = true;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _playerInRange = false;
        }
    }

    public void Plant(ItemData seed)
    {
        _plantedSeed = seed;
        _state = FieldState.Planted;
        Debug.Log($"Planted {seed.name} in the field.");
    }

    public void Water()
    {
        bool hasWaterdrop = false;
        foreach(ItemData item in InventoryManager.instance.inventory)
        {
            if(item.name == "Waterdrop" && item.quantity > 0)
            {
                item.quantity--;
                TestInventoryUiManager.instance.UpdateInventoryUI();
                if(item.quantity <= 0)
                {
                    InventoryManager.instance.inventory.Remove(item);
                }
                hasWaterdrop = true;
                break;
            }
        }
        if (!hasWaterdrop)
        {
            Debug.LogWarning("No Waterdrop in inventory! Cannot water the field.");
            return;
        }
        _state = FieldState.Watered;
        StartCoroutine(GrowCrops());
        Debug.Log("Field watered. Crops will grow soon.");
    }

    private System.Collections.IEnumerator GrowCrops()
    {
        yield return new WaitForSeconds(_growthTime);
        _state = FieldState.ReadyToHarvest;
        Debug.Log("Crops are ready to harvest!");
    }

    public void Harvest()
    {
       Debug.Log("_plantedSeed:" + _plantedSeed?.name); 
       ItemData cropResult = FarmingManager.instance.GetCropResult(_plantedSeed);

       if(InventoryManager.instance.AddItemToInventory(cropResult))
       {
           Debug.Log("Inventory count after harvest: " + InventoryManager.instance.inventory.Count);
           _state = FieldState.Empty;
           _plantedSeed = null;
           TestInventoryUiManager.instance.UpdateInventoryUI();
           foreach(ItemData item in InventoryManager.instance.inventory)
            {
                Debug.Log("Item: " + item.name + " Quantity: " + item.quantity);
            }
       }
       else
       {
           Debug.LogWarning("Inventory is full! Cannot harvest.");
       }
    }

    public enum FieldState
    {
        Blocked,
        Empty,
        Planted,
        Watered,
        ReadyToHarvest
    }
}
