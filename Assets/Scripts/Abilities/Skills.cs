using System;
using System.Collections.Generic;
using UnityEngine;

public class Skills : MonoBehaviour
{
    public static Skills Instance;

    [Header("Item List")]
    public List<InventoryItem> itemsToProcess = new List<InventoryItem>();
    public List<InventoryItem> defaultItems = new List<InventoryItem>();
    public List<InventoryItem> weaponItems = new List<InventoryItem>();
    public List<InventoryItem> skillItems = new List<InventoryItem>();

    public event Action OnProcessedItemsChanged;

    private void Awake()
    {
        Instance = this;
    }

    public void ProcessItem(InventoryItem item)
    {
        if (itemsToProcess.Contains(item))
        {
            itemsToProcess.Remove(item);

            if (item.isWeapon)
            {
                if (weaponItems.Count > 0)
                {
                    itemsToProcess.Add(weaponItems[0]); 
                    weaponItems.Clear(); 
                }
                weaponItems.Add(item);
                OnProcessedItemsChanged?.Invoke();
            }
            else if (item.isSkill)
            {
                if (skillItems.Count > 0)
                {
                    itemsToProcess.Add(skillItems[0]); 
                    skillItems.Clear(); 
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
