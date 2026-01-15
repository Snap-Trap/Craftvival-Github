using System.Collections.Generic;
using Unity.AppUI.UI;
using UnityEngine;

public class Resources : MonoBehaviour, IDamagable
{
    // Script made by Charly
    public float durability = 5f;
    public List<GameObject> droppedItems;

    public void TakeDamage(float amount)
    {   
        if (gameObject.name == "Water")
        {
            PlayerStatus playerStatus = FindFirstObjectByType<PlayerStatus>();
            if (playerStatus != null)
            {
                playerStatus.AddStatus(10, "Water");
            }
            return;  // Return early and skip the rest of the code
        }
        durability -= amount;
        Debug.Log(gameObject.name + " lost " + amount + " durability");
        if (durability <= 0)
        {
            switch (gameObject.tag)
            {
                case "Tree":
                    // Drop Wood
                    DropItem("Wood");
                    break;

                case "Rock":
                    // Drop stone
                    DropItem("Stone");
                    break;

                default:
                    // No matching tag drops nothing
                    Debug.Log(gameObject.name + " Dropped Nothin");
                    break;
            }
            Debug.Log(gameObject.name + " Gathered");
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);  // Destroy the parent object
            }
            else
            {
                Destroy(gameObject);  // Destroy the current object if no parent
            }
        }
    }

    private void DropItem(string itemType)
    {
        if (droppedItems != null && droppedItems.Count > 0)
        {
            foreach (GameObject item in droppedItems)
            {
                if (item != null) 
                {
                    Instantiate(item, transform.position, Quaternion.identity);
                    Debug.Log(gameObject.name + " dropped " + itemType);
                }
            }
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
