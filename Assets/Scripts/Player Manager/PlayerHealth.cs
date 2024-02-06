using UnityEngine;

public class PlayerHealth : GenericHealth
{
    public static PlayerHealth Instance;

    [Header("isInvulnerable")]
    public bool isInvulnerable = false; 
    public float invulnerabilityDuration = 3f; 
    public float cooldownDuration = 35f; 
    public float cooldownTimer = 0.0f; 

    public GameObject invulnerabilityEffect;

    [Header("RecoverArmor")]
    protected float armorRecoveryTimer = 0f;
    private void Awake()
    {
        Instance = this;
    }

    public override void Damage(float amountToDamage)
    {
        if (!isInvulnerable)
        {
            base.Damage(amountToDamage);
            DataManager.Instance.currentHealth = currentHealth;
            DataManager.Instance.currentArmor = currentArmor;
            HeartManager.Instance.UpdateHearts();
            ArmorManager.Instance.HandleArmorIncrease();

            //Debug.Log("Player damaged by " + amountToDamage + " health left: " + currentHealth);
        }
    }


    private void Update()
    {
        if (isInvulnerable)
        {
            //Debug.Log("Invulnerable Duration: " + invulnerabilityDuration);


            invulnerabilityDuration -= Time.deltaTime;

            if (invulnerabilityEffect != null)
            {
                invulnerabilityEffect.SetActive(true);
            }

            if (invulnerabilityDuration <= 0.0f)
            {
                isInvulnerable = false;         
            }
            if (DataManager.Instance.currentHealth <= 0.0f)
            {
                if (invulnerabilityEffect != null)
                {
                    invulnerabilityEffect.SetActive(false);
                }
            }
        }
        else
        {
            if (cooldownTimer > 0.0f)
            {
                //Debug.Log("Invulnerable Cooldown Timer: " + cooldownTimer);
                cooldownTimer -= Time.deltaTime;
                SkillsManager.Instance.UpdateSkillImages();
            }

            if (invulnerabilityEffect != null)
            {
                invulnerabilityEffect.SetActive(false);
            }
        }
    }

    public void ActivateInvulnerability()
    {
        if (!isInvulnerable && cooldownTimer <= 0.0f)
        {
            if (DataManager.Instance.currentMana >= 5f)
            {
                DataManager.Instance.currentMana -= 5f;
                MagicManager.Instance.HandleManaReduction();

                DataManager.Instance.SaveMana();

                isInvulnerable = true;

                invulnerabilityDuration = 3.0f;
                cooldownTimer = cooldownDuration;
            }
        }
    }
}
