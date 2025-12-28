using UnityEngine;
using TMPro;

public class CurrencyManager : MonoBehaviour
{
    public TMP_Text currencyText;

    void Start()
    {
        UpdateUI();
    }

    public bool SpendGold(int amount)
    {
        // DataManager 인스턴스의 골드를 사용
        if (DataManager.instance.gold < amount)
            return false;

        DataManager.instance.gold -= amount;
        UpdateUI();
        return true;
    }

    public void UpdateUI()
    {
        if (DataManager.instance != null)
        {
            currencyText.text = DataManager.instance.gold.ToString("N0");
        }
    }
} // 여기도 마지막 닫는 괄호가 필요합니다.