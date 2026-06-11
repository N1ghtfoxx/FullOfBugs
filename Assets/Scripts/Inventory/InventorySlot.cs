using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    public Image itemImage;
    public TMP_Text itemCountText;

    public void UpdateItemSlot(ItemData item)
    {
        if(item != null)
        {
            itemImage.sprite = item.icon;
            itemCountText.text = item.quantity > 1 ? item.quantity.ToString() : "";
        } else {
            itemImage.sprite = null;
            itemCountText.text = "";
        }
    }
}