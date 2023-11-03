using System.Collections;
using UnityEngine;

public class Priests : MonoBehaviour
{
    public static Priests Instance;
    public bool priestsActive = false;
    private float healthRestoreRate = 0.8f; // 4 máu 5 giây
    private float manaRestoreRate = 6f; // 30 mana 5 giây
    public float cooldownDuration = 60f; // Thời gian cooldown
    public float cooldownTimer = 0f; // Đếm thời gian cooldown
    

    private void Awake()
    {
        Instance = this;
    }
    // Hàm để kích hoạt kỹ năng của Linh mục (Priests)
    public void ActivatePriestsSkill()
    {
        if (!priestsActive && cooldownTimer <= 0)
        {
            Debug.Log("(Priests)");
            priestsActive = true;
            StartCoroutine(RestoreHealthAndMana());
        }
    }

    // Coroutine để gradually restore sức khỏe và ma lực trong vòng 5 giây
    public IEnumerator RestoreHealthAndMana()
    {
        float restoreTimer = 5f;
        while (restoreTimer > 0)
        {
            if (priestsActive)
            {
                // Phục hồi sức khỏe và ma lực từ từ
                float healthToRestore = healthRestoreRate * Time.deltaTime;
                float manaToRestore = manaRestoreRate * Time.deltaTime;

                // Thêm giá trị đã phục hồi vào sức khỏe và ma lực của người chơi
                DataManager.Instance.currentHealth = Mathf.Min(DataManager.Instance.currentHealth + healthToRestore, DataManager.Instance.heartContainers * 2f);
                PlayerHealth.Instance.currentHealth = DataManager.Instance.currentHealth;
                HeartManager.Instance.UpdateHearts();
                DataManager.Instance.currentMana = Mathf.Min(DataManager.Instance.currentMana + manaToRestore, DataManager.Instance.maxMana);
                MagicManager.Instance.HandleManaIncrease();
                DataManager.Instance.SaveMana();

                restoreTimer -= Time.deltaTime;
                Debug.Log("Recovering health and magic power...");
            }

            yield return null;
        }

        // Đặt lại trạng thái của kỹ năng
        priestsActive = false;
        cooldownTimer = cooldownDuration;
    }

    void Update()
    {
        // Cập nhật bộ đếm thời gian cooldown
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
            SkillsManager.Instance.UpdateSkillImages();
            Debug.Log("cooldown: " + cooldownTimer + " Seconds");
        }
    }
}
