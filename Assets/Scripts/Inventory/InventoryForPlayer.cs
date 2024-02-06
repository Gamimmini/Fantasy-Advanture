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
    [Header("List item Count")]
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

    public void CountAndAddItems()
    {
        items.Clear();

        foreach (Transform child in transform)
        {
            ItemInInventory itemInInventory = child.GetComponent<ItemInInventory>();

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

        foreach (ItemInInventory itemInInventory in FindObjectsOfType<ItemInInventory>())
        {
            ItemInInventoryData itemCountData = new ItemInInventoryData
            {
                itemCode = itemInInventory.itemProfile.itemCode,
                count = itemInInventory.count
            };
            itemCountDataList.Add(itemCountData);
        }

        string jsonData = JsonConvert.SerializeObject(itemCountDataList);

        File.WriteAllText(inventoryDataPath, jsonData);
    }

    public void LoadInventoryData()
    {
        if (File.Exists(inventoryDataPath))
        {
            string jsonData = File.ReadAllText(inventoryDataPath);

        
            List<ItemInInventoryData> itemCountDataList = JsonConvert.DeserializeObject<List<ItemInInventoryData>>(jsonData);

            //Debug.Log("Loaded JSON Data: " + jsonData);

            ItemInInventory[] allItemInInventory = FindObjectsOfType<ItemInInventory>();

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
                CountAndAddItems(); 
                SaveInventoryData(); 
                break;
            }
        }
    }

}