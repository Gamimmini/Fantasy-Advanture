using UnityEngine;
using System.IO;
public class Door : Interactable
{
    public static Door Instance;
    [Header("Door variables")]
    public string doorID;
    public DoorType thisDoortype;
    public bool open = false;
    public InventoryForPlayer playerInventory;
    public SpriteRenderer doorSprite;
    public BoxCollider2D physicsCollider;

    [Header("Abilities")]
    public InventoryItem goldKey;
    public InventoryItem silverKey;
    public InventoryItem bronzeKey;

    private void Awake()
    {
        Instance = this;
        LoadDoorState();

    }
    private void Update()
    {
        if(Input.GetButtonDown("attack"))
        {
            if(playerInRange && thisDoortype == DoorType.Silverkey)
            {
                if (playerInventory.CheckForItem(silverKey))
                {
                    // Giảm số lượng SilverKey đi 1
                    playerInventory.DecreaseItemByType("Silver");

                    // Mở cửa
                    Open();
                    SaveDoorState();
                }
            }
            if (playerInRange && thisDoortype == DoorType.Bronzekey)
            {
                if (playerInventory.CheckForItem(bronzeKey))
                {
                    // Giảm số lượng SilverKey đi 1
                    playerInventory.DecreaseItemByType("Bronze");

                    // Mở cửa
                    Open();
                    SaveDoorState();
                }
            }
            if (playerInRange && thisDoortype == DoorType.Goldkey)
            {
                if (playerInventory.CheckForItem(goldKey))
                {
                    // Giảm số lượng SilverKey đi 1
                    playerInventory.DecreaseItemByType("Gold");

                    // Mở cửa
                    Open();
                    SaveDoorState();
                }
            }
        }    
    }
    public void Open()
    {
        doorSprite.enabled = false;
        open = true;
        physicsCollider.enabled = false;
    }   
    public void Close()
    {
        doorSprite.enabled = true;
        open = false;
        physicsCollider.enabled = true;
    }
    public void SaveDoorState()
    {
        DoorState doorState = new DoorState
        {
            doorID = doorID,
            open = open
        };

        string json = JsonUtility.ToJson(doorState);
        string filePath = Path.Combine(Application.dataPath, "DataDoor", $"Door_{doorID}.json");
        File.WriteAllText(filePath, json);
    }

    public void LoadDoorState()
    {
        string filePath = Path.Combine(Application.dataPath, "DataDoor", $"Door_{doorID}.json");
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            DoorState doorState = JsonUtility.FromJson<DoorState>(json);

            open = doorState.open;

            if (open)
            {
                Open();
            }
            else
            {
                Close();
            }
        }
    }
    public string DoorDataFilePath
    {
        get { return Path.Combine(Application.dataPath, "DataDoor", $"Door_{doorID}.json"); }
    }

}
