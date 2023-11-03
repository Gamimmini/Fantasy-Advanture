using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ItemCountManager : MonoBehaviour
{
    public static ItemCountManager Instance;

    public List<ItemData> itemDataList = new List<ItemData>();

    private void Awake()
    {
        Instance = this;
    }

    public void SaveInventory()
    {
        // Tạo một danh sách để lưu thông tin mỗi mục
        itemDataList.Clear();

        foreach (var itemInInventory in FindObjectsOfType<ItemInInventory>())
        {
            var itemData = new ItemData
            {
                itemCode = itemInInventory.itemCode,
                count = itemInInventory.count
            };
            itemDataList.Add(itemData);

        }

        // Chuyển danh sách thành JSON và lưu vào tệp
        string json = JsonUtility.ToJson(itemDataList);
        File.WriteAllText("inventory.json", json);
        Debug.Log("Saved inventory to JSON: " + json);
    }

    public void LoadInventory()
    {
        if (File.Exists("inventory.json"))
        {
            // Đọc JSON từ tệp
            string json = File.ReadAllText("inventory.json");
            itemDataList = JsonUtility.FromJson<List<ItemData>>(json);

            // Cập nhật count của mỗi mục trong danh sách
            foreach (var itemInInventory in FindObjectsOfType<ItemInInventory>())
            {
                var itemData = itemDataList.Find(data => data.itemCode == itemInInventory.itemCode);
                if (itemData != null)
                {
                    itemInInventory.count = itemData.count;
                }
            }
        }
    }
}
