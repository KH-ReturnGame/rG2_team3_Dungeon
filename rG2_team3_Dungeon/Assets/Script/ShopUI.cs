using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopUI : MonoBehaviour
{
    public TMP_Text itemNameText;
    public TMP_Text itemDescriptionText;
    public TMP_Text ownedCountText;
    public Image itemIcon;

    ItemData currentItem;
    CurrencyManager currencyManager;

    void Start()
    {
        currencyManager = FindObjectOfType<CurrencyManager>();
    }

    public void ShowItem(ItemData item)
    {
        currentItem = item;

        itemNameText.text = item.itemName;
        itemDescriptionText.text = item.description;
        ownedCountText.text = "보유: " + item.ownedCount;
        itemIcon.sprite = item.icon;
    }

    public void BuyItem(int quantity)
    {
        if (currentItem == null) return;

        int totalPrice = currentItem.price * quantity;

        if (currencyManager.SpendGold(totalPrice))
        {
            currentItem.ownedCount += quantity;
            ownedCountText.text = "보유: " + currentItem.ownedCount;
        }
        else
        {
            Debug.Log("돈 부족");
        }
    }
}
