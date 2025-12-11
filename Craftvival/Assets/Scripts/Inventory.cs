using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public class Item
    {
        public string itemName;
        public string itemType;
        public string itemDescription;
        public Sprite itemIcon;
    }

    Dictionary<Item, int> inventory = new Dictionary<Item, int>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (var entry in inventory)
        {
            Debug.Log(entry.Key + " " + entry.Value);
        }
    }

    public void AddItem(Item item, int amount = 1)
    {
        if (!CanAddItem(item, amount))
        {
            Debug.Log("No more space for " + item.itemName);
            return;
        }

        if (inventory.ContainsKey(item))
        {
            inventory[item] += amount;
        }
        else
        {
            inventory[item] = 1;
        }
    }

    private bool CanAddItem(Item item, int amount = 1)
    {
        if (inventory.ContainsKey(item) && inventory[item] >= 64)
        {
            return false;
        }
        return true;
    }

    public bool HasItem(Item item, int amount = 1)
    {
        return inventory.ContainsKey(item) && inventory[item] >= amount;
    }

    public bool RemoveItem(Item item, int amount = 1)
    {
        if (!HasItem(item, amount))
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
