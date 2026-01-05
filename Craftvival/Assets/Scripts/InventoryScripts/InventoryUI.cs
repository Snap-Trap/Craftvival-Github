using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static UnityEditor.Timeline.Actions.MenuPriority;

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

    public static void RemoveItemFromUI(ItemScriptableObject removeItem)
    {
        //loop through inventory slots to try find the given item to remove
        foreach (KeyValuePair<GameObject, ItemScriptableObject> itemSlot in inventorySlots)
        {
            if(itemSlot.Value == removeItem)
            {
                inventorySlots[itemSlot.Key] = null;
                UpdateInventoryIcons();
                return;
            }
        }
        //if not found in inventory, then check hotbar
        foreach (KeyValuePair<GameObject, ItemScriptableObject> itemSlot in hotbarSlots)
        {
            if (itemSlot.Value == removeItem)
            {
                hotbarSlots[itemSlot.Key] = null;
                UpdateInventoryIcons();
                return;
            }
        }
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
        UpdateInventoryIcons();
    }

    private void Update()
    {
        // If toggleAction is pressed (this frame), toggle the inventory UI
        if (toggleAction.WasPressedThisFrame())
        {
            //T: if toggle button is pressed while ui is active, it disable the ui, if its inactive then it enables it 
            inventoryBackground.SetActive(!inventoryBackground.activeSelf);
            UpdateInventoryIcons();
        }
    }

    //buttons need a non static function to add to listener (i think, could be wrong but it works like this)
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
            //TODO: add some visual thing to show which slot is selected
            slot.transform.GetChild(0).GetComponent<Image>().color = new Color(0.75f, 075f, 0.75f);
            slot.GetComponent<Image>().color = new Color(0.75f, 0.55f, 0.3f);
        }
        else if (selectedSlot == slot)
        {
            //if slot is equal to selected slot, set selected slot to null
            selectedSlot = null;
            slot.transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1);
            slot.GetComponent<Image>().color = new Color(1, 0.7f, 0.4f);
        }
        else
        {            
            //else swap inventory values
            ItemScriptableObject tempItem;
            //check if the newly selected slot is from the inventory list
            if (inventorySlots.ContainsKey(slot))
            {
                //if so then save said slot be saved in a temp var
                tempItem = inventorySlots[slot];
                //check if the previous selected slot is from the inventory
                if (inventorySlots.ContainsKey(selectedSlot))
                {
                    //then swap the values of the slots
                    inventorySlots[slot] = inventorySlots[selectedSlot];
                    inventorySlots[selectedSlot] = tempItem;
                }
                else
                {
                    //else the previous selected slot is from the hotbar, so swap the values from there
                    inventorySlots[slot] = hotbarSlots[selectedSlot];
                    hotbarSlots[selectedSlot] = tempItem;
                }
            }
            else //do the same but with the new selected slot being from the hotbar
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
            //update the visual things
            UpdateInventoryIcons();
            //reset selected slot
            selectedSlot.transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1);
            selectedSlot.GetComponent<Image>().color = new Color(1, 0.7f, 0.4f);
            selectedSlot = null;

        }

    }

    public static void UpdateInventoryIcons()
    {
        //update both the lists
        UpdateIconsLoop(inventorySlots);
        UpdateIconsLoop(hotbarSlots);

    }

    private static void UpdateIconsLoop(Dictionary<GameObject, ItemScriptableObject> dict)
    {
        foreach (KeyValuePair<GameObject, ItemScriptableObject> itemSlot in dict)
        {
            if (itemSlot.Value != null)
            {
                //set the proper icon value for the slot and enable it
                itemSlot.Key.transform.GetChild(0).GetComponent<Image>().sprite = itemSlot.Value.itemIcon;
                itemSlot.Key.transform.GetChild(0).GetComponent<Image>().enabled = true;
                //set the proper item count for the slot
                itemSlot.Key.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = Inventory.GetItemAmount(itemSlot.Value).ToString();
                itemSlot.Key.transform.GetChild(2).GetComponent<TextMeshProUGUI>().enabled = true;
            }
            else
            {
                //since its null, disable the icon and item count
                itemSlot.Key.transform.GetChild(0).GetComponent<Image>().enabled = false;
                itemSlot.Key.transform.GetChild(2).GetComponent<TextMeshProUGUI>().enabled = false;
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
