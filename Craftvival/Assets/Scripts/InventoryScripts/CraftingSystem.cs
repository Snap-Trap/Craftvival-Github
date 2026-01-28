using UnityEngine;

public class CraftingSystem : MonoBehaviour
{
    //creator: Tristan
    public static bool CanCraft(CraftRecipeSO recipe)
    {
        foreach (CraftRecipeSO.Ingredient ingredient in recipe.input)
        {
            if (Inventory.GetItemAmount(ingredient.item) > 0)
            {
                //not enough of specific item
                return false;
            }
        }
        return true;
    }
    public static void Craft(CraftRecipeSO recipe)
    {
        if (!CanCraft(recipe))
        {
            return;
        }
        //remove the ingredients
        foreach (CraftRecipeSO.Ingredient ingredient in recipe.input)
        {
            Inventory.RemoveItem(ingredient.item, ingredient.itemAmount);
        }

        //add the crafted item to player
        Inventory.AddItem(recipe.output.item, recipe.output.itemAmount);
    }
}
