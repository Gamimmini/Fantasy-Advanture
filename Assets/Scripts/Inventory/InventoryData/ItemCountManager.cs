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

        string json = JsonUtility.ToJson(itemDataList);
        File.WriteAllText("inventory.json", json);
        Debug.Log("Saved inventory to JSON: " + json);
    }

    public void LoadInventory()
    {
        if (File.Exists("inventory.json"))
        {
            string json = File.ReadAllText("inventory.json");
            itemDataList = JsonUtility.FromJson<List<ItemData>>(json);

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
