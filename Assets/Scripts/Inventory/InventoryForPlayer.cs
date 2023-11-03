using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using Newtonsoft.Json;


public class InventoryForPlayer : MonoBehaviour
{
    public static InventoryForPlayer Instance;
    [Header("item Profile")]
    public List<ItemInInventory> items;

    [Header("item Check")]
    public InventoryItem currentItem;
    //public List<Item> itemCheck = new List<Item>();
    public int numberOfKeys;

    [Header("Game Object")]
    public InventoryManager inventoryManager;
    [Header("Path")]
    public string inventoryDataPath;

    public List<ItemInInventoryData> itemCounts = new List<ItemInInventoryData>();
    private void Awake()
    {
        Instance = this;
        inventoryDataPath = Application.dataPath + "/inventoryData.json";
        LoadInventoryData();
    }
    void Start()
    {

        CountAndAddItems();
        SaveInventoryData();
    }

    // Hàm này đếm và thêm các GameObject con vào danh sách
    public void CountAndAddItems()
    {
        // Xóa danh sách hiện tại trước khi đếm lại
        items.Clear();

        // Lặp qua tất cả các GameObject con của InventoryForPlayer
        foreach (Transform child in transform)
        {
            // Kiểm tra xem GameObject con có component ItemInInventory không
            ItemInInventory itemInInventory = child.GetComponent<ItemInInventory>();

            // Nếu có, thêm vào danh sách nếu count > 0
            if (itemInInventory != null && itemInInventory.count > 0)
            {
                items.Add(itemInInventory);
            }
        }

    }
    public bool CheckForItem(InventoryItem item)
    {
        foreach (ItemInInventory itemInInventory in items)
        {
            if (itemInInventory.itemProfile.itemCode == item.itemCode)
            {
                return true;
            }
        }
        return false;
    }
    public void AddItem(InventoryItem itemToAdd, ItemInInventory item)
    {
        if (itemToAdd.isKey)
        {
            numberOfKeys++;
            //ItemInInventory.Instance.itemCode = ItemCode.Item5;
            item.IncreaseCount();
            CountAndAddItems();
            SaveInventoryData();


        }
        else if (itemToAdd.isBow)
        {
            //ItemInInventory.Instance.itemCode = ItemCode.Item2;
            item.IncreaseCount();
            CountAndAddItems();
            SaveInventoryData();
        }
        else
        {
            item.IncreaseCount();
            CountAndAddItems();
            SaveInventoryData();
        }    
    }

    public void SaveInventoryData()
    {
        List<ItemInInventoryData> itemCountDataList = new List<ItemInInventoryData>();

        // Lặp qua danh sách các mục và lấy thông tin về số lượng
        foreach (ItemInInventory itemInInventory in FindObjectsOfType<ItemInInventory>())
        {
            ItemInInventoryData itemCountData = new ItemInInventoryData
            {
                itemCode = itemInInventory.itemProfile.itemCode,
                count = itemInInventory.count
            };
            itemCountDataList.Add(itemCountData);
        }

        // Chuyển đổi danh sách này thành chuỗi JSON
        string jsonData = JsonConvert.SerializeObject(itemCountDataList);

        // Lưu chuỗi JSON vào tệp
        File.WriteAllText(inventoryDataPath, jsonData);
    }

    public void LoadInventoryData()
    {
        if (File.Exists(inventoryDataPath))
        {
            // Đọc chuỗi JSON từ tệp
            string jsonData = File.ReadAllText(inventoryDataPath);

            // Chuyển đổi chuỗi JSON thành danh sách các mục và số lượng
            List<ItemInInventoryData> itemCountDataList = JsonConvert.DeserializeObject<List<ItemInInventoryData>>(jsonData);

            //Debug.Log("Loaded JSON Data: " + jsonData); // In chuỗi JSON để kiểm tra

            // Tìm tất cả các ItemInInventory trong scene
            ItemInInventory[] allItemInInventory = FindObjectsOfType<ItemInInventory>();

            // Cập nhật số lượng của từng mục dựa trên dữ liệu đã đọc
            foreach (ItemInInventoryData itemCountData in itemCountDataList)
            {
                ItemInInventory itemInInventory = Array.Find(allItemInInventory, item => item.itemProfile.itemCode == itemCountData.itemCode);
                if (itemInInventory != null)
                {
                    itemInInventory.count = itemCountData.count;
                    //Debug.Log("Updated count for ItemCode " + itemCountData.itemCode + " to " + itemCountData.count);
                }
                else
                {
                    //Debug.LogWarning("ItemInInventory not found for ItemCode: " + itemCountData.itemCode);
                }
            }
        }
        else
        {
            //Debug.LogWarning("InventoryDataPath does not exist: " + inventoryDataPath);
        }
    }
    public void DecreaseItemByType(string itemType)
    {
        foreach (ItemInInventory itemInInventory in items)
        {
            if (itemInInventory.itemProfile.ItemType == itemType)
            {
                itemInInventory.DecreaseCount();
                CountAndAddItems(); // Cập nhật danh sách item
                SaveInventoryData(); // Lưu dữ liệu Inventory
                break; // Đã tìm thấy và giảm count, có thể thoát khỏi vòng lặp
            }
        }
    }

}