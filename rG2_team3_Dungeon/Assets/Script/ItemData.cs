using UnityEngine;

[CreateAssetMenu(menuName = "Shop/Item")]
public class ItemData : ScriptableObject
{
    public string id; // 저장을 위한 고유 ID (예: item_01, item_02)
    public string itemName;
    public Sprite icon;
    public string description;
    public int price;
    public int maxCount; // 최대 구매 가능 개수 추가

    [HideInInspector]
    public int ownedCount; // 실시간 보유량 (DataManager에서 관리하게 됨)
}