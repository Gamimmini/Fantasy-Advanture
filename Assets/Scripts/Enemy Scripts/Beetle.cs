using System.Collections;
using UnityEngine;

public class Beetle : log
{
    public GameObject pointA;
    public GameObject pointB;
    public GameObject pointC;
    public GameObject pointD;
    public GameObject point1;
    public GameObject point2;
    public GameObject point3;
    public GameObject point4;
    private Transform currentPoint;
    private Transform playerPoint;
    private bool hasAttacked = false;

    public float resetInterval = 30.0f; // Time interval to reset currentPoint
    private float lastResetTime; // Track the last reset time
    public override void Start()
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
                currentPoint = pointA.transform; // Reset currentPoint to pointA
                lastResetTime = Time.time; // Update the last reset time
            }
            // Lựa chọn một trong các điểm ngẫu nhiên từ point1 đến point4
            int randomPointIndex = Random.Range(1, 5); // 1 <= randomPointIndex < 5

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
                    playerPoint = point1.transform; // Mặc định là point1 nếu có lỗi xảy ra
                    break;
            }

            yield return new WaitForSeconds(3.5f); // Đợi 3.5 giây trước khi chọn lại điểm ngẫu nhiên
        }
    }
    public override void CheckDistance()
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
                    // Đến điểm A hoặc B, thực hiện chuyển đổi currentPoint
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
        base.TakeDamage(damage); // Gọi phương thức TakeDamage của class cha

        // Đặt hasAttacked thành false khi kẻ thù bị trừ máu
        hasAttacked = false;
    }

}
