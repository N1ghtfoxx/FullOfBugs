using UnityEngine;

public class FarmField : MonoBehaviour, IInteractable
{
    [SerializeField] private FieldState _state;
    [SerializeField] private Sprite _blockedSprite;
    [SerializeField] private Sprite _emptySprite;
    private SpriteRenderer _spriteRenderer;
    private ItemData _plantedSeed;
    private bool _playerInRange;
    private float _growthTime = 30f; 
    public bool instantInteract { get; set; } = false;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        UpdateSprite();
    }

    public void Interact()
    {
        switch (_state)
        {
            case FieldState.Blocked:
                // show a message that the field is blocked
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
                // show a message that the plant is growing and cannot be harvested yet
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
        UpdateSprite();
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
        UpdateSprite();
        StartCoroutine(GrowCrops());
        Debug.Log("Field watered. Crops will grow soon.");
    }

    public void ReduceGrowthTime(float amount)
    {
        _growthTime -= amount;
    }

    private System.Collections.IEnumerator GrowCrops()
    {
        yield return new WaitForSeconds(_growthTime);
        _state = FieldState.ReadyToHarvest;
        UpdateSprite();
        Debug.Log("Crops are ready to harvest!");
    }

    public void Harvest()
    {
       ItemData cropResult = _plantedSeed;

       if(InventoryManager.instance.AddItemToInventory(cropResult))
       {
           _state = FieldState.Empty;
           UpdateSprite();
           _plantedSeed = null;
           TestInventoryUiManager.instance.UpdateInventoryUI();
       }
       else
       {
           Debug.LogWarning("Inventory is full! Cannot harvest.");
       }
    }

    private void UpdateSprite()
    {
        switch (_state)
        {
            case FieldState.Blocked:
                _spriteRenderer.sprite = _blockedSprite;
                break;
            case FieldState.Empty:
                _spriteRenderer.sprite = _emptySprite;
                break;
            case FieldState.Planted:
                _spriteRenderer.sprite = FarmingManager.instance.GetSeedSprite(_plantedSeed);
                break;
            case FieldState.Watered:
                _spriteRenderer.sprite = FarmingManager.instance.GetSproutSprite(_plantedSeed);
                break;  
            case FieldState.ReadyToHarvest:
                _spriteRenderer.sprite = FarmingManager.instance.GetGrownPlantSprite(_plantedSeed);
                break;  
            default:
                break;
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
