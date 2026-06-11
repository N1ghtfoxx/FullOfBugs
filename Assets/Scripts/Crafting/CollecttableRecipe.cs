using Crafting;
using UnityEngine;

public class CollecttableRecipe : MonoBehaviour, IInteractable
{
    [SerializeField] Recipe recipe;

    void Start()
    {
        if (CraftingManager.instance.HasRecipe(recipe))
            Destroy(gameObject);
    }

    public void Interact()
    {
        CraftingManager.instance.AddRecipe(recipe);
        Destroy(gameObject);
    }
}
