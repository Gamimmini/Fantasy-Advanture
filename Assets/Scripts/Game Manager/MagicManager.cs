using UnityEngine;
using UnityEngine.UI;

public class MagicManager : MonoBehaviour
{
    public static MagicManager Instance;
    public Slider magicSlider;
    //public Data playerInventory;

    private void Awake()
    {
        Instance = this;
    }
    /* public void OnEnable()
    {
        DataManager.Instance.currentMana = DataManager.Instance.maxMana;
        DataManager.Instance.LoadMana();
    } */
    
    void Start()
    {
        magicSlider.interactable = false;
        magicSlider.maxValue = DataManager.Instance.maxMana;
        magicSlider.value = DataManager.Instance.maxMana;
        DataManager.Instance.currentMana = DataManager.Instance.maxMana;
        DataManager.Instance.LoadMana();
        HandleManaIncrease();
        HandleManaReduction();

    }

    public void IncreaseMana(float manaCost)
    {
        DataManager.Instance.currentMana += manaCost;
        if (DataManager.Instance.currentMana > DataManager.Instance.maxMana)
        {
            DataManager.Instance.currentMana = DataManager.Instance.maxMana;
        }
        HandleManaIncrease();
        DataManager.Instance.SaveMana();
    }
    public void ReduceMana(float manaCost)
    {
        if (DataManager.Instance.currentMana > 0)
        {
            DataManager.Instance.currentMana -= manaCost;
            HandleManaReduction();
            DataManager.Instance.SaveMana();

        }
    }

    public void HandleManaIncrease()
    {
        magicSlider.value = DataManager.Instance.currentMana;
        if (magicSlider.value > magicSlider.maxValue)
        {
            magicSlider.value = magicSlider.maxValue;
            DataManager.Instance.currentMana = DataManager.Instance.maxMana;
        }
    }

    public void HandleManaReduction()
    {
        magicSlider.value = DataManager.Instance.currentMana;
        if (magicSlider.value < 0)
        {
            magicSlider.value = 0;
            DataManager.Instance.currentMana = 0;
        }
    }

}