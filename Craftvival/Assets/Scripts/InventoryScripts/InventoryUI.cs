using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    //Creator: Luca
    //Editor: Tristan

    // Simple toggle for the inventory UI, needs the Background to work
    public InputAction toggleAction;

    public GameObject inventoryBackground;
    public GameObject hotbarBackground;

    public GameObject itemSlotPrefab;
    public int horizontalCellCount;

    private static Dictionary<GameObject, ItemScriptableObject> inventorySlots = new Dictionary<GameObject, ItemScriptableObject>();
    private static Dictionary<GameObject, ItemScriptableObject> hotbarSlots = new Dictionary<GameObject, ItemScriptableObject>();

    private static GameObject selectedSlot;

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
                tempCell.GetComponent<Button>().onClick.AddListener(() => { SelectSlot(tempCell); });
                inventorySlots.Add(tempCell, null);
                
            }
            
        }

        //T: do the same for the hotbar slots (yes i know its double code in a way, ill fix it later)
        for(int i = 0; i < hotbarBackground.transform.childCount; i++)
        {
            GameObject child = hotbarBackground.transform.GetChild(i).gameObject;
            for (int cellIndex = 0; cellIndex < horizontalCellCount; cellIndex++)
            {
                GameObject tempCell = Instantiate(itemSlotPrefab, child.transform);
                tempCell.GetComponent<Button>().onClick.AddListener(() => { SelectSlot(tempCell); });
                hotbarSlots.Add(tempCell, null);
            }
        }
    }

    public void Update()
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
            selectedSlot = null;
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
