using UnityEngine;

public class FarmField : MonoBehaviour, IInteractable
{
    private FieldState _state;
    private ItemData _plantedSeed;
    private bool _playerInRange;
    private float _growthTime = 30f; 
    public bool instantInteract { get; set; } = false;

    public void Interact()
    {
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
            default:
                break;
        }
    }

    public void OnMouseDown()
    {
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
    }

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
    }

    public void Water()
    {
        _state = FieldState.Watered;
        StartCoroutine(GrowCrops());
    }

    private System.Collections.IEnumerator GrowCrops()
    {
        yield return new WaitForSeconds(_growthTime);
        _state = FieldState.ReadyToHarvest;
    }

    public void Harvest()
    {
       if (_state == FieldState.ReadyToHarvest)
        {
            // Add the harvested crop to the player's inventory
            InventoryManager.instance.AddItemToInventory(_plantedSeed);
            _plantedSeed = null;
            _state = FieldState.Empty;
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
