using Crafting;
using UnityEngine;
using UnityEngine.UI;

public class CraftingSlot : InventorySlot
{
    public Image itemShape;
    public int slotIndex;
    [SerializeField] GameObject _itemPrefab;
    [SerializeField] Transform _spawnPoint;
    public override void UpdateItemSlot(ItemData item)
    {
        if(_item != null)
        {
            if (InventoryManager.instance.AddItemToInventory(_item))
                TestInventoryUiManager.instance.UpdateInventoryPlusUI();
            else
            {
                GameObject go = Instantiate(_itemPrefab, _spawnPoint);
                go.GetComponent<CollectableItem>().SetItem(_item);
            }
        }
        base.UpdateItemSlot(item);
    }

    public bool CheckIngrediant(ItemData item)
    {
        Debug.Log(CraftingManager.instance.currentRecipe.ingrediants.Length);
        Recipe current = CraftingManager.instance.currentRecipe;
        if (item.ingrediant == current.ingrediants[slotIndex -1] && SkillManager.instance.HasSkill(current.requiredSkill))
        {
            CraftingManager.instance.AddToRecipeSlot(slotIndex -1);
            return true;
        }
        else
        {
            FailFeedbackManager.instance.ShowFailFeedbackUI(_slotBackground.sprite, _slotBackground.gameObject);
            return false;
        }
    }

    public void RemoveItem()
    {
        if(slotIndex != 0)
        CraftingManager.instance.RemoveFromRecipeSlot(slotIndex -1);
        _item = null;
        UpdateItemSlot(null);
    }
}
