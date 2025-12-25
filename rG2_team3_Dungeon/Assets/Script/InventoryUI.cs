using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public GameObject itemSlotPrefab;
    public Transform slotParent;
    public int slotCount = 8;

    void Start()
    {
        for (int i = 0; i < slotCount; i++)
        {
            Instantiate(itemSlotPrefab, slotParent);
        }
    }
}
