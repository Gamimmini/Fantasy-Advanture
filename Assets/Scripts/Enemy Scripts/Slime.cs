using UnityEngine;

public class Slime : Zombie
{
    [Header("Bulelt Prefab")]
    public GameObject blastPrefab;
    protected float slimeCooldownTimer = 0f;
    protected override void CheckDistance()
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
            GameObject blastObject = Instantiate(blastPrefab, transform.position, Quaternion.identity);
            TrineBlast blast = blastObject.GetComponent<TrineBlast>();

            float blastAngle = i * 30f; 

            Vector2 blastDirection = Quaternion.Euler(0, 0, blastAngle) * Vector2.up;
            blast.Setup(blastDirection, Vector3.zero);
        }
    }
    private float targetFollowSpeed = 0.3f;
    private void FollowTarget()
    {
        Vector3 currentTargetPosition = target.transform.position;
        Vector3 currentSlimePosition = transform.position;

        Vector3 directionToTarget = currentSlimePosition - currentTargetPosition;

        float xChange = Mathf.Lerp(0, directionToTarget.x, targetFollowSpeed * Time.deltaTime);
        float yChange = Mathf.Lerp(0, directionToTarget.y, targetFollowSpeed * Time.deltaTime);

        Vector3 newTargetPosition = new Vector3(currentTargetPosition.x + xChange, currentTargetPosition.y + yChange, currentTargetPosition.z);

        target.transform.position = newTargetPosition;
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
