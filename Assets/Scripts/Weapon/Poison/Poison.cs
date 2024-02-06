using UnityEngine;

public class Poison : MonoBehaviour
{
    [Header("Poison Settings")]
    public GameObject poisonPool;
    public float throwSpeed = 4f;

    public void ThrowPoison(Vector2 throwDirection)
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = throwDirection * throwSpeed;
        Destroy(gameObject, 1f);
    }

    private void OnDestroy()
    {
        Instantiate(poisonPool, transform.position, Quaternion.identity);
    }
}
