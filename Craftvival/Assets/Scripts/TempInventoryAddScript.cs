using UnityEngine;

public class TempInventoryAddScript : MonoBehaviour
{
    public ItemScriptableObject toAddItem;
    private void OnCollisionEnter(Collision collision)
    {
        Inventory.AddItem(toAddItem);
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
