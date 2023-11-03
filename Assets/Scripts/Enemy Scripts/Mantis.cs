using UnityEngine;

public class Mantis : Zombie
{
    [Header("Color Change")]
    public Color lowHealthColor;
    // Máu dưới 50% sẽ thay đổi màu
    public float lowHealthThreshold = 0.3f; 

    public SpriteRenderer mySprite;
    public static float speedMultiplier = 1f;

    public override void Start()
    {
        base.Start();
        mySprite = GetComponent<SpriteRenderer>();
    }
  
    public override void CheckDistance()
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
        base.TakeDamage(damage); // Gọi phương thức TakeDamage của class cha
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

}
