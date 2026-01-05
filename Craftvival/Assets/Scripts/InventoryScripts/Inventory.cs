using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    //creator: Tristan
    //why do we have this?
    public class Item
    {
        public string itemName;
        public string itemType;
        public string itemDescription;
        public Sprite itemIcon;
    }

    private static Dictionary<ItemScriptableObject, int> inventory = new Dictionary<ItemScriptableObject, int>();

    //just a simple debug to see what is currently in the inventory
    private static void printInventory()
    {
        foreach (var entry in inventory)
        {
            Debug.Log(entry.Key + " " + entry.Value);
        }
    }
    //adds an item for the specified amount if its within the max stack amount
    public static void AddItem(ItemScriptableObject item, int amount = 1)
    {
        int maxAdded = CanAddItem(item, amount);
        if (maxAdded == 0)
        {
            Debug.Log("No more space for " + item.itemName);
            return;
        }
        else if( maxAdded != amount)
        {
            // drop the amount that cant be added
            for(int i = amount - maxAdded; i > 0; i--)
            {
                //drop (to be made)
            }
        }

        if (inventory.ContainsKey(item))
        {
            inventory[item] += amount;
            //update the inventory icons to show the correct item count
            InventoryUI.UpdateInventoryIcons();
        }
        else
        {
            inventory[item] = amount;
            InventoryUI.AddItemToUI(item);
        }
        printInventory();
    }

    private static int CanAddItem(ItemScriptableObject item, int amount = 1)
    {
        //if current amount plus to be added is more than maxStack, then return amount that can be added, else return to be added amount
        if (inventory.ContainsKey(item) && inventory[item] + amount > item.maxStack)
        {
            return item.maxStack - inventory[item];
        }
        return amount;
    }

    //returns the amount of the specified item the player has in inventory
    public static int GetItemAmount(ItemScriptableObject item)
    {
        if (inventory.ContainsKey(item))
        {
            return inventory[item];
        }
        return 0;
    }

    public static bool RemoveItem(ItemScriptableObject item, int amount = 1)
    {
        if (GetItemAmount(item) <= 0)
        {
            return false;
        }
        inventory[item] -= amount;
        if (inventory[item] <= 0)
        {
            inventory.Remove(item);
        }
        return true;

    }
}
