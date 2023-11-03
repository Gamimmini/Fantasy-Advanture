using UnityEngine;
using UnityEngine.UI;
public class ButtonInfo : MonoBehaviour
{
    public int ItemID;
    public Text PriceTxt;
    public Text QuantityTxt;
    //public GameObject ShopManager;
    public InventoryItem contents;
    public ItemInInventory itemCode;
    public ShopManager shopItemList;


    void Update()
    {
        ShopItemData itemData = shopItemList.items.Find(x => x.itemID == ItemID);
        if (itemData != null)
        {
            PriceTxt.text = "Price: $" + itemData.price.ToString();
            QuantityTxt.text = "Quantity: " + itemData.quantity.ToString();
            // Hiển thị hình ảnh sản phẩm
            //ItemImage.sprite = itemData.itemSprite;
        }
    }
    public void IncreaseCount()
    {
        InventoryForPlayer.Instance.AddItem(contents, itemCode);
    }
}
