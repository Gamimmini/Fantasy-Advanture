using UnityEngine;

public class PoisonPool : MonoBehaviour
{
    public float damagePerSecond = 1f; // Sát thương gây ra mỗi giây
    public float poolDuration = 3f;     // Thời gian tồn tại của vùng độc

    private float timeElapsed = 0f;     // Thời gian đã trôi qua

    private void OnTriggerStay2D(Collider2D other)
    {
        // Kiểm tra xem đối tượng va chạm có tag "Enemy" (hoặc tag của quái vật) không
        if (other.CompareTag("enemy"))
        {
            // Gây sát thương cho quái vật mỗi giây
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                float modifiedDamage = damagePerSecond * Berserker.Instance.berserkerDamageMultiplier;
                enemy.TakeDamage(modifiedDamage * Time.deltaTime);
                //Debug.Log("Modified Damage: " + modifiedDamage);
            }
        }
    }

    private void Update()
    {
        // Cộng thời gian trôi qua
        timeElapsed += Time.deltaTime;

        // Nếu thời gian trôi qua vượt qua thời gian tồn tại của vùng độc, hủy đối tượng
        if (timeElapsed >= poolDuration)
        {
            Destroy(gameObject);
        }
    }
}
