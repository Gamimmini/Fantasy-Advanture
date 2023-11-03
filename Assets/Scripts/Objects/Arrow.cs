using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed;
    public Rigidbody2D myRigidbody;
    public float lifetime;
    private float lifetimeCounter;
    //public float magicCost;
    public float manaValue;

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