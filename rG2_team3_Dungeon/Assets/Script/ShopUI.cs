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
        ownedCountText.text = "보유: " + item.ownedCount + " / " + item.maxCount;
        itemIcon.sprite = item.icon;
    }

    public void BuyItem(int quantity)
    {
        if (currentItem == null) return;

        // 최대 구매 가능 개수 체크
        if (currentItem.ownedCount + quantity > currentItem.maxCount)
        {
            Debug.Log(currentItem.itemName + " : 최대 구매 개수를 초과했습니다!");
            return;
        }

        int totalPrice = currentItem.price * quantity;

        if (currencyManager.SpendGold(totalPrice))
        {
            currentItem.ownedCount += quantity;
            ownedCountText.text = "보유: " + currentItem.ownedCount + " / " + currentItem.maxCount;
        }
        else
        {
            Debug.Log("골드가 부족합니다.");
        }
    }
} // 이 중괄호가 빠져있었을 확률이 높습니다!