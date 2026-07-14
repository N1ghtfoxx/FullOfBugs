using UnityEngine;

public class CollectableMapItem : MonoBehaviour, IInteractable
{
    [SerializeField] protected ItemData _data;
    public bool instantInteract { get; set; } = false;

    public void Interact()
    {
        if (InventoryManager.instance.AddItemToInventory(_data))
        {
            LootNotificationManager.instance.ShowNotification(_data);
            Destroy(gameObject);
        }
        else 
            FailFeedbackManager.instance.ShowFailFeedbackInGame(_data.icon, gameObject);
    }

    public void Selected()
    {
        FailFeedbackManager.instance.ShowFailFeedbackInGame(_data.icon, gameObject);
    }
}
