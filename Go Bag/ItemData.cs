using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "GoBag/Item")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public bool isNeededInBag; // Is this item required?
    public Sprite itemIcon; // For UI display
    public GameObject itemPrefab; // The 3D model
}