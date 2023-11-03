using UnityEngine;

public class log : Enemy
{
    public Rigidbody2D myRigidbody;

    [Header("Target Variables")]
    public Transform target;
    public float chaseRadius;  // Bán kính để bắt đầu truy đuổi mục tiêu
    public float attackRadius;  // Bán kính để tấn công mục tiêu
    //public Transform homePosition;

    [Header("Animator")]
    public Animator anim;

    public override void Start()
    {
        base.Start();
        currentState = EnemyState.idle;
        myRigidbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        target = GameObject.FindWithTag("Player").transform;
        //anim.SetBool("wakeUp", true);
    }
    void FixedUpdate()
    {
        CheckDistance();
    }
    // Kiểm tra khoảng cách đến mục tiêu và thực hiện hành động tương ứng.
    public virtual void CheckDistance()
    {
        if(Vector3.Distance(target.position,transform.position) <= chaseRadius
            && Vector3.Distance(target.position,transform.position) > attackRadius)
        {
            if (currentState == EnemyState.idle || currentState == EnemyState.walk && currentState != EnemyState.stagger)
            {
                // Tính toán vị trí mới của Enemy để tiến lại gần người chơi
                Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * 1.5f * Time.deltaTime);
                
                // Cập nhật hình ảnh và vị trí của Enemy
                changeAnim(temp - transform.position);
                myRigidbody.MovePosition(temp);
                
                ChangeState(EnemyState.walk);
                anim.SetBool("wakeUp", true);
            }    
        }    
        else if (Vector3.Distance(target.position, transform.position) > chaseRadius)
        {
            anim.SetBool("wakeUp", false);
        }    
    }
    private void SetAnimFloat(Vector2 setVector)
    {
        anim.SetFloat("moveX", setVector.x);
        anim.SetFloat("moveY", setVector.y);
    }    
    public void changeAnim(Vector2 direction)
    {
        if(Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if(direction.x > 0)
            {
                SetAnimFloat(Vector2.right);
            }   
            else if (direction.x < 0)
            {
                SetAnimFloat(Vector2.left);
            }    
        }    
        else if (Mathf.Abs(direction.x) < Mathf.Abs(direction.y))
        {
            if (direction.y > 0)
            {
                SetAnimFloat(Vector2.up);
            }
            else if (direction.y < 0)
            {
                SetAnimFloat(Vector2.down);
            }
        }    
    }    
    public void ChangeState(EnemyState newState)
    {
        if(currentState != newState)
        {
            currentState = newState;
        }    
    }    
}
