using Skilltree;
using UnityEngine;

namespace Crafting
{
    [System.Serializable]
    public class Recipe
    {
        public RecipeName name;
        public Ingrediant[] ingrediants;
        public Result result;
        public Sprite[] slotSprites;
        public float craftTime;
        public SkillID requiredSkill;
    }

    public enum Ingrediant
    {
        none,
        Strawberry,
        Mint,
        LuminousMoss,
        DungBall,
        CaveMineral
    }

    public enum Result
    {
        HealingPotion,
        HealingPotionPlus,
        GlowingPotion,
        GlowingPotionPlus,
        Fertilizer,
    }

    public enum RecipeName
    {
        HealingRecipe,
        GlowingRecipe,
        FertilizerRecipe
    }
}