using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Items")]
[System.Serializable]
public class InventoryItem : ScriptableObject
{
    public ItemCode itemCode;
    public string itemName;
    public string itemDescription;
    public Sprite itemImage;
    //public int numberHeld;
    public bool usable;
    public bool unique;
    public bool isKey;
    public bool isBow;
    public bool isWeapon;
    public bool isSkill;
    public UnityEvent thisEvent;
    public string ItemType;
    public void Use()
    {
        thisEvent.Invoke();
        
    }
    
}