using UnityEngine;

public class PoisonPool : MonoBehaviour
{
    [Header("Poison Pool Settings")]
    public float damagePerSecond = 1f; 
    public float poolDuration = 3f; 

    [Header("Internal Timer")]
    private float timeElapsed = 0f;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                float modifiedDamage = damagePerSecond * Berserker.Instance.berserkerDamageMultiplier;
                enemy.TakeDamage(modifiedDamage * Time.deltaTime);
            }
        }
    }

    private void Update()
    {
        timeElapsed += Time.deltaTime;

        if (timeElapsed >= poolDuration)
        {
            Destroy(gameObject);
        }
    }
}
