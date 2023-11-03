using UnityEngine;

public class PhysicalInventoryItem : MonoBehaviour
{
    //[SerializeField] private PlayerInventory playerInventory;
    [SerializeField] private InventoryItem thisItem;
    [SerializeField] private ItemInInventory itemInInventoryCount;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !other.isTrigger)
        {
            AddItemToInventory();
            Destroy(this.gameObject);
        }
    }

    /*
    void AddItemToInventory()
    {
        if (playerInventory && thisItem)
        {
            if (playerInventory.myInventory.Contains(thisItem))
            {
                //thisItem.numberHeld += 1;
            }
            else
            {
                playerInventory.myInventory.Add(thisItem);
                //thisItem.numberHeld += 1;
            }
        }
    }
    */
    void AddItemToInventory()
    {
        if (itemInInventoryCount && thisItem)
        {
            // Tăng giá trị count trong ItemInInventory
            itemInInventoryCount.IncreaseCount(); 
            InventoryForPlayer.Instance.CountAndAddItems();
            InventoryForPlayer.Instance.SaveInventoryData();
        }
    }
}