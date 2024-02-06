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
    private void GetAllShopItems()
    {
        Transform[] allChildren = GetComponentsInChildren<Transform>(true);

        foreach (Transform child in allChildren)
        {
            ShopItemData itemData = child.GetComponent<ShopItemData>();
            if (itemData != null)
            {
                items.Add(itemData);
            }
        }
    }

    public void SaveQuantity()
    {
        List<ShopItemQuantity> saveDataList = new List<ShopItemQuantity>();

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
        string path = Path.Combine(Application.dataPath, "ShopItemQuantity.json");
        File.WriteAllText(path, jsonData);
    }

    public void LoadQuantity()
    {
        string path = Path.Combine(Application.dataPath, "ShopItemQuantity.json");

        if (File.Exists(path))
        {
            string jsonData = File.ReadAllText(path);
            List<ShopItemQuantity> saveDataList = JsonConvert.DeserializeObject<List<ShopItemQuantity>>(jsonData);

            foreach (ShopItemQuantity saveData in saveDataList)
            {
                ShopItemData itemData = items.Find(x => x.itemID == saveData.itemID);
                if (itemData != null)
                {
                    itemData.quantity = saveData.quantity;
                }
            }
        }
    }

}
