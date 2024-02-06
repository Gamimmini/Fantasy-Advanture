using UnityEngine;

public class Goblin : Zombie
{
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
