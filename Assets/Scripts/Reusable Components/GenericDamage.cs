using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class GenericDamage : MonoBehaviour
{
    public static GenericDamage Instance;
    // Lượng sát thương gây ra khi va chạm.
    [SerializeField] public float damage;
    [SerializeField] public string otherTag;
    protected Collider2D triggerCollider;


    public static float damageMultiplier = 1f;
    private void Awake()
    {
        Instance = this;
    }
    public void ActivateSkill()
    {
        // Đặt Damage tăng gấp đôi
        damageMultiplier = 1.5f;
        Debug.Log("ActivateSkill is Called: ");
    }
    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(otherTag) && other.isTrigger)
        {
            GenericHealth temp = other.GetComponent<GenericHealth>();
            if (temp)
            {
                float alteredDamage = damage * damageMultiplier;
                temp.Damage(alteredDamage);
                Debug.Log("Damage: " + alteredDamage);
            }
            if (other.GetComponent<Collider2D>().isTrigger)
            {
                
            }
            Enemy temps = other.GetComponent<Enemy>();
            if (temps)
            {
                float modifiedDamage = damage * Berserker.Instance.berserkerDamageMultiplier;
                temps.TakeDamage(modifiedDamage);
                //Debug.Log("Modified Damage: " + modifiedDamage);
            }
        }
    }
    
}