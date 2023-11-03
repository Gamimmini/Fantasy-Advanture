
using System.Collections;
using UnityEngine;

public class Wraith : Zombie
{
    private float checkAttack = 0f;
    private bool flashRandom = true;
    private float flachRandomTimer = 0f;
    public override void CheckDistance()
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
                // Tăng giá trị checkAttack dựa trên thời gian
                checkAttack += 1;
                Debug.Log("checkAttack: " + checkAttack);

                // Kiểm tra nếu checkAttack vượt quá 3 thì gọi FlashRandom
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
        // Tính toán vector hướng từ Wraith đến target
        Vector3 directionToTarget = (target.position - transform.position).normalized;

        // Đặt khoảng cách Wraith và target 
        float desiredDistance = 1.0f;

        // Tính toán vị trí mới cho Wraith
        Vector3 newPosition = target.position - (directionToTarget * desiredDistance);

        // Di chuyển Wraith đến vị trí mới
        myRigidbody.MovePosition(newPosition);

        // Thay đổi trạng thái và chạy animation đi bộ
        ChangeState(EnemyState.walk);
        changeAnim(directionToTarget);
    }
    protected void FlashRandom()
    {
        if (flashRandom || checkAttack > 3 && !flashRandom)
        {
            // Tạo vị trí x và y ngẫu nhiên trong boundary
            float randomX = Random.Range(boundary.bounds.min.x, boundary.bounds.max.x);
            float randomY = Random.Range(boundary.bounds.min.y, boundary.bounds.max.y);

            // Tạo một Vector3 từ vị trí x và y ngẫu nhiên, giữ nguyên z của Wraith
            Vector3 randomPosition = new Vector3(randomX, randomY, transform.position.z);

            // Di chuyển Wraith đến vị trí ngẫu nhiên
            myRigidbody.MovePosition(randomPosition);

            // Thay đổi trạng thái và chạy animation đi bộ
            ChangeState(EnemyState.walk);

            // Tính toán hướng di chuyển và chạy animation
            Vector3 directionToRandom = (randomPosition - transform.position).normalized;
            changeAnim(directionToRandom);
            flashRandom = false;
        }

    }

}
