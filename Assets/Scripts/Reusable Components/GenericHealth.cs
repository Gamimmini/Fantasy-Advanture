using UnityEngine;

public class GenericHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public float currentHealth;

    [Header("End Game Screen")]
    public GameObject endGameScreen;

    [Header("Armor Settings")]
    public float currentArmor;
    protected float maxArmor;

    [Header("Damage Effect")]
    public GameObject damageEffect;
    private float damageEffectDuration = 0.4f;
    void Start()
    {
        currentHealth = DataManager.Instance.currentHealth;
        currentArmor = DataManager.Instance.currentArmor;
        maxArmor = DataManager.Instance.maxArmor;
        InvokeRepeating("RecoverArmor", 0f, 3f);
    }
    public virtual void Heal(float amountToHeal)
    {
        currentHealth += amountToHeal;

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
    public virtual void FullHeal()
    {
        currentHealth = DataManager.Instance.currentHealth;
    }
  

    public virtual void Damage(float amountToDamage)
    {
        //Debug.Log("Phương thức Damage được gọi với lượng: " + amountToDamage);

        if (amountToDamage > 0)
        {
            //Debug.Log("Đang nhận sát thương!");
            if (damageEffect != null && DataManager.Instance.currentHealth > 0)
            {
                //Debug.Log("Kích hoạt hiệu ứng sát thương");
                damageEffect.SetActive(true);
                Invoke("DeactivateDamageEffect", damageEffectDuration);
            }
            if (DataManager.Instance.currentHealth <= 0 || currentHealth <= 0)
            {
                damageEffect.SetActive(false);
            }
        }
        

        if (currentArmor > 0)
        {
            currentArmor -= amountToDamage;
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
            currentHealth -= amountToDamage;
            DataManager.Instance.currentHealth = currentHealth;
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
        if(currentHealth <= 0 || DataManager.Instance.currentHealth <= 0)
        {
            currentHealth = 0;
            CheckHealthStatus();
        }    
    }

    private void DeactivateDamageEffect()
    {
        if (damageEffect != null)
        {
            damageEffect.SetActive(false);
        }
    }

    public virtual void InstantDeath()
    {
        currentHealth = 0;
    }
    private void CheckHealthAndHandleEndGame()
    {
        if (currentHealth <= 0 || DataManager.Instance.currentHealth <= 0)
        {
            if (endGameScreen != null)
            {
                endGameScreen.SetActive(true);
            }
            //Destroy(gameObject);
           Time.timeScale = 0f;
        }
    }
    private void CheckHealthStatus()
    {
        if (currentHealth <= 0 || DataManager.Instance.currentHealth <= 0)
        {
            currentHealth = 0;
            if (endGameScreen != null)
            {
                endGameScreen.SetActive(true);
            }
            CheckHealthAndHandleEndGame();
        }
    }
}