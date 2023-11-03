using System.Collections;
using UnityEngine;
using System.IO;
public class Enemy : MonoBehaviour
{
    public static Enemy Instance;

    public int id;

    [Header("State Machine")]
    public EnemyState currentState;

    [Header("Enemy Stats Health")]
    public float maxHealth;
    public float health;
    [SerializeField] protected FloatingHealthBar healthBar;

    [Header("Enemy Stats Basic")]
    public string enemyName;
    public int baseAttack;
    public float moveSpeed;
    public Vector2 homePosition;

    [Header("Death Effects")]
    public GameObject deathEffect;
    private float deathEffectDelay = 1f;
    public LootTable thisLoot;

    [Header("Death Signals")]
    public SignalSender roomSignal; // Tín hiệu được gửi khi kẻ thù chết


    protected void Awake()
    {
        // health = maxHealth;
        LoadHealth();
        // Gán vị trí ban đầu của kẻ thù
        homePosition = transform.position;  
        healthBar = GetComponentInChildren<FloatingHealthBar>();
    }
    private void OnEnable()
    {
        // Đặt lại vị trí của kẻ thù khi nó được kích hoạt
        transform.position = homePosition; 
        //health = maxHealth;
        //healthBar.UpdateHealthBar(health, maxHealth);
        currentState = EnemyState.idle;
    }
    public virtual void Start()
    {
    }
    public virtual void TakeDamage(float damage)
    {
        health -= damage;
        healthBar.UpdateHealthBar(health, maxHealth);
        SaveHealth();
        if (health < 0)
        {
            health = 0; // Đảm bảo health không thể âm.
        }
        if (health <= 0)
        {
            //SaveEnemyData();
            DeathEffect(); 
            MakeLoot();
            if (roomSignal != null)
            {
                // Gửi tín hiệu khi kẻ thù chết.
                roomSignal.Raise();
            }
            this.gameObject.SetActive(false); 
        }    
    }
    // Tạo các vật phẩm thưởng
    private void MakeLoot()
    {
        if (thisLoot != null)
        {
            // Lấy một vật phẩm thưởng ngẫu nhiên từ danh sách thưởng
            Powerup current = thisLoot.LootPowerup(); 
            if (current != null)
            {
                Instantiate(current.gameObject, transform.position, Quaternion.identity); // Tạo vật phẩm thưởng
            }
        }
    }
    // Khi sức khỏe dưới 0, kích hoạt hiệu ứng chết
    private void DeathEffect()
    {
        if(deathEffect != null)
        {
            GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity); // Tạo hiệu ứng chết
            Destroy(effect, deathEffectDelay); // Hủy hiệu ứng sau một khoảng thời gian
        }    
    }    
    public void Knock(Rigidbody2D myRigidbody, float knockTime)
    {
        StartCoroutine(knockCo(myRigidbody, knockTime));
        //TakeDamage(damage);
    }    
    private IEnumerator knockCo(Rigidbody2D myRigidbody, float knockTime)
    {
        if (myRigidbody != null)
        {

            yield return new WaitForSeconds(knockTime);
            // Đặt vận tốc của Rigidbody về 0 để kết thúc knockback
            myRigidbody.velocity = Vector2.zero; 
            currentState = EnemyState.idle;
            myRigidbody.velocity = Vector2.zero;
        }
    }
    // Save enemy data to a JSON file
    public void SaveHealth()
    {
        if (id > 1000)
        {
            // Không lưu dữ liệu cho các ID lớn hơn 1000
            return;
        }
        // Xác định đường dẫn đầy đủ đến thư mục "EnemyHealthData" trong thư mục Assets
        string dataPath = Application.dataPath + "/EnemyHealthData";

        // Kiểm tra nếu thư mục không tồn tại, hãy tạo nó
        if (!Directory.Exists(dataPath))
        {
            Directory.CreateDirectory(dataPath);
        }

        // Tạo đối tượng chứa thông tin sức khỏe
        EnemyHealthState healthState = new EnemyHealthState
        {
            id = this.id,
            health = this.health
        };

        // Chuyển đối tượng thành chuỗi JSON
        string json = JsonUtility.ToJson(healthState);

        // Xác định đường dẫn đầy đủ đến tệp JSON
        string filePath = Path.Combine(dataPath, "EnemyHealth_" + id + ".json");

        // Ghi dữ liệu JSON vào tệp
        File.WriteAllText(filePath, json);
    }

    public void LoadHealth()
    {
        // Xác định đường dẫn đầy đủ đến thư mục "EnemyHealthData" trong thư mục Assets
        string dataPath = Application.dataPath + "/EnemyHealthData";

        // Xác định đường dẫn đầy đủ đến tệp JSON
        string filePath = Path.Combine(dataPath, "EnemyHealth_" + id + ".json");

        if (File.Exists(filePath))
        {
            // Đọc dữ liệu từ tệp JSON
            string json = File.ReadAllText(filePath);

            // Chuyển đổi dữ liệu JSON thành đối tượng sức khỏe
            EnemyHealthState healthState = JsonUtility.FromJson<EnemyHealthState>(json);
            this.health = healthState.health;

            // Cập nhật thanh máu sau khi load sức khỏe
            healthBar.UpdateHealthBar(health, maxHealth);

            // Kiểm tra nếu health dưới 0, đặt health thành 0 và tắt GameObject
            if (health <= 0)
            {
                health = 0;
                this.gameObject.SetActive(false);
            }
        }
        else
        {
            //Debug.Log("No health data found for ID " + id);
        }
    }


}
