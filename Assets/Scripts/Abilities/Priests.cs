using System.Collections;
using UnityEngine;

public class Priests : MonoBehaviour
{
    public static Priests Instance;

    [Header("Priests")]
    public bool priestsActive = false;
    private float healthRestoreRate = 0.8f;
    private float manaRestoreRate = 6f;
    public float cooldownDuration = 60f;
    public float cooldownTimer = 0f;

    public GameObject recoveryEffect;
    private void Awake()
    {
        Instance = this;
    }
    public void ActivatePriestsSkill()
    {
        if (!priestsActive && cooldownTimer <= 0)
        {
            //Debug.Log("(Priests)");
            priestsActive = true;
            StartCoroutine(RestoreHealthAndMana());
        }
    }

    public IEnumerator RestoreHealthAndMana()
    {
        float restoreTimer = 5f;
        while (restoreTimer > 0)
        {
            if (priestsActive)
            {
                float healthToRestore = healthRestoreRate * Time.deltaTime;
                float manaToRestore = manaRestoreRate * Time.deltaTime;

                DataManager.Instance.currentHealth = Mathf.Min(DataManager.Instance.currentHealth + healthToRestore, DataManager.Instance.heartContainers * 2f);
                PlayerHealth.Instance.currentHealth = DataManager.Instance.currentHealth;
                HeartManager.Instance.UpdateHearts();
                DataManager.Instance.currentMana = Mathf.Min(DataManager.Instance.currentMana + manaToRestore, DataManager.Instance.maxMana);
                MagicManager.Instance.HandleManaIncrease();
                DataManager.Instance.SaveMana();

                restoreTimer -= Time.deltaTime;

                if (recoveryEffect != null)
                {
                    recoveryEffect.SetActive(true);
                }
                //Debug.Log("Recovering health and magic power...");
            }

            yield return null;
        }
        recoveryEffect.SetActive(false);
        priestsActive = false;
        cooldownTimer = cooldownDuration;
    }

    void Update()
    {
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
            SkillsManager.Instance.UpdateSkillImages();
            //Debug.Log("cooldown: " + cooldownTimer + " Seconds");
        }
        if (DataManager.Instance.currentHealth <= 0.0f)
        {
            if (recoveryEffect != null)
            {
                recoveryEffect.SetActive(false);
            }
        }
    }
}
