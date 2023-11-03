using System.Collections;
using UnityEngine;

public class Skelly : Zombie
{
    public float lowHealthThreshold = 0.3f;
    private float healthRestoreRate = 0.8f;
    public bool isRestoringHealth = false;
    public bool isTakingDamage = false;
    public Transform healTransform;

    public override void Update()
    {
        base.Update();
        CheckHealth();
    }
    public override void CheckDistance()
    {
        if(!isTakingDamage)
        {
            if (boundary.bounds.Contains(target.transform.position)
             && Vector3.Distance(target.position, transform.position) > attackRadius)
            {
                if (currentState == EnemyState.idle || currentState == EnemyState.walk && currentState != EnemyState.stagger)
                {
                    Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
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
        else if (isTakingDamage)
        {
            // Di chuyển về homePosition
            Debug.Log("Moving towards healTransform");
            Vector3 temp = Vector3.MoveTowards(transform.position, healTransform.position, moveSpeed * 5 * Time.deltaTime);
            changeAnim(temp - transform.position);
            myRigidbody.MovePosition(temp);
        }
        else
        {
            // Random movement when not taking damage
            MoveRandom();
        }
    }    

    private void CheckHealth()
    {
        if (health / maxHealth <= lowHealthThreshold)
        {
            if(!isRestoringHealth)
            {
                isTakingDamage = true;
                if (Vector3.Distance(transform.position, healTransform.position) < 1f)
                {
                    StartCoroutine(RestoreHealthOverTime());
                }
            }    
            

            
        }
    }

    private IEnumerator RestoreHealthOverTime()
    {
        while (health < maxHealth)
        {
            // Hồi máu (1 máu mỗi giây)
            health += healthRestoreRate * Time.deltaTime;
            health = Mathf.Min(health, maxHealth);

            // Cập nhật thanh máu (nếu cần)
            if (healthBar != null)
            {
                healthBar.UpdateHealthBar(health, maxHealth);
            }

            yield return null;
        }

        if (health == maxHealth)
        {
            isRestoringHealth = true;
            isTakingDamage = false;
        }
    }
}
