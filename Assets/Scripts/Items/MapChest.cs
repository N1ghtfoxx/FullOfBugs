using UnityEngine;
using System.Collections.Generic;
using Crafting;

public class MapChest : MonoBehaviour, IInteractable
{
    public bool instantInteract { get; set; } = false;

    [SerializeField] List<ItemData> _items;
    [SerializeField] List<Recipe> _recipes;
    [SerializeField] Sprite _emptySprite;

    public void Interact()
    {
        List<ItemData> items = new List<ItemData>();
        foreach (ItemData item in _items)
        {
            if (!InventoryManager.instance.AddItemToInventory(item))
            {
                items.Add(item);
                FailFeedbackManager.instance.ShowFailFeedbackInGame(GetComponent<SpriteRenderer>().sprite, gameObject);
            }
        }
        _items = items;
        if (_recipes != null)
            foreach (Recipe recipe in _recipes)
                CraftingManager.instance.AddRecipe(recipe);

        if(_items.Count == 0)
        {
            GetComponent<SpriteRenderer>().sprite = _emptySprite;
        }
        //Save The Cheststate
    }

    public void Selected()
    {

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Load the Cheststate
    }

}
