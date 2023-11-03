using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class GenericDamagePlayer : GenericDamage
{
    /*
    public static GenericDamagePlayer Instance;


    private float berserkerDamageMultiplier = 1.0f; // Độ gia tăng Damage cho kỹ năng Berserker
    private bool isBerserkerActive = false; // Xác định xem kỹ năng Berserker đã được kích hoạt chưa
    private float berserkerDuration = 10.0f; // Thời gian tồn tại của kỹ năng Berserker (3 giây)
    private float berserkerCooldown = 30.0f; // Thời gian hồi chiêu cho kỹ năng Berserker (30 giây)
    private float berserkerCooldownTimer = 0.0f; // Thời gian đếm ngược cho cooldown

    private void Awake()
    {
        Instance = this;
        //Debug.Log("GenericDamagePlayer Instance is assigned: " + (Instance != null));
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(otherTag) && other.isTrigger)
        {
            float modifiedDamage = damage * berserkerDamageMultiplier;
            Debug.Log("Modified Damage: " + modifiedDamage);
            GenericHealth temp = other.GetComponent<GenericHealth>();
            if (temp)
            {
                temp.Damage(modifiedDamage);
            }
            Enemy temps = other.GetComponent<Enemy>();
            if (temps)
            {
                temps.TakeDamage(modifiedDamage);
            }

        }
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
                //Debug.Log("Berserker Cooldown: " + berserkerCooldown);
                //Debug.Log("Berserker Cooldown Timer: " + berserkerCooldownTimer);
                berserkerCooldownTimer -= Time.deltaTime;
            }
        }
    }

    public void ActivateBerserkerSkill()
    {
        // Kiểm tra xem kỹ năng Berserker có sẵn và có trong cooldown không
        if (!isBerserkerActive && berserkerCooldownTimer <= 0.0f)
        {
            // Kích hoạt kỹ năng Berserker
            isBerserkerActive = true;
            Debug.Log("Berserker skill is now active!");
            // Đặt Damage tăng gấp đôi (đừng vượt quá giới hạn 5)
            berserkerDamageMultiplier = Mathf.Min(berserkerDamageMultiplier * 2.0f, 5.0f);

            // Đặt lại thời gian tồn tại và cooldown
            berserkerDuration = 10.0f;
            berserkerCooldownTimer = berserkerCooldown;
        }
    }
    */
}