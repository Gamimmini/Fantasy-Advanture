using UnityEngine;

public class Wraith : Zombie
{
    [Header("Move & Attack")]
    private float checkAttack = 0f;
    private bool flashRandom = true;
    private float flachRandomTimer = 0f;
    protected override void CheckDistance()
    {
        if (boundary.bounds.Contains(target.transform.position)
             && Vector3.Distance(target.position, transform.position) > attackRadius && checkAttack == 0f && flachRandomTimer == 0f)
        {
            if (currentState == EnemyState.idle || currentState == EnemyState.walk && currentState != EnemyState.stagger)
            {
                FlashToTarget();
            }
        }
        else if(boundary.bounds.Contains(target.transform.position)
             && Vector3.Distance(target.position, transform.position) > attackRadius && checkAttack <= 3f && checkAttack > 0 && flachRandomTimer == 0f)
        {
            Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * 3f * Time.deltaTime);
            changeAnim(temp - transform.position);
            myRigidbody.MovePosition(temp);
            ChangeState(EnemyState.walk);
        }    
        else if (boundary.bounds.Contains(target.transform.position)
                    && Vector3.Distance(target.position, transform.position) <= attackRadius)
        {
            if (currentState == EnemyState.walk && currentState != EnemyState.stagger)
            {
                StartCoroutine(AttackCo());
                checkAttack += 1;
                //Debug.Log("checkAttack: " + checkAttack);

                if (checkAttack > 3)
                {
                    FlashRandom();
                    if(!flashRandom)
                    {
                        FlashRandom();
                    }    
                }
            }
        }
        else if (!boundary.bounds.Contains(target.transform.position) || 
            boundary.bounds.Contains(target.transform.position)
             && Vector3.Distance(target.position, transform.position) > attackRadius && !flashRandom && checkAttack > 3)
        {
            MoveRandom();
            if (!flashRandom)
            {
                flachRandomTimer += 1 * Time.deltaTime;
                Debug.Log("flachRandomTimer: " + flachRandomTimer);
                if (flachRandomTimer >= 2)
                {
                    checkAttack = 0;
                    flashRandom = true;
                    flachRandomTimer = 0;
                }    
            }
        }
    }
    protected void FlashToTarget()
    {
        Vector3 directionToTarget = (target.position - transform.position).normalized;

        float desiredDistance = 1.0f;

        Vector3 newPosition = target.position - (directionToTarget * desiredDistance);

        myRigidbody.MovePosition(newPosition);

        ChangeState(EnemyState.walk);
        changeAnim(directionToTarget);
    }
    protected void FlashRandom()
    {
        if (flashRandom || checkAttack > 3 && !flashRandom)
        {
            float randomX = Random.Range(boundary.bounds.min.x, boundary.bounds.max.x);
            float randomY = Random.Range(boundary.bounds.min.y, boundary.bounds.max.y);

            Vector3 randomPosition = new Vector3(randomX, randomY, transform.position.z);

            myRigidbody.MovePosition(randomPosition);

            ChangeState(EnemyState.walk);

            Vector3 directionToRandom = (randomPosition - transform.position).normalized;
            changeAnim(directionToRandom);
            flashRandom = false;
        }

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
