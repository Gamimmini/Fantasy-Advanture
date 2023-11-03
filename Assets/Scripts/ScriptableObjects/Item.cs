using UnityEngine;


[CreateAssetMenu]
[System.Serializable]
public class Item : ScriptableObject
{
    public Sprite itemSprite;
    public string itemDescription;
    public bool isKey;
    public bool isBow;
    public int itemCode;
}
