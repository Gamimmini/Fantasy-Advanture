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
    public SignalSender roomSignal; 


    protected void Awake()
    {
        // health = maxHealth;
        LoadHealth();
        homePosition = transform.position;  
        healthBar = GetComponentInChildren<FloatingHealthBar>();
    }
    private void OnEnable()
    {
        transform.position = homePosition; 
        //health = maxHealth;
        //healthBar.UpdateHealthBar(health, maxHealth);
        currentState = EnemyState.idle;
    }
    public virtual void TakeDamage(float damage)
    {
        health -= damage;
        healthBar.UpdateHealthBar(health, maxHealth);
        SaveHealth();
        if (health < 0)
        {
            health = 0; 
        }
        if (health <= 0)
        {
            //SaveEnemyData();
            DeathEffect(); 
            MakeLoot();
            if (roomSignal != null)
            {
                roomSignal.Raise();
            }
            this.gameObject.SetActive(false); 
        }    
    }
    protected virtual void MakeLoot()
    {
        if (thisLoot != null)
        {

            Powerup current = thisLoot.LootPowerup(); 
            if (current != null)
            {
                Instantiate(current.gameObject, transform.position, Quaternion.identity); 
            }
        }
    }
    protected virtual void DeathEffect()
    {
        if(deathEffect != null)
        {
            GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity); 
            Destroy(effect, deathEffectDelay); 
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
            myRigidbody.velocity = Vector2.zero; 
            currentState = EnemyState.idle;
            myRigidbody.velocity = Vector2.zero;
        }
    }
    public void SaveHealth()
    {
        if (id > 1000)
        {
            return;
        }
        string dataPath = Application.dataPath + "/EnemyHealthData";

        if (!Directory.Exists(dataPath))
        {
            Directory.CreateDirectory(dataPath);
        }

        EnemyHealthState healthState = new EnemyHealthState
        {
            id = this.id,
            health = this.health
        };

        string json = JsonUtility.ToJson(healthState);

        string filePath = Path.Combine(dataPath, "EnemyHealth_" + id + ".json");

        File.WriteAllText(filePath, json);
    }

    public void LoadHealth()
    {
        string dataPath = Application.dataPath + "/EnemyHealthData";

        string filePath = Path.Combine(dataPath, "EnemyHealth_" + id + ".json");

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);

            EnemyHealthState healthState = JsonUtility.FromJson<EnemyHealthState>(json);
            this.health = healthState.health;

            healthBar.UpdateHealthBar(health, maxHealth);

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
