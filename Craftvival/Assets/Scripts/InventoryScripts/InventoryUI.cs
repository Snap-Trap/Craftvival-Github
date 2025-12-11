using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    //Creator: Luca

    // Simple toggle for the inventory UI, needs the Canvas to work
    public InputAction toggleAction;
    public Canvas inventoryCanvas;

    public List<GameObject> inventorySlots = new List<GameObject>();

    public void Start()
    {
        inventoryCanvas = GetComponent<Canvas>();

        inventoryCanvas.gameObject.SetActive(false);
    }

    public void Update()
    {
        // If toggleAction is pressed, toggle the inventory UI
        if (toggleAction.ReadValue<float>() == 1)
        {
            inventoryCanvas.gameObject.SetActive(true);
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
