using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public GameObject itemSlotPrefab;
    public Transform slotParent;
    public ItemData[] items;

    void Start()
    {
        for (int i = 0; i < items.Length; i++)
        {
            GameObject slot = Instantiate(itemSlotPrefab, slotParent);
            slot.GetComponent<ItemSlot>().SetItem(items[i]);
        }
    }
}
