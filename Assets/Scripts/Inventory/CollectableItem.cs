using UnityEngine;

public class CollectableItem : MonoBehaviour, IInteractable
{
    [SerializeField] ItemData _data;

    public bool instantInteract { get; set; } = false;

    public void Interact()
    {
        if(InventoryManager.instance.AddItemToInventory(_data))
            Destroy(gameObject);
        else FailFeedbackManager.instance.ShowFailFeedbackInGame(_data.icon, gameObject);
    }

    void Awake()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = _data.icon;
    }

    public void SetItem(ItemData data)
    {
        _data = data;
        gameObject.GetComponent<SpriteRenderer>().sprite = _data.icon;
    }
}
