using UnityEngine;

public class FarmField : MonoBehaviour, IInteractable
{
    private FieldState _state;
    private ItemData _plantedSeed;
    private bool _playerInRange;
    public bool instantInteract { get; set; } = false;

    public void Interact()
    {
        
    }

    public void OnMouseDown()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        
    }

    public void Plant(ItemData seed)
    {
        
    }

    public void Water()
    {
        
    }

    public void Harvest()
    {
        
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
