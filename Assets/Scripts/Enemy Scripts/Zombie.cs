using System.Collections;
using UnityEngine;

public class Zombie : log
{
    [Header("Boundary")]
    public Collider2D boundary;

    [Header("Move Random Settings")]
    public float wanderInterval = 5f; 
    protected float nextWanderTime;
    protected Vector3 targetPosition;
    protected float cooldownTimer = 0f;
    protected float currentMoveSpeed = 1f;
    protected float accelerationRate = 0.5f;
    public virtual void Update()
    {
        if (cooldownTimer <= 0f)
        {
            GetRandomTargetPosition();
            cooldownTimer = 4f; 
        }
        else
        {
            cooldownTimer -= Time.deltaTime;
        }
    }
    protected override void CheckDistance()
    {
        if (boundary.bounds.Contains(target.transform.position)
             && Vector3.Distance(target.position, transform.position) > attackRadius)
        {
            if (currentState == EnemyState.idle || currentState == EnemyState.walk && currentState != EnemyState.stagger)
            {
                currentMoveSpeed += Time.deltaTime * accelerationRate;

               // Debug.Log("Current Move Speed: " + currentMoveSpeed);

                Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * 0.3f * currentMoveSpeed * Time.deltaTime);
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
            currentMoveSpeed = 1f;
        }
    }
    public IEnumerator AttackCo()
    {
        currentState = EnemyState.attack;
        anim.SetBool("attack", true);

        yield return new WaitForSeconds(1f);
        currentState = EnemyState.walk;
        anim.SetBool("attack", false);
    }
    protected void GetRandomTargetPosition()
    {
        float randomX = Random.Range(boundary.bounds.min.x, boundary.bounds.max.x);
        float randomY = Random.Range(boundary.bounds.min.y, boundary.bounds.max.y);
        targetPosition = new Vector3(randomX, randomY, transform.position.z);
    }
    protected virtual void MoveRandom()
    {
        /*
        if (Time.time >= nextWanderTime)
        {
            // Tạo một hướng lung tung ngẫu nhiên
            nextWanderTime = Time.time + wanderInterval;
        }
        */
        Vector3 temp = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * 0.2f * Time.deltaTime);
        changeAnim(temp - transform.position);
        myRigidbody.MovePosition(temp);
        ChangeState(EnemyState.walk);
    }
}
