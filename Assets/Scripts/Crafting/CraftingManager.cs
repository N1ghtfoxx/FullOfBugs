using UnityEngine;
using NUnit.Framework;
using System.Collections.Generic;
using Crafting;
using System.Linq;
using System;

public class CraftingManager : Singleton<CraftingManager>
{
    public Recipe currentRecipe;
    public CraftingUI craftingUI;
    private bool[] _ingrediantBool;
    private List<Recipe> recipes = new List<Recipe>();
    public float remainingCraftTime = 30;
    public Recipe[] testRecipe;
    public bool isCrafting = false;

    [SerializeField] ItemData[] resultItems;

    protected override void Awake()
    {
        base.Awake();
        craftingUI = FindFirstObjectByType<CraftingUI>();
        if (craftingUI == null) Debug.Log("Hier");
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
        if (recipes.Contains(recipe)) return;
        recipes.Add(recipe);
        switch (recipe.name)
        {
            case RecipeName.HealingRecipe:
                TryUpgradeRecipe(RecipeName.HealingRecipe, Skilltree.SkillID.StrongHealingPotion);
                break;
            case RecipeName.GlowingRecipe:
                TryUpgradeRecipe(RecipeName.GlowingRecipe, Skilltree.SkillID.LongGlowPotion);
                break;
            default:
                break;
        }
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

    public void TryUpgradeRecipe(RecipeName name, Skilltree.SkillID skill)
    {
        Recipe recipe = recipes.Find(r => r.name == name);
        if (SkillManager.instance.HasSkill(skill) && recipe!=null)
        {
            recipe.result = (Result)(int)recipe.result + 1;
            recipe.slotSprites[0] = resultItems[(int)recipe.result].icon;
        }
        craftingUI.UpdateResultShape();
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

    public void AddToRecipeSlot(int index)
    {
        _ingrediantBool[index] = true;
    }

    public void RemoveFromRecipeSlot(int index)
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
        craftingUI.FillResultSlot(resultItems[(int)currentRecipe.result]);

    }

}
