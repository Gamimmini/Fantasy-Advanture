using UnityEngine;

public class Slime : Zombie
{
    public GameObject blastPrefab;
    protected float slimeCooldownTimer = 0f;
    public override void CheckDistance()
    {
        if (boundary.bounds.Contains(target.transform.position))
        {
            if (currentState == EnemyState.idle || currentState == EnemyState.walk && currentState != EnemyState.stagger)
            {
                //FollowTarget();
                if (slimeCooldownTimer <= 0f)
                {
                    StartCoroutine(AttackCo());
                    MakeBlast();
                    
                    slimeCooldownTimer = Random.Range(2f, 4f);

                }
                else
                {
                    slimeCooldownTimer -= Time.deltaTime;
                    //Debug.Log(slimeCooldownTimer);
                    MoveRandom();
                }
            }
            
        }
        else if (!boundary.bounds.Contains(target.transform.position))
        {

            MoveRandom();
        }
        
    }
    
    public void MakeBlast()
    {
        for (int i = 0; i < 12; i++)
        {
            // Tạo đạn Trine Blast
            GameObject blastObject = Instantiate(blastPrefab, transform.position, Quaternion.identity);
            TrineBlast blast = blastObject.GetComponent<TrineBlast>();

            float blastAngle = i * 30f; 

            // Xác định hướng bay và thiết lập thông tin cho đạn Trine Blast.
            Vector2 blastDirection = Quaternion.Euler(0, 0, blastAngle) * Vector2.up; // Đạn sẽ bắn lên theo góc blastAngle
            blast.Setup(blastDirection, Vector3.zero);
        }
    }
    private float targetFollowSpeed = 0.3f;
    private void FollowTarget()
    {
        Vector3 currentTargetPosition = target.transform.position;
        Vector3 currentSlimePosition = transform.position;

        // Tính toán hướng kéo target về Slime
        Vector3 directionToTarget = currentSlimePosition - currentTargetPosition;

        // Xác định x và y cần thay đổi dựa trên tốc độ và thời gian
        float xChange = Mathf.Lerp(0, directionToTarget.x, targetFollowSpeed * Time.deltaTime);
        float yChange = Mathf.Lerp(0, directionToTarget.y, targetFollowSpeed * Time.deltaTime);

        // Tạo vị trí mới của target
        Vector3 newTargetPosition = new Vector3(currentTargetPosition.x + xChange, currentTargetPosition.y + yChange, currentTargetPosition.z);

        // Di chuyển target đến vị trí mới
        target.transform.position = newTargetPosition;
    }

}
