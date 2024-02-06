using UnityEngine;

public class Arrow : MonoBehaviour
{
    [Header("Arrow Movement Settings")]
    public float speed;

    [Header("Rigidbody Component")]
    public Rigidbody2D myRigidbody;

    [Header("Arrow Lifetime Settings")]
    public float lifetime;
    private float lifetimeCounter;

    [Header("Mana Value Settings")]
    public float manaValue;
    // public float magicCost;

    [Header("Collision Settings")]
    [SerializeField] public string otherTag;
    void Start()
    {
        lifetimeCounter = lifetime;
    }

    private void Update()
    {
        lifetimeCounter -= Time.deltaTime;
        if (lifetimeCounter <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public virtual void Setup(Vector2 velocity, Vector3 direction)
    {
        myRigidbody.velocity = velocity.normalized * speed;
        transform.rotation = Quaternion.Euler(direction);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(otherTag))
        {
            Destroy(this.gameObject);
        }
    }

}