using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowItemUI : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI ItemQuantity;
    private int quantity;

    public void Setup(ItemData itemData)
    {
        icon.enabled = true;
        //Debug.Log("Setting up item UI for: " + itemData.name);
        icon.sprite = itemData.icon;
        itemName.text = itemData.name;
        // ItemQuantity.text = itemData.quantity.ToString()+"x";
        quantity = itemData.quantity;
        UpdateQuantity();
    }

    public void SetupMsg(string msg)
    {
        icon.sprite = null;
        icon.gameObject.SetActive(false);
        itemName.text = msg;
        quantity = 0;
        UpdateQuantity();
    }

    public void AddQuantity(int amount)
    {
        // increase item quantity
        quantity += amount;
        UpdateQuantity();
    }

    private void UpdateQuantity()
    { 
        if (quantity <= 0)
        {
            ItemQuantity.gameObject.SetActive(false);
            return;
        }
        ItemQuantity.gameObject.SetActive(true);
        // update item quantity text in ui
        ItemQuantity.text = quantity.ToString() + "x";
    }
}
