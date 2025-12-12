using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    //Creator: Luca
    //Editor: Tristan

    // Simple toggle for the inventory UI, needs the Canvas to work
    // T: no it dont, i changed the required item to be the grid layout group, and simply grab the parent of that object to get the canvas
    public InputAction toggleAction;
    public GridLayoutGroup inventoryGrid;

    private Canvas inventoryCanvas;

    private List<GameObject> inventorySlots = new List<GameObject>();

    private void Start()
    {
        //T: set the canvas
        inventoryCanvas = inventoryGrid.transform.parent.GetComponent<Canvas>();

        //T: turn the inventory UI off
        inventoryCanvas.gameObject.SetActive(false);

        //T: simple for loop that goes through every child object in the inventory grid so i dont have to manually add them again
        int childCount = inventoryGrid.transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            //T: get the child of the grid by its index
            GameObject child = inventoryGrid.transform.GetChild(i).gameObject;
            inventorySlots.Add(child);
        }
    }

    public void Update()
    {
        // If toggleAction is pressed (this frame), toggle the inventory UI
        if (toggleAction.WasPressedThisFrame())
        {
            //T: if toggle button is pressed while ui is active, it disable the ui, if its inactive then it enables it 
            inventoryCanvas.gameObject.SetActive(!inventoryCanvas.gameObject.activeSelf);
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
