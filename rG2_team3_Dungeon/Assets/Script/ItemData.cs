using UnityEngine;

[CreateAssetMenu(menuName = "Shop/Item")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public string description;
    public int price;
    public int ownedCount;
}
