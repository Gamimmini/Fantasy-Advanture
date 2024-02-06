using System.Collections;
using UnityEngine;

public class Beetle : log
{
    [Header("Movement Patrol Points")]
    public GameObject pointA;
    public GameObject pointB;
    public GameObject pointC;
    public GameObject pointD;

    [Header("Enemy Movement Points")]
    public GameObject point1;
    public GameObject point2;
    public GameObject point3;
    public GameObject point4;
    private Transform currentPoint;
    private Transform playerPoint;
    private bool hasAttacked = false;

    [Header("Reset Interval")]
    public float resetInterval = 30.0f; 
    private float lastResetTime;
    protected override void Start()
    {
        base.Start();
        lastResetTime = Time.time;
        StartCoroutine(RandomizePlayerPoint());
        currentPoint = pointA.transform;
    }
    IEnumerator RandomizePlayerPoint()
    {
        while (true)
        {
            if (Time.time - lastResetTime >= resetInterval)
            {
                currentPoint = pointA.transform; 
                lastResetTime = Time.time; 
            }
   
            int randomPointIndex = Random.Range(1, 5); 

            switch (randomPointIndex)
            {
                case 1:
                    playerPoint = point1.transform;
                    break;
                case 2:
                    playerPoint = point2.transform;
                    break;
                case 3:
                    playerPoint = point3.transform;
                    break;
                case 4:
                    playerPoint = point4.transform;
                    break;
                default:
                    playerPoint = point1.transform;
                    break;
            }

            yield return new WaitForSeconds(3.5f); 
        }
    }
    protected override void CheckDistance()
    {
        if (!hasAttacked)
        {
            if (Vector3.Distance(target.position, transform.position) <= chaseRadius
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
            else if (Vector3.Distance(target.position, transform.position) <= chaseRadius
                        && Vector3.Distance(target.position, transform.position) <= attackRadius)
            {
                if (currentState == EnemyState.walk && currentState != EnemyState.stagger)
                {
                        StartCoroutine(AttackCo());
                }
            }
            else if (Vector3.Distance(target.position, transform.position) > chaseRadius)
            {
                if (Vector3.Distance(transform.position, currentPoint.position) < 1.2f)
                {
                    if (currentPoint == pointA.transform)
                    {
                        currentPoint = pointB.transform;
                    }
                    else if (currentPoint == pointB.transform)
                    {
                        if (pointC == null)
                        {
                            currentPoint = pointA.transform;
                        }
                        else
                        {
                            currentPoint = pointC.transform;
                        }
                    }
                    else if (currentPoint == pointC.transform)
                    {
                        currentPoint = pointD.transform;
                    }
                    else if (currentPoint == pointD.transform)
                    {
                        currentPoint = pointA.transform;
                    }
                }
                Move();
            }
        }
        else if (hasAttacked)
        {
            if (Vector3.Distance(target.position, transform.position) >= 3f)
            {
                hasAttacked = false;
            }
            else
            {
                Vector3 temp = Vector3.MoveTowards(transform.position, playerPoint.position, moveSpeed * 4f * Time.deltaTime);
                changeAnim(temp - transform.position);
                myRigidbody.MovePosition(temp);
            }
        }


    }
    public IEnumerator AttackCo()
    {
        currentState = EnemyState.attack;
        anim.SetBool("attack", true);
        yield return new WaitForSeconds(1f);
        currentState = EnemyState.walk;
        anim.SetBool("attack", false);
        hasAttacked = true;
        //Debug.Log("AttackCo completed");
    }

    public void Move()
    {
        Vector3 temp = Vector3.MoveTowards(transform.position, currentPoint.position, moveSpeed * Time.deltaTime);
        changeAnim(temp - transform.position);
        myRigidbody.MovePosition(temp);

    }
    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);

        hasAttacked = false;
    }

}
