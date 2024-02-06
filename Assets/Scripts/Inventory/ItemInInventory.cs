using UnityEngine;
public class ItemInInventory : MonoBehaviour
{
    public static ItemInInventory Instance;

    [Header("Item Settings")]
    public InventoryItem itemProfile;
    public ItemCode itemCode;

    [Header("Count Settings")]
    public int count = 0;
    private void Awake()
    {
        Instance = this;
        if (itemProfile != null)
        {
            itemCode = itemProfile.itemCode;
        }
        
    }
    public void SetItemCode(ItemCode code)
    {
        itemCode = code;
    }

    public void IncreaseCount()
    {
        count++;
    }
    public void DecreaseCount()
    {
        if (count > 0)
        {
            count--;
        }
    }
 
}
