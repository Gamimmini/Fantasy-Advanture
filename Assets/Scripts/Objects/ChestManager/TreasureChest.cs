using UnityEngine;
using UnityEngine.UI;

public class TreasureChest : Interactable
{
    [Header("Contents")]
    public InventoryItem contents;
    public ItemInInventory itemCode;
    //public Inventory playerInventory;
    public InventoryForPlayer playerInventory;
    public int id;

    public bool isOpen;
    public bool RuntimeValue;

    [Header("Signals and dialogs")]
    public SignalSender raiseItem;
    public GameObject dialogBox;
    public Text dialogText;

    [Header("Animation")]
    protected Animator anim;

    private string filePath;

    void Start()
    {
        anim = GetComponent<Animator>();
        LoadChestData();
        UpdateChestState();
    }
    void Update()
    {
        if (Input.GetButtonDown("attack") && playerInRange)
        {
            if (!isOpen)
            {
                OpenChest();
            }
            else
            {
                ChestAlreadyOpen();
            }
        }
    }
    public virtual void SaveChestData()
    {
        ChestData chestData = new ChestData
        {
            id = this.id,
            isOpen = this.isOpen
        };

        string json = JsonUtility.ToJson(chestData);
        string filePath = "Assets/DataChest/ChestData_" + id + ".json";
        System.IO.File.WriteAllText(filePath, json);

    }
    public virtual void LoadChestData()
    {
        string filePath = "Assets/DataChest/ChestData_" + id + ".json";
        if (System.IO.File.Exists(filePath))
        {
            string json = System.IO.File.ReadAllText(filePath);
            ChestData chestData = JsonUtility.FromJson<ChestData>(json);
            this.isOpen = chestData.isOpen;
        }
    }

    private void UpdateChestState()
    {
        if (isOpen)
        {
            anim.SetBool("opened", true);
        }
    }
    public virtual void OpenChest()
    {
        // Dialog window on
        dialogBox.SetActive(true);
        // dialog text = contents text
        dialogText.text = contents.itemDescription;
        // add contents to the inventory 
        playerInventory.currentItem = contents;
        InventoryForPlayer.Instance.AddItem(contents, itemCode);
        // Raise the signal to the player to animate
        raiseItem.Raise();
        // raise the context clue
        context.Raise();
        // set the chest to opened
        isOpen = true;
        anim.SetBool("opened", true);
        //storedOpen.RuntimeValue = isOpen;
        //ChestData chestData = new ChestData();
        //chestData.RuntimeValue = isOpen;
        SaveChestData();


    }
    public void ChestAlreadyOpen()
    {
        // Dialog off
        dialogBox.SetActive(false);
        //playerInventory.currentItem = null;
        // raise the signal to the player to stop animating
        raiseItem.Raise();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger && !isOpen)
        {
            context.Raise();
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger && !isOpen)
        {
            context.Raise();
            playerInRange = false;
        }
    }
}