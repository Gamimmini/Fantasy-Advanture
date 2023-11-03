
using System.Collections.Generic;
using UnityEngine;


public class ResetManager : MonoBehaviour
{
    public List<BoolValue> resetBoolValue;
    public List<FloatValue> resetFloatValue;
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
