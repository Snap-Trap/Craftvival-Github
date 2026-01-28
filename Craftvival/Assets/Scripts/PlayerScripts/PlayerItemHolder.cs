using UnityEngine;

public class PlayerItemHolder : MonoBehaviour
{
    public Transform itemHolder;

    private GameObject currentItem;
    private ItemScriptableObject lastEquippedItem;

    private void Start()
    {
        lastEquippedItem = InventoryUI.equippedItem;
    }

    void Update()
    {
        // Only react when equipped item changes
        if (InventoryUI.equippedItem != lastEquippedItem)
        {
            Equip(InventoryUI.equippedItem);
            lastEquippedItem = InventoryUI.equippedItem;
        }
    }

    void Equip(ItemScriptableObject item)
    {
        // Remove old item
        if (currentItem != null)
        {
            Destroy(currentItem);
        }

        // Nothing equipped
        if (item == null)
            return;

        // Spawn new item
        currentItem = Instantiate(item.itemPrefab,itemHolder);

        currentItem.transform.localPosition = Vector3.zero;
        currentItem.transform.localRotation = Quaternion.identity;
    }
}

