using UnityEngine;
using TMPro;

public class QuantityUI : MonoBehaviour
{
    public TMP_Text quantityText;
    public int quantity = 1;

    ShopUI shopUI;

    void Start()
    {
        shopUI = FindObjectOfType<ShopUI>();
        UpdateText();
    }

    public void Increase()
    {
        quantity++;
        UpdateText();
    }

    public void Decrease()
    {
        if (quantity > 1)
            quantity--;

        UpdateText();
    }

    public void Buy()
    {
        shopUI.BuyItem(quantity);
    }

    void UpdateText()
    {
        quantityText.text = quantity.ToString();
    }
}
