
using System.Collections.Generic;
using UnityEngine;


public class ResetManager : MonoBehaviour
{
    [Header("Boolean Values to Reset")]
    public List<BoolValue> resetBoolValue;

    [Header("Float Values to Reset")]
    public List<FloatValue> resetFloatValue;

    [Header("Inventories to Reset")]
    public List<Inventory> resetInventories;
    public void ResetBoolValue()
    {
        foreach (var boolValue in resetBoolValue)
        {
            boolValue.RuntimeValue = boolValue.initialValue;
        }
    }
    public void ResetFloatValue()
    {
        foreach (var floatValue in resetFloatValue)
        {
            floatValue.RuntimeValue = floatValue.initialValue;
        }
    }
    public void ResetItems()
    {
        foreach (var inventory in resetInventories)
        {
            inventory.ClearItems();
        }
    }
}
