using UnityEngine;

public class CraftingSystem : MonoBehaviour
{
    //creator: Tristan
    public static bool CanCraft(CraftRecipeSO recipe)
    {
        for (int i = 0; i < recipe.input.Count; i++)
        {
            if (!Inventory.HasItem(recipe.input[i].item, recipe.input[i].itemAmount))
            {
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
    }
}
