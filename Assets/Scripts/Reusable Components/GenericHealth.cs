using UnityEngine;

public class GenericHealth : MonoBehaviour
{
    
    public float currentHealth;
   
    public GameObject endGameScreen;

    public float currentArmor;
    protected float maxArmor;


    void Start()
    {
        currentHealth = DataManager.Instance.currentHealth;
        currentArmor = DataManager.Instance.currentArmor;
        maxArmor = DataManager.Instance.maxArmor;
        InvokeRepeating("RecoverArmor", 0f, 3f);
    }
    // hồi phục sức khỏe với lượng sức khỏe cụ thể
    public virtual void Heal(float amountToHeal)
    {
        currentHealth += amountToHeal;

        // Đảm bảo sức khỏe không vượt quá giá trị tối đa.
        if (currentHealth > DataManager.Instance.currentHealth)
        {
            currentHealth = DataManager.Instance.currentHealth;
            
        }
  
    }

    public void RecoverArmor()
    {
        if (currentArmor < maxArmor && currentHealth > 0)
        {
            currentArmor += 1;
           
            if (currentArmor > maxArmor)
            {
                currentArmor = maxArmor;
            }
            DataManager.Instance.currentArmor = currentArmor;
            ArmorManager.Instance.HandleArmorIncrease();
            DataManager.Instance.SaveArmor();
        }    
           

    }    
    // hồi phục đầy đủ sức khỏe.
    public virtual void FullHeal()
    {
        currentHealth = DataManager.Instance.currentHealth;
    }

    // gây sát thương với lượng sát thương cụ thể.
    public virtual void Damage(float amountToDamage)
    {
        if (currentArmor > 0)
        {
            // Trừ giá trị sát thương khỏi giá trị giáp.
            currentArmor -= amountToDamage;
            // Nếu giá trị giáp nhỏ hơn hoặc bằng 0, đặt nó thành 0.
            if (currentArmor <= 0)
            {
                currentArmor = 0;
                ArmorManager.Instance.HandleArmorReduction();
            }
            DataManager.Instance.currentArmor = currentArmor;
            ArmorManager.Instance.HandleArmorReduction();
            DataManager.Instance.SaveArmor();
        }
        else if (currentArmor <= 0)
        {
            // Nếu không còn giáp, trừ sát thương cho sức khỏe.
            currentHealth -= amountToDamage;
            DataManager.Instance.currentHealth = currentHealth;
            // Đảm bảo sức khỏe không nhỏ hơn 0.
            if (currentHealth < 0)
            {
                currentHealth = 0;
                HeartManager.Instance.UpdateHearts();
                DataManager.Instance.SaveHealth();
                CheckHealthStatus();
            }
            HeartManager.Instance.UpdateHearts();
            DataManager.Instance.SaveHealth();
        }
        else if(currentHealth < 0)
        {
            currentHealth = 0;
            CheckHealthStatus();
        }    
    }


    // giết ngay lập tức
    public virtual void InstantDeath()
    {
        currentHealth = 0;
    }
    private void CheckHealthAndHandleEndGame()
    {
        if (currentHealth <= 0)
        {
            // Kích hoạt màn hình EndGame (nếu có)
            if (endGameScreen != null)
            {
                endGameScreen.SetActive(true);
            }
            
        }
    }
    private void CheckHealthStatus()
    {
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            CheckHealthAndHandleEndGame();
        }
    }
}