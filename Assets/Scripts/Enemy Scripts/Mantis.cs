using UnityEngine;

public class Mantis : Zombie
{
    [Header("Status Change")]
    public Color lowHealthColor;
    public float lowHealthThreshold = 0.3f;
    public SpriteRenderer mySprite;
    
    [Header("Speed Change")]
    public static float speedMultiplier = 1f;

    protected override void Start()
    {
        base.Start();
        mySprite = GetComponent<SpriteRenderer>();
    }

    protected override void CheckDistance()
    {
        if (boundary.bounds.Contains(target.transform.position)
             && Vector3.Distance(target.position, transform.position) > attackRadius)
        {
            if (currentState == EnemyState.idle || currentState == EnemyState.walk && currentState != EnemyState.stagger)
            {
                Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * 2f * Time.deltaTime);
                changeAnim(temp - transform.position);
                myRigidbody.MovePosition(temp);
                ChangeState(EnemyState.walk);
            }
        }
        else if (boundary.bounds.Contains(target.transform.position)
                    && Vector3.Distance(target.position, transform.position) <= attackRadius)
        {
            if (currentState == EnemyState.walk && currentState != EnemyState.stagger)
            {
                StartCoroutine(AttackCo());
            }
        }
        else if (!boundary.bounds.Contains(target.transform.position))
        {
            MoveRandom();
        }
    }
    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage); 
        CheckHealth();
        //Debug.Log("CheckHealth is Called: ");
    }
    private void CheckHealth()
    {
        if (health / maxHealth <= lowHealthThreshold)
        {
            mySprite.color = lowHealthColor;
            GenericDamage.Instance.ActivateSkill();
            IncreaseSpeed();
        }
    }
    private void IncreaseSpeed()
    {
        speedMultiplier = 3f;
    }
    protected override void MoveRandom()
    {
        /*
         if (Time.time >= nextWanderTime)
         {
             // Tạo một hướng lung tung ngẫu nhiên
             nextWanderTime = Time.time + wanderInterval;
         }
         */
        Vector3 temp = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        changeAnim(temp - transform.position);
        myRigidbody.MovePosition(temp);
        ChangeState(EnemyState.walk);
    }
}
