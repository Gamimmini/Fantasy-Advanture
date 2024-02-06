using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
//[System.Serializable]
public class Inventory : ScriptableObject
{
    [Header("Current Item")]
    public Item currentItem;

    [Header("Item List")]
    public List<Item> items = new List<Item>();

    [Header("Key and Coins")]
    public int numberOfKeys;
    public int coins;

    [Header("Magic")]
    public float maxMagic = 10;
    public float currentMagic;


    public void OnEnable()
    {
        currentMagic = maxMagic;
    }

    public void ReduceMagic(float magicCost)
    {
        currentMagic -= magicCost;
    }

    public bool CheckForItem(Item item)
    {
        if (items.Contains(item))
        {
            return true;
        }
        return false;
    }
    public void AddItem(Item itemToAdd)
    {
        if(itemToAdd.isKey)
        {
            numberOfKeys++;
        }
        else
        {
            if (!items.Contains(itemToAdd))
            {
                items.Add(itemToAdd);
            }    
        }    
    }
    public void ClearItems()
    {
        items.Clear();
    }

}
