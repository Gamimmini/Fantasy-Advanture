using UnityEngine;

public class Berserker : MonoBehaviour
{
    public static Berserker Instance;

    [Header("Berserker Damage")]
    public float berserkerDamageMultiplier = 1.0f;

    [Header("Berserker Cooldown")]
    public bool isBerserkerActive = false; 
    public float berserkerDuration = 4.0f; 
    public float berserkerCooldown = 30.0f; 
    public float berserkerCooldownTimer = 0.0f;

    [Header("Berserker Effect")]
    public GameObject berserkerEffect;

    private Vector3 originalPlayerScale;
    private void Awake()
    {
        Instance = this;
        //berserkerDamageMultiplier = damage;

        originalPlayerScale = GameObject.FindGameObjectWithTag("Player").transform.localScale;
    }
    private void Update()
    {
        if (isBerserkerActive)
        {
            //Debug.Log("Berserker Duration: " + berserkerDuration);
            berserkerDuration -= Time.deltaTime;
            if (berserkerEffect != null)
            {
                berserkerEffect.SetActive(true);
            }
            if (berserkerDuration <= 0.0f)
            {
                isBerserkerActive = false;
                berserkerDamageMultiplier = 1.0f;
            }
        }
        else
        {
            if (berserkerCooldownTimer > 0.0f)
            {
                //Debug.Log("Berserker Cooldown Timer: " + berserkerCooldownTimer);
                berserkerCooldownTimer -= Time.deltaTime;
                SkillsManager.Instance.UpdateSkillImages();
            }
            if (berserkerEffect != null)
            {
                berserkerEffect.SetActive(false);
            }
        }
        if (DataManager.Instance.currentHealth <= 0.0f)
        {
            if (berserkerEffect != null)
            {
                berserkerEffect.SetActive(false);
            }
        }
    }

    public void ActivateBerserkerSkill()
    {
        if (!isBerserkerActive && berserkerCooldownTimer <= 0.0f)
        {
            if (DataManager.Instance.currentMana >= 5f)
            {
                DataManager.Instance.currentMana -= 5f;
                MagicManager.Instance.HandleManaReduction();
                isBerserkerActive = true;

                berserkerDamageMultiplier = Mathf.Min(berserkerDamageMultiplier * 2.0f, 5.0f);

                berserkerDuration = 3.0f;
                berserkerCooldownTimer = berserkerCooldown;
            }    
                
        }
    }
}
