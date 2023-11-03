using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;


public class ShopManager : MonoBehaviour
{
    //public int[,] shopItems = new int[5, 5];
    public List<ShopItemData> items;
    public int coins;
    public TMP_Text CoinsTxt;
    public CoinTextManager coinTextManager;
    public Sprite purchasedItemSprite;

    private void Awake()
    {
        GetAllShopItems();
        
    }
    public void Update()
    {
        coins = DataManager.Instance.coins;
        CoinsTxt.text = "Coins: " + coins.ToString();
        coinTextManager.coinText.text = CoinsTxt.text;
    }
    void Start()
    {
        
        coins = DataManager.Instance.coins;
        CoinsTxt.text = "Coins: " + coins.ToString();
        LoadQuantity();
    }
    public void Buy()
    {

        GameObject ButtonRef = GameObject.FindGameObjectWithTag("Event").GetComponent<EventSystem>().currentSelectedGameObject;
        ButtonInfo buttonInfo = ButtonRef.GetComponent<ButtonInfo>();
        int itemID = ButtonRef.GetComponent<ButtonInfo>().ItemID;

        // Tìm sản phẩm trong danh sách và kiểm tra Quantity
        ShopItemData itemData = items.Find(x => x.itemID == itemID);

        if (itemData != null && coins >= itemData.price && itemData.quantity > 0)
        {
            coins -= itemData.price;
            itemData.quantity--;
            SaveQuantity();
            CoinsTxt.text = "Coins:" + coins.ToString();
            buttonInfo.IncreaseCount();
            coinTextManager.UpdateCoinCount(-itemData.price);
            DataManager.Instance.coins = coins;
            DataManager.Instance.SaveCoin();
        }
    }
    // Hàm để lấy tất cả các thành phần ShopItemData từ các gameObject con
    private void GetAllShopItems()
    {
        // Lấy tất cả các gameObject con của ShopManager
        Transform[] allChildren = GetComponentsInChildren<Transform>(true);

        foreach (Transform child in allChildren)
        {
            // Kiểm tra xem gameObject con có chứa thành phần ShopItemData không
            ShopItemData itemData = child.GetComponent<ShopItemData>();
            if (itemData != null)
            {
                // Nếu có, thêm vào danh sách items
                items.Add(itemData);
            }
        }
    }
    // Phương thức để lưu dữ liệu quantity vào một file JSON
    public void SaveQuantity()
    {
        List<ShopItemQuantity> saveDataList = new List<ShopItemQuantity>();

        // Lặp qua danh sách các mặt hàng và lưu số lượng của mỗi mặt hàng
        foreach (ShopItemData itemData in items)
        {
            ShopItemQuantity saveData = new ShopItemQuantity
            {
                itemID = itemData.itemID,
                quantity = itemData.quantity
            };

            saveDataList.Add(saveData);
        }

        string jsonData = JsonConvert.SerializeObject(saveDataList);
        // Lưu dữ liệu JSON vào tệp
        string path = Path.Combine(Application.dataPath, "ShopItemQuantity.json");
        File.WriteAllText(path, jsonData);
        //Debug.Log("Data saved successfully.");
    }

    // Phương thức để tải dữ liệu quantity từ file JSON
    public void LoadQuantity()
    {
        string path = Path.Combine(Application.dataPath, "ShopItemQuantity.json");

        if (File.Exists(path))
        {
            string jsonData = File.ReadAllText(path);

            // Deserializing JSON để tải số lượng mặt hàng
            List<ShopItemQuantity> saveDataList = JsonConvert.DeserializeObject<List<ShopItemQuantity>>(jsonData);

            foreach (ShopItemQuantity saveData in saveDataList)
            {
                // Tìm mặt hàng trong danh sách dựa trên itemID và cập nhật số lượng
                ShopItemData itemData = items.Find(x => x.itemID == saveData.itemID);
                if (itemData != null)
                {
                    itemData.quantity = saveData.quantity;
                }
            }
        }

    }
}
