using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class InventorySlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    public Image itemImage;
    public TMP_Text itemCountText;
    public static InventorySlot draggedSlot;
    public SlotType slotType;
    private Canvas _canvas;
    private GameObject _draggedItem;
    private ItemData _item;

    void Awake()
    {
        _canvas = GetComponentInParent<Canvas>();
    }

    public void UpdateItemSlot(ItemData item)
    {
        _item = item;
        if(item != null)
        {
            itemImage.sprite = item.icon;
            itemCountText.text = item.quantity > 1 ? item.quantity.ToString() : "";
        } else {
            itemImage.sprite = null;
            itemCountText.text = "";
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("BeginDrag");
        if(itemImage.sprite == null) return;

        draggedSlot = this;
        _draggedItem = new GameObject("DraggedItem");
        Debug.Log("DraggedItem");
        var image = _draggedItem.AddComponent<Image>();
        image.sprite = itemImage.sprite;
        image.raycastTarget = false;
        _draggedItem.transform.SetParent(_canvas.transform, false);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(_draggedItem != null)
        {
            _draggedItem.transform.position = eventData.position;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        draggedSlot = null;
        if(_draggedItem != null)
        {
            Destroy(_draggedItem);
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if(draggedSlot == null) return;

        if(slotType == SlotType.Inventory)
        {
            InventoryManager.Instance.AddItemToInventory(draggedSlot._item, InventoryManager.Instance.inventory, InventoryManager.Instance.maxInventorySlots);
        } 
        else 
        {
            InventoryManager.Instance.AddItemToStorage(draggedSlot._item, InventoryManager.Instance.chest, InventoryManager.Instance.maxChestSlots);
        }

        if(draggedSlot.slotType == SlotType.Inventory)
        {
            InventoryManager.Instance.RemoveItemFromInventory(draggedSlot._item.name, InventoryManager.Instance.inventory);
        }
        else
        {
            InventoryManager.Instance.RemoveItemFromStorage(draggedSlot._item.name, InventoryManager.Instance.chest);
        }
        TestInventoryUiManager.instance.UpdateInventoryUI();
        TestStorageUiManager.instance.UpdateStorageUI();   
    }

    public enum SlotType
    {
        Inventory,
        Storage,
    }
}