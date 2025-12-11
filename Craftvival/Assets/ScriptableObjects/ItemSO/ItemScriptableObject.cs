using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/ItemScriptableObject")]
public class ItemScriptableObject : ScriptableObject
{
    //creator: Tristan
    public string itemName;
    public ItemType itemType;
    public string itemDescription;
    public Sprite itemIcon;
    public GameObject prefab;

    public int maxStack = 1;

    public enum ItemType
    {
        Ingredient,
        Food,
        Armor,
        Weapon,
        Object
    }
}
