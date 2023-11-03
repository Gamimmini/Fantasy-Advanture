using UnityEngine;

public class PlayerHealth : GenericHealth
{
    public static PlayerHealth Instance;

    [Header("isInvulnerable")]
    public bool isInvulnerable = false; // Biến để kiểm tra xem người chơi có trong trạng thái miễn nhiễm sát thương hay không
    public float invulnerabilityDuration = 3f; // Thời gian miễn nhiễm sát thương (3 giây)
    public float cooldownDuration = 35f; // Thời gian hồi chiêu (35 giây)
    public float cooldownTimer = 0.0f; // Biến để đếm thời gian hồi chiêu

    [Header("RecoverArmor")]
    protected float armorRecoveryTimer = 0f;
    private void Awake()
    {
        Instance = this;
    }

    // Ghi đè phương thức Damage từ lớp cha.
    public override void Damage(float amountToDamage)
    {
        if (!isInvulnerable)
        {
            base.Damage(amountToDamage);
            DataManager.Instance.currentHealth = currentHealth;
            DataManager.Instance.currentArmor = currentArmor;
            HeartManager.Instance.UpdateHearts();
            ArmorManager.Instance.HandleArmorIncrease();


            // Debug để kiểm tra khi người chơi bị sát thương và sức khỏe hiện tại.
            //Debug.Log("Player damaged by " + amountToDamage + " health left: " + currentHealth);
        }
    }


    private void Update()
    {
        if (isInvulnerable)
        {
            //Debug.Log("Invulnerable Duration: " + invulnerabilityDuration);

            // Nếu kỹ năng Invulnerable đã được kích hoạt, đếm ngược thời gian
            invulnerabilityDuration -= Time.deltaTime;

            // Kiểm tra nếu thời gian kỹ năng Invulnerable đã kết thúc
            if (invulnerabilityDuration <= 0.0f)
            {
                // Tắt kỹ năng Invulnerable
                isInvulnerable = false;
               
            }
        }
        else
        {
            // Nếu kỹ năng Invulnerable không được kích hoạt, đếm ngược cooldown
            if (cooldownTimer > 0.0f)
            {
                //Debug.Log("Invulnerable Cooldown Timer: " + cooldownTimer);
                cooldownTimer -= Time.deltaTime;
                SkillsManager.Instance.UpdateSkillImages();
            }
        }
    }

    public void ActivateInvulnerability()
    {
        // Kiểm tra xem kỹ năng Invulnerable có sẵn và có trong cooldown không
        if (!isInvulnerable && cooldownTimer <= 0.0f)
        {
            // Kiểm tra xem có đủ mana để kích hoạt kỹ năng không
            if (DataManager.Instance.currentMana >= 5f)
            {
                DataManager.Instance.currentMana -= 5f;
                MagicManager.Instance.HandleManaReduction();
                // Lưu trạng thái mana sau khi đã giảm
                DataManager.Instance.SaveMana();

                // Kích hoạt kỹ năng Invulnerable
                isInvulnerable = true;

                // Đặt lại thời gian tồn tại và cooldown
                invulnerabilityDuration = 3.0f;
                cooldownTimer = cooldownDuration;
            }
        }
    }
}
