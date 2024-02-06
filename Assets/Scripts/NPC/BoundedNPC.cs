using UnityEngine;

public class BoundedNPC : Sign
{
    [Header("Movement Settings")]
    private Vector3 directionVector;

    [Header("Transform Settings")]
    private Transform myTransform;

    [Header("Speed Settings")]
    public float speed;

    [Header("Rigidbody Settings")]
    private Rigidbody2D myRigidbody;

    [Header("Animator Settings")]
    private Animator anim;

    [Header("Collider Settings")]
    public Collider2D bounds;

    [Header("Movement State")]
    private bool isMoving;

    [Header("Move Time Settings")]
    public float minMoveTime;
    public float maxMoveTime;
    private float moveTimeSeconds;

    [Header("Wait Time Settings")]
    public float minWaitTime;
    public float maxWaitTime;
    private float waitTimeSeconds;


    void Start()
    {
        moveTimeSeconds = Random.Range(minMoveTime, maxMoveTime);
        waitTimeSeconds = Random.Range(minMoveTime, maxMoveTime);
        anim = GetComponent<Animator>();
        myTransform = GetComponent<Transform>();
        myRigidbody = GetComponent<Rigidbody2D>();
        ChangeDirection();
    }
    public override void Update()
    {
        base.Update();
        if (isMoving)
        {
            moveTimeSeconds -= Time.deltaTime;
            if (moveTimeSeconds <= 0)
            {
                moveTimeSeconds = Random.Range(minMoveTime, maxMoveTime);
                isMoving = false;
            }
            if (!playerInRange)
            {
                Move();
            }
        }
        else
        {
            waitTimeSeconds -= Time.deltaTime;
            if (waitTimeSeconds <= 0)
            {
                ChooseDifferentDirection();
                isMoving = true;
                waitTimeSeconds = Random.Range(minMoveTime, maxMoveTime);
            }
        }
    }
    private void ChooseDifferentDirection()
    {
        Vector3 temp = directionVector;
        ChangeDirection();
        int loops = 0;
        while (temp == directionVector && loops < 100)
        {
            loops++;
            ChangeDirection();
        }
    }

    private void Move()
    {
        Vector3 temp = myTransform.position + directionVector * speed * Time.deltaTime;
        if (bounds.bounds.Contains(temp))
        {
            myRigidbody.MovePosition(temp);
        }
        else
        {
            ChangeDirection();
        }
    }
    void ChangeDirection()
    {
        
        int direction = Random.Range(0, 4);
        switch (direction)
        {
            case 0:
                directionVector = Vector3.right;
                break;
            case 1:
                directionVector = Vector3.up;
                break;
            case 2:
                directionVector = Vector3.left;
                break;
            case 3:
                directionVector = Vector3.down;
                break;
            default:
                break;
        }
        UpdateAnimation();
    }
    void UpdateAnimation()
    {
        anim.SetFloat("MoveX", directionVector.x);
        anim.SetFloat("MoveY", directionVector.y);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        ChooseDifferentDirection();
    }
}
