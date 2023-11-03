using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;



[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory/Player Inventory")]

public class PlayerInventory : ScriptableObject
{
    public List<InventoryItem> myInventory = new List<InventoryItem>();
}