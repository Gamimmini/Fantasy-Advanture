using UnityEngine;

public class Berserker : MonoBehaviour
{
    public static Berserker Instance;

    public float berserkerDamageMultiplier = 1.0f; // Độ gia tăng Damage cho kỹ năng Berserker
    public bool isBerserkerActive = false; // Xác định xem kỹ năng Berserker đã được kích hoạt chưa
    public float berserkerDuration = 4.0f; // Thời gian tồn tại của kỹ năng Berserker (4 giây)
    public float berserkerCooldown = 30.0f; // Thời gian hồi chiêu cho kỹ năng Berserker (30 giây)
    public float berserkerCooldownTimer = 0.0f; // Thời gian đếm ngược cho cooldown

    private void Awake()
    {
        Instance = this;
        //berserkerDamageMultiplier = damage;
    }
    private void Update()
    {
        if (isBerserkerActive)
        {
            //Debug.Log("Berserker Duration: " + berserkerDuration);
            // Nếu kỹ năng Berserker đã được kích hoạt, đếm ngược thời gian
            berserkerDuration -= Time.deltaTime;

            // Kiểm tra nếu thời gian kỹ năng Berserker đã kết thúc
            if (berserkerDuration <= 0.0f)
            {
                // Tắt kỹ năng Berserker và cập nhật Damage về giá trị ban đầu
                isBerserkerActive = false;
                berserkerDamageMultiplier = 1.0f;
            }
        }
        else
        {
            // Nếu kỹ năng Berserker không được kích hoạt, đếm ngược cooldown
            if (berserkerCooldownTimer > 0.0f)
            {
                //Debug.Log("Berserker Cooldown Timer: " + berserkerCooldownTimer);
                berserkerCooldownTimer -= Time.deltaTime;
                SkillsManager.Instance.UpdateSkillImages();
            }
        }
    }

    public void ActivateBerserkerSkill()
    {
        // Kiểm tra xem kỹ năng Berserker có sẵn và có trong cooldown không
        if (!isBerserkerActive && berserkerCooldownTimer <= 0.0f)
        {
            if (DataManager.Instance.currentMana >= 5f)
            {
                DataManager.Instance.currentMana -= 5f;
                MagicManager.Instance.HandleManaReduction();
                // Kích hoạt kỹ năng Berserker
                isBerserkerActive = true;

                // Đặt Damage tăng gấp đôi (đừng vượt quá giới hạn 5)
                berserkerDamageMultiplier = Mathf.Min(berserkerDamageMultiplier * 2.0f, 5.0f);

                // Đặt lại thời gian tồn tại và cooldown
                berserkerDuration = 3.0f;
                berserkerCooldownTimer = berserkerCooldown;
            }    
                
        }
    }
}
