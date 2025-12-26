using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemSlot : MonoBehaviour
{
    public Image icon;
    public TMP_Text itemName;

    ItemData data;
    ShopUI shopUI;

    void Start()
    {
        shopUI = FindObjectOfType<ShopUI>();
    }

    public void SetItem(ItemData item)
    {
        data = item;
        icon.sprite = item.icon;
        itemName.text = item.itemName;
    }

    public void OnClick()
    {
        shopUI.ShowItem(data);
    }
}
