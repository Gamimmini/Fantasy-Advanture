using UnityEngine;
using UnityEngine.UI;
public class ArmorManager : MonoBehaviour
{
    public static ArmorManager Instance;
    public Slider armorSlider;
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        armorSlider.maxValue = DataManager.Instance.maxArmor;
        armorSlider.value = DataManager.Instance.currentArmor;
        //DataManager.Instance.currentArmor = DataManager.Instance.maxArmor;
        DataManager.Instance.LoadArmor();
        HandleArmor();

    }
    public void HandleArmorIncrease()
    {
        armorSlider.value = DataManager.Instance.currentArmor;
        if (armorSlider.value > armorSlider.maxValue)
        {
            armorSlider.value = armorSlider.maxValue;
            DataManager.Instance.currentArmor = DataManager.Instance.maxArmor;
        }
    }

    public void HandleArmorReduction()
    {
        armorSlider.value = DataManager.Instance.currentArmor;
        if (armorSlider.value < 0)
        {
            armorSlider.value = 0;
            DataManager.Instance.currentArmor = 0;
        }
    }
    public void HandleArmor()
    {
        HandleArmorIncrease();
        HandleArmorReduction();
    }    
}
