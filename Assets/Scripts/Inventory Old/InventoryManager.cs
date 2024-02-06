using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{

    [Header("Inventory Information")]
    //public PlayerInventory playerInventory;
    [SerializeField] public GameObject blankInventorySlot;
    [SerializeField] public GameObject inventoryPanel;
    [SerializeField] public TextMeshProUGUI descriptionText;
    [SerializeField] public GameObject useButton;
    public InventoryItem currentItem;
    [SerializeField] private InventoryForPlayer inventoryForPlayer;
    //[SerializeField] private Skills skills;

    public void SetTextAndButton(string description, bool buttonActive)
    {
        descriptionText.text = description;
        if (buttonActive)
        {
            useButton.SetActive(true);
        }
        else
        {
            useButton.SetActive(false);
        }
    }
    public virtual void MakeInventorySlots()
    {
        if (inventoryForPlayer)
        {
            List<ItemInInventory> inventoryItems = inventoryForPlayer.items; 

            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (inventoryItems[i].count > 0)
                {
                    GameObject temp = Instantiate(blankInventorySlot, inventoryPanel.transform.position, Quaternion.identity);
                    temp.transform.SetParent(inventoryPanel.transform);
                    InventorySlot newSlot = temp.GetComponent<InventorySlot>();
                    if (newSlot)
                    {
                        newSlot.Setup(inventoryItems[i], this); 
                    }
                    //InventoryForPlayer.Instance.SaveInventoryData();
                }
            }
        }
    }

    void OnEnable()
    {
        ClearInventorySlots();
        MakeInventorySlots();
        //Debug.Log("made inventory");
        SetTextAndButton("", false);
    }

    public void SetupDescriptionAndButton(string newDescriptionString, bool isButtonUsable, InventoryItem newItem)
    {
        currentItem = newItem;
        descriptionText.text = newDescriptionString;
        useButton.SetActive(isButtonUsable);
    }
    void ClearInventorySlots()
    {
        for (int i = 0; i < inventoryPanel.transform.childCount; i++)
        {
            Destroy(inventoryPanel.transform.GetChild(i).gameObject);
        }
    }

    public void SetCurrentItem(InventoryItem newItem)
    {
        currentItem = newItem;
    }

    public void UseButtonPressed()
    {
        if (inventoryForPlayer && currentItem)
        {
            foreach (ItemInInventory item in inventoryForPlayer.items)
            {
                if (item.itemProfile == currentItem)
                {
                    if (currentItem.ItemType == "heal")
                    {
                        DataManager.Instance.currentHealth += 1;
                        if (DataManager.Instance.currentHealth > DataManager.Instance.heartContainers * 2f)
                        {
                            DataManager.Instance.currentHealth = DataManager.Instance.heartContainers * 2f;
                            PlayerHealth.Instance.currentHealth = DataManager.Instance.currentHealth;
                            HeartManager.Instance.UpdateHearts();
                        }
                        PlayerHealth.Instance.currentHealth = DataManager.Instance.currentHealth;
                        HeartManager.Instance.UpdateHearts();
                        DataManager.Instance.SaveHealth();
                    }
                    else if (currentItem.ItemType == "mana")
                    {
                        DataManager.Instance.currentMana += 10;
                        if (DataManager.Instance.currentMana > DataManager.Instance.maxMana)
                        {
                            DataManager.Instance.currentMana = DataManager.Instance.maxMana;
                            MagicManager.Instance.HandleManaIncrease();
                        }
                        MagicManager.Instance.HandleManaIncrease();
                        DataManager.Instance.SaveMana();
                    }
                    else if (currentItem.ItemType == "Berserker" || currentItem.ItemType == "invulnerable" || currentItem.ItemType == "Priests")
                    {
                        if (Berserker.Instance.isBerserkerActive == false && Berserker.Instance.berserkerCooldownTimer <= 0
                            && Priests.Instance.priestsActive == false && Priests.Instance.cooldownTimer <= 0
                            && PlayerHealth.Instance.isInvulnerable == false && PlayerHealth.Instance.cooldownTimer <= 0)
                        {
                            Skills.Instance.ProcessItem(currentItem);
                            
                        }
                        else
                        {
                            //Debug.Log("skill is on cooldown.");
                        }
                    }
                    else if (currentItem.ItemType == "Bow")
                    {
                        Skills.Instance.ProcessItem(currentItem);
                    }
                    else if (currentItem.ItemType == "Poison")
                    {
                        Skills.Instance.ProcessItem(currentItem);
                    }
                    else if (currentItem.ItemType == "Scepter")
                    {
                        Skills.Instance.ProcessItem(currentItem);
                    }
                    if (currentItem.ItemType != "Berserker" && currentItem.ItemType != "Bow" 
                        && currentItem.ItemType != "invulnerable" && currentItem.ItemType != "Priests"
                        && currentItem.ItemType != "Poison" && currentItem.ItemType != "Scepter")
                    {
                        item.DecreaseCount();
                    }
                    InventoryForPlayer.Instance.CountAndAddItems();
                    ClearInventorySlots();
                    MakeInventorySlots();
                    InventoryForPlayer.Instance.SaveInventoryData();
                    break;
                }
            }
        }
    }
}