using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CraftRecipeSO", menuName = "Scriptable Objects/CraftRecipeSO")]
public class CraftRecipeSO : ScriptableObject
{
    //creator: Tristan
    public CraftingTableType craftingTableType;

    [Serializable]
    public struct Ingredient
    {
        public ItemScriptableObject item;
        public int itemAmount;
    }

    public List<Ingredient> input;

    public Ingredient output;

    public enum CraftingTableType
    {
        Hand,
        Workbench
    }
}
