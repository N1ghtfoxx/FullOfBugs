using Crafting;
using UnityEngine;
using UnityEngine.UI;

public class CraftingSlot : InventorySlot
{
    public Image itemShape;
    public int slotIndex;

    private void OnEnable()
    {
        UpdateItemSlot(null);
    }

    public bool CheckIngrediant(ItemData item)
    {
        Debug.Log(CraftingManager.instance.currentRecipe.ingrediants.Length);
        Recipe current = CraftingManager.instance.currentRecipe;
        if (item.ingrediant == current.ingrediants[slotIndex] && SkillManager.instance.HasSkill(current.requiredSkill))
        {
            CraftingManager.instance.AddToRecipeSlot(slotIndex);
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
        CraftingManager.instance.RemoveFromRecipeSlot(slotIndex);
        UpdateItemSlot(null);
    }
}
