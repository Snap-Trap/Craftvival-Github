using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    //Creator: Luca
    //Editor: Tristan (pretty much all the code written including comments is mine)

    // Simple toggle for the inventory UI, needs the Background to work
    public InputAction toggleAction;

    public GameObject inventoryBackground;
    public GameObject hotbarBackground;

    public GameObject itemSlotPrefab;
    public int horizontalCellCount;

    public static Sprite nullIcon = null;

    private static Dictionary<GameObject, ItemScriptableObject> inventorySlots = new Dictionary<GameObject, ItemScriptableObject>();
    private static Dictionary<GameObject, ItemScriptableObject> hotbarSlots = new Dictionary<GameObject, ItemScriptableObject>();

    private static GameObject selectedSlot;

    public static bool AddItemToUI(ItemScriptableObject addedItem) // returns false if there is no space in UI anymore
    {
        print("EnteredFunction");
        //check for each slot if the itemslot already has an item attached to it
        foreach (KeyValuePair<GameObject, ItemScriptableObject> itemSlot in inventorySlots)
        {
            if (itemSlot.Value == null)
            {
                print("FoundFreeSlot");
                //if no item attached, then set it to the new item
                inventorySlots[itemSlot.Key] = addedItem;
                UpdateInventoryIcons();
                return true;
            }
        }
        //if no space in inventory, then check hotbar
        foreach (KeyValuePair<GameObject, ItemScriptableObject> itemSlot in hotbarSlots)
        {
            if (itemSlot.Value == null)
            {
                //if no item attached, then set it to the new item
                hotbarSlots[itemSlot.Key] = addedItem;
                UpdateInventoryIcons();
                return true;
            }
        }
        //if no space in hotbar, return false
        return false;
    }

    private void Start()
    {

        //T: turn the inventory UI off
        inventoryBackground.SetActive(false);

        //T: simple for loop that goes through every child object in the inventory grid so i dont have to manually add them again
        int childCount = inventoryBackground.transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            //T: get the child of the grid by its index
            GameObject child = inventoryBackground.transform.GetChild(i).gameObject;
            for (int cellIndex = 0; cellIndex < horizontalCellCount; cellIndex++)
            {
                //create a new cell for each horizontalCellCount
                GameObject tempCell = Instantiate(itemSlotPrefab, child.transform);
                tempCell.GetComponent<Button>().onClick.AddListener(() => { NonStaticSelectSlot(tempCell); });
                inventorySlots.Add(tempCell, null);
                
            }
            
        }

        //T: do the same for the hotbar slots
        for(int i = 0; i < hotbarBackground.transform.childCount; i++)
        {
            GameObject child = hotbarBackground.transform.GetChild(i).gameObject;
            for (int cellIndex = 0; cellIndex < horizontalCellCount; cellIndex++)
            {
                GameObject tempCell = Instantiate(itemSlotPrefab, child.transform);
                tempCell.GetComponent<Button>().onClick.AddListener(() => { NonStaticSelectSlot(tempCell); });
                hotbarSlots.Add(tempCell, null);
            }
        }
    }

    private void Update()
    {
        // If toggleAction is pressed (this frame), toggle the inventory UI
        if (toggleAction.WasPressedThisFrame())
        {
            //T: if toggle button is pressed while ui is active, it disable the ui, if its inactive then it enables it 
            inventoryBackground.SetActive(!inventoryBackground.activeSelf);
        }
    }

    public void NonStaticSelectSlot(GameObject slot)
    {
        SelectSlot(slot);
    }

    private static void SelectSlot(GameObject slot)
    {
        //if no slot has been selected yet
        if (selectedSlot == null)
        {
            //set selected slot to new selected slot
            selectedSlot = slot;
        }
        else if (selectedSlot == slot)
        {
            //if slot is equal to selected slot, set selected slot to null
            selectedSlot = null;
        }
        else
        {            
            //else swap inventory values
            ItemScriptableObject tempItem;
            if (inventorySlots.ContainsKey(slot))
            {
                tempItem = inventorySlots[slot];
                if (inventorySlots.ContainsKey(selectedSlot))
                {
                    inventorySlots[slot] = inventorySlots[selectedSlot];
                    inventorySlots[selectedSlot] = tempItem;
                }
                else
                {
                    inventorySlots[slot] = hotbarSlots[selectedSlot];
                    hotbarSlots[selectedSlot] = tempItem;
                }
            }
            else
            {
                tempItem = hotbarSlots[slot];
                if (inventorySlots.ContainsKey(selectedSlot))
                {
                    hotbarSlots[slot] = inventorySlots[selectedSlot];
                    inventorySlots[selectedSlot] = tempItem;
                }
                else
                {
                    hotbarSlots[slot] = hotbarSlots[selectedSlot];
                    hotbarSlots[selectedSlot] = tempItem;
                }
            }
            UpdateInventoryIcons();
            selectedSlot = null;
        }

    }

    private static void UpdateInventoryIcons()
    {
        foreach (KeyValuePair<GameObject, ItemScriptableObject> itemSlot in inventorySlots)
        {
            if(itemSlot.Value != null)
            {
                itemSlot.Key.transform.GetChild(0).GetComponent<Image>().sprite = itemSlot.Value.itemIcon;
                itemSlot.Key.transform.GetChild(0).GetComponent<Image>().enabled = true;
            }
            else
            {
                itemSlot.Key.transform.GetChild(0).GetComponent<Image>().enabled = false;
            }

        }
        foreach (KeyValuePair<GameObject, ItemScriptableObject> itemSlot in hotbarSlots)
        {
            if (itemSlot.Value != null)
            {
                itemSlot.Key.transform.GetChild(0).GetComponent<Image>().sprite = itemSlot.Value.itemIcon;
                itemSlot.Key.transform.GetChild(0).GetComponent<Image>().enabled = true;
            }
            else
            {
                itemSlot.Key.transform.GetChild(0).GetComponent<Image>().enabled = false;
            }
        }
    }

    public void OnEnable()
    {
        toggleAction.Enable();
    }

    public void OnDisable()
    {
        toggleAction.Disable();
    }
}
