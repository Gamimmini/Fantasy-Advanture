using System.Collections;
using UnityEngine;

public class MeleeEnemy : log
{
    [Header("Movement Patrol Points")]
    public GameObject pointA;
    public GameObject pointB;
    public GameObject pointC;
    public GameObject pointD;
    protected Transform currentPoint;


    protected override void Start()
    {
        base.Start();
        currentPoint = pointA.transform;   
    }


    protected override void CheckDistance()
    {
        if (Vector3.Distance(target.position, transform.position) <= chaseRadius
             && Vector3.Distance(target.position, transform.position) > attackRadius)
        {
            if (currentState == EnemyState.idle || currentState == EnemyState.walk && currentState != EnemyState.stagger)
            {
                Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * 2 * Time.deltaTime);
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
            if (Vector3.Distance(transform.position, currentPoint.position) < 0.1f)
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
    public IEnumerator AttackCo()
    {
        currentState = EnemyState.attack;
        anim.SetBool("attack", true);
        
        yield return new WaitForSeconds(1f);
        currentState = EnemyState.walk;
        anim.SetBool("attack", false);
    }
    public void Move()
    {
        Vector3 temp = Vector3.MoveTowards(transform.position, currentPoint.position, moveSpeed * Time.deltaTime);
        changeAnim(temp - transform.position);
        myRigidbody.MovePosition(temp);
    }
      
}