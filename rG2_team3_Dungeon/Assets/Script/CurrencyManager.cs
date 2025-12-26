using UnityEngine;
using TMPro;

public class CurrencyManager : MonoBehaviour
{
    public int currentGold = 1000;
    public TMP_Text currencyText;

    void Start()
    {
        UpdateUI();
    }

    public bool SpendGold(int amount)
    {
        if (currentGold < amount)
            return false;

        currentGold -= amount;
        UpdateUI();
        return true;
    }

    void UpdateUI()
    {
        currencyText.text = currentGold.ToString();
    }
}
