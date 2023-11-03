using System.Collections;
using UnityEngine;

public class Zombie : log
{
    public Collider2D boundary;
    public float wanderInterval = 5f; // Thời gian lung tung giữa các hành động
    protected float nextWanderTime;
    protected Vector3 targetPosition;
    protected float cooldownTimer = 0f;

    public virtual void Update()
    {
        // Kiểm tra xem cooldown đã hoàn thành chưa
        if (cooldownTimer <= 0f)
        {
            GetRandomTargetPosition();
            cooldownTimer = 2f; 
        }
        else
        {
            cooldownTimer -= Time.deltaTime; // Giảm thời gian cooldown mỗi frame.
        }
    }
    public override void CheckDistance()
    {
        if (boundary.bounds.Contains(target.transform.position)
             && Vector3.Distance(target.position, transform.position) > attackRadius)
        {
            if (currentState == EnemyState.idle || currentState == EnemyState.walk && currentState != EnemyState.stagger)
            {
                Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * 0.5f * Time.deltaTime);
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
    public IEnumerator AttackCo()
    {
        currentState = EnemyState.attack;
        anim.SetBool("attack", true);

        yield return new WaitForSeconds(1f);
        currentState = EnemyState.walk;
        anim.SetBool("attack", false);
    }
    // Tạo mục tiêu di chuyển lung tung trong boundary
    protected void GetRandomTargetPosition()
    {
        float randomX = Random.Range(boundary.bounds.min.x, boundary.bounds.max.x);
        float randomY = Random.Range(boundary.bounds.min.y, boundary.bounds.max.y);
        targetPosition = new Vector3(randomX, randomY, transform.position.z);
    }
    protected void MoveRandom()
    {
        if (Time.time >= nextWanderTime)
        {
            // Tạo một hướng lung tung ngẫu nhiên
            nextWanderTime = Time.time + wanderInterval;
        }

        // Di chuyển theo hướng lung tung
        Vector3 temp = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        changeAnim(temp - transform.position);
        myRigidbody.MovePosition(temp);
        ChangeState(EnemyState.walk);
    }
}
