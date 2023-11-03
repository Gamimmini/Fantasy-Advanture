using System;
using System.Collections.Generic;
using UnityEngine;

public class Skills : MonoBehaviour
{
    public static Skills Instance;

    // Danh sách các mục chưa được xử lý
    public List<InventoryItem> itemsToProcess = new List<InventoryItem>();

    // Danh sách các mục mặc định đã xử lý
    public List<InventoryItem> defaultItems = new List<InventoryItem>();

    // Danh sách các mục vũ khí đã xử lý
    public List<InventoryItem> weaponItems = new List<InventoryItem>();

    // Danh sách các mục kỹ năng đã xử lý
    public List<InventoryItem> skillItems = new List<InventoryItem>();
    // Define the event here
    public event Action OnProcessedItemsChanged;

    private void Awake()
    {
        Instance = this;
    }

    // Hàm để xử lý mục từ danh sách chưa được xử lý sang danh sách đã xử lý
    public void ProcessItem(InventoryItem item)
    {
        if (itemsToProcess.Contains(item))
        {
            itemsToProcess.Remove(item);

            if (item.isWeapon)
            {
                if (weaponItems.Count > 0)
                {
                    itemsToProcess.Add(weaponItems[0]); // Thêm phần tử hiện tại của weaponItems vào itemsToProcess
                    weaponItems.Clear(); // Xóa danh sách weaponItems
                }
                weaponItems.Add(item);
                OnProcessedItemsChanged?.Invoke();
            }
            else if (item.isSkill)
            {
                if (skillItems.Count > 0)
                {
                    itemsToProcess.Add(skillItems[0]); // Thêm phần tử hiện tại của skillItems vào itemsToProcess
                    skillItems.Clear(); // Xóa danh sách skillItems
                }
                skillItems.Add(item);
                OnProcessedItemsChanged?.Invoke();
            }
            else
            {
                if (defaultItems.Count > 0)
                {
                    itemsToProcess.Add(defaultItems[0]); 
                    defaultItems.Clear();
                }
                defaultItems.Add(item);
                OnProcessedItemsChanged?.Invoke();
            }
        }
    }
    public bool CheckForWeaponItem(InventoryItem item)
    {
        foreach (InventoryItem itemInInventory in weaponItems)
        {
            if (itemInInventory.itemCode == item.itemCode)
            {
                return true;
            }
        }
        return false;
    }

    // Hàm kiểm tra xem một mục có trong danh sách skillItems hay không
    public bool CheckForSkillItem(InventoryItem item)
    {
        foreach (InventoryItem itemInInventory in skillItems)
        {
            if (itemInInventory.itemCode == item.itemCode)
            {
                return true;
            }
        }
        return false;
    }
}
