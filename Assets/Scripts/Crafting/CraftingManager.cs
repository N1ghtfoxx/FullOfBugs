using UnityEngine;
using NUnit.Framework;
using System.Collections.Generic;
using Crafting;
using System.Linq;

public class CraftingManager : Singleton<CraftingManager>
{
    public Recipe currentRecipe;
    private CraftingUI craftingUI;
    private bool[] _ingrediantBool;
    private List<Recipe> recipes = new List<Recipe>();
    public float remainingCraftTime = 30;
    public Recipe[] testRecipe;
    public bool isCrafting = false;

    protected override void Awake()
    {
        base.Awake();
        craftingUI = FindObjectOfType<CraftingUI>();
    }

    [ContextMenu("Toggle CraftingUI")]
    public void ToggleCraftingMenu()
    {
        craftingUI.gameObject.SetActive(!craftingUI.gameObject.activeSelf);
    }

    [ContextMenu("Add Recipe")]
    public void AddTestRecipe()
    {
        AddRecipe(testRecipe[0]);
    }

    [ContextMenu("Add Recipe List")]
    public void AddTestRecipeList()
    {
        foreach (Recipe r in testRecipe)
            AddRecipe(r);
    }

    public bool HasRecipe(Recipe recipe)
    {
        return recipes.Contains(recipe);
    }

    public bool CraftingAvailable()
    {
        return recipes.Count > 0;
    }

    public void AddRecipe(Recipe recipe)
    {
        recipes.Add(recipe);
        if(recipes.Count == 1)
            craftingUI.SwitchShowCrafting(true);
        List<string> options = new List<string>();
        foreach (Recipe r in recipes)
        {
            options.Add(RecipeName.GetName(typeof(RecipeName), r.name));
        }
        int i = currentRecipe != null ? recipes.IndexOf(currentRecipe) : 0;
        craftingUI.UpdateDropdown(options, i);
    }

    public int GetCurrentRecipeIndex()
    {
        return recipes.IndexOf(currentRecipe);
    }

    public void SetCurrentRecipe(int index)
    {
        currentRecipe = recipes[index];
        _ingrediantBool = new bool[currentRecipe.ingrediants.Length];
    }

    private void AddToRecipeSlot(int index)
    {
        _ingrediantBool[index] = true;
    }

    private void RemoveFromRecipeSlot(int index)
    {
        _ingrediantBool[index] = false;
    }

    public bool CheckIngrediants()
    {
        return !_ingrediantBool.Contains(false);

    }

    public void StartCraft()
    {
        isCrafting = true;
        remainingCraftTime = currentRecipe.craftTime;
        StartCoroutine(CraftProgress());
    }



    private System.Collections.IEnumerator CraftProgress()
    {
        while(remainingCraftTime > 0)
        {
            remainingCraftTime -= Time.deltaTime;
            craftingUI?.UpdateProgressbar(1 - remainingCraftTime / currentRecipe.craftTime);
            yield return null;
        }
        remainingCraftTime = 0;
        craftingUI?.UpdateProgressbar(1 - remainingCraftTime / currentRecipe.craftTime);
        isCrafting = false;

        Debug.Log("Crafting complete");
        //InventoryManager.instance.AddItem(currentRecipe.result);
    }

}
