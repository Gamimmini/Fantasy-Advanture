using System.Collections;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    public PlayerState currentState;
    public float speed;
    private Rigidbody2D myRigidbody;
    private Vector3 change;
    private Animator animator;

    //[SerializeField] private GenericAbility currentAbility;
    private Vector2 facingDirection = Vector2.down;

    // VectorValue lưu trữ vị trí xuất phát của người chơi
    public VectorValue startingPosition;

    // Danh sách các vật phẩm trong hòm đồ của người chơi
    public InventoryForPlayer playerInventory;
    public Skills playerSkills;

    // SpriteRenderer để hiển thị hình ảnh vật phẩm nhận được
    public SpriteRenderer receivedItemSprite; 

    // SignalSender để gửi tín hiệu khi người chơi bị tấn công
    public SignalSender playerHit;

    // SignalSender để gửi tín hiệu khi người chơi tiêu hao năng lượng ma thuật
    public SignalSender reduceMagic;

    // Các thuộc tính liên quan đến hiệu ứng tạm thời IFrame (chớp chớp khi bị tấn công)
    [Header("IFrame Stuff")]
    public Color flashColor;
    public Color regularColor;
    public float flashDuration;
    public int numberOfFlashes;
    public Collider2D triggerCollider;
    public SpriteRenderer mySprite;

    //
    [Header("Prefab Weapon")]
    public GameObject projectile;
    public GameObject poisonPrefab;
    public GameObject trineBlastPrefab;

    // Biến kiểm tra xem có thể ném độc hay không
    private bool canThrowPoison = true;
    // Thời gian giữa các lần ném độc (1.2 giây)
    private float throwCooldown = 1.2f; 

    [Header("Weapon")]
    public InventoryItem bow;
    public InventoryItem poison;
    public InventoryItem scepter;

    [Header("Abilities")]
    public InventoryItem berserker;
    public InventoryItem invulnerable; 
    public InventoryItem priests;
    void Start()
    {
        currentState = PlayerState.walk;
        animator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        animator.SetFloat("moveX", 0);
        animator.SetFloat("moveY", -1);
        transform.position = startingPosition.initialValue; 
    }

    void Update()
    {
        if (currentState == PlayerState.interact)
        {
            return;
        }    
        //change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");
        if(Input.GetButtonDown("attack") && currentState != PlayerState.attack && currentState != PlayerState.stagger)
        {
            StartCoroutine(AttackCo());
        }
        else if (Input.GetButtonDown("Attack2") && currentState != PlayerState.attack && currentState != PlayerState.stagger)
        {
           if (playerSkills.CheckForWeaponItem(bow))
           {
               StartCoroutine(SecondAttackCo());
           }
            if (canThrowPoison && playerSkills.CheckForWeaponItem(poison))
            {
                ThrowPoison();
                canThrowPoison = false; // Tắt khả năng ném độc

                // Đặt thời gian đếm ngược cho cooldown
                StartCoroutine(ResetThrowCooldown());
            }
            if (playerSkills.CheckForWeaponItem(scepter))
            {
                StartCoroutine(SecondAttackCo());
            }
        }
        else if (Input.GetButtonDown("Ability") && currentState != PlayerState.attack && currentState != PlayerState.stagger)
        {
            if (playerSkills.CheckForSkillItem(berserker))
            {
                if (GenericDamage.Instance != null)
                {
                    // Kiểm tra xem GenericDamagePlayer.Instance có tồn tại trước khi gọi phương thức
                    Berserker.Instance.ActivateBerserkerSkill();
                    SkillsManager.Instance.UpdateSkillImages();
                }
                else
                {
                    // Xử lý trường hợp GenericDamagePlayer.Instance là null
                    Debug.LogWarning("GenericDamagePlayer.Instance is null. Make sure it is properly initialized.");
                }
            }
            if (playerSkills.CheckForSkillItem(invulnerable))
            {
                PlayerHealth.Instance.ActivateInvulnerability();
                SkillsManager.Instance.UpdateSkillImages();
            }
            if (playerSkills.CheckForSkillItem(priests))
            {
                Priests.Instance.ActivatePriestsSkill();
                SkillsManager.Instance.UpdateSkillImages();
            }

        }
        else if (currentState == PlayerState.walk || currentState == PlayerState.idle)
        {
           UpdateAnimationAndMove();
        }
    }

    private IEnumerator AttackCo()
    {
        // Bắt đầu trạng thái tấn công và kích hoạt hoạt ảnh tấn công.
        animator.SetBool("attacking", true);
        currentState = PlayerState.attack;

        // Chờ một khung hình, sau đó tắt hoạt ảnh tấn công và trả lại trạng thái ban đầu
        yield return null;
        animator.SetBool("attacking", false);
        yield return new WaitForSeconds(0.3f);

        // 
        if (currentState != PlayerState.interact)
        {
            currentState = PlayerState.walk;
        }        
    }
    private IEnumerator SecondAttackCo()
    {
        currentState = PlayerState.attack;

        // Thực hiện hành động tấn công phụ
        yield return null;

        if (playerSkills.CheckForWeaponItem(bow))
        {
            MakeArrow();
        }
        if (playerSkills.CheckForWeaponItem(scepter))
        {
            MakeTrineBlast();
        }
        yield return new WaitForSeconds(.4f);
        //
        if (currentState != PlayerState.interact)
        {
            currentState = PlayerState.walk;
        }
    }


    private void MakeArrow()
    {
        PlayerAbilities.Instance.MakeArrow(transform);
    }

    private void MakeTrineBlast()
    {
        PlayerAbilities.Instance.MakeTrineBlast(transform);
    }
    private IEnumerator ResetThrowCooldown()
    {
        yield return new WaitForSeconds(throwCooldown);
        canThrowPoison = true; // Cho phép ném độc lại sau khi cooldown kết thúc
    }
    private void ThrowPoison()
    {
        if (DataManager.Instance.currentMana > 5)
        {
            // Kiểm tra xem người chơi có bình độc không
            if (playerSkills.CheckForWeaponItem(poison))
            {

                // Tạo đối tượng bình độc từ prefab
                GameObject poisonObj = Instantiate(poisonPrefab, transform.position, Quaternion.identity);

                // Lấy hướng ném từ hướng mặt của người chơi
                Vector2 throwDirection = new Vector2(animator.GetFloat("moveX"), animator.GetFloat("moveY")).normalized;

                // Lấy component Poison từ đối tượng bình độc
                Poison poisonScript = poisonObj.GetComponent<Poison>();

                // Gọi phương thức ThrowPoison của đối tượng Poison và truyền hướng ném
                poisonScript.ThrowPoison(throwDirection);
                DataManager.Instance.currentMana -= 5;
                MagicManager.Instance.HandleManaReduction();
                DataManager.Instance.SaveMana();
            }
        }    
        
    }

    // Cập nhật hoạt ảnh và di chuyển của player dựa trên thay đổi hướng di chuyển.
    void UpdateAnimationAndMove()
    {
        if (change != Vector3.zero)
        {
            MoveCharacter();
            change.x = Mathf.Round(change.x);
            change.y = Mathf.Round(change.y);
            animator.SetFloat("moveX", change.x);
            animator.SetFloat("moveY", change.y);
            animator.SetBool("moving", true);
        }
        else
        {
            animator.SetBool("moving", false);
        }
    }

    // Hàm được gọi khi player nhận một món đồ mới. Nó xử lý việc hiển thị món đồ được nhận.
    public void RaiseItem()
    {
        if (playerInventory.currentItem != null)
        {
            if (currentState != PlayerState.interact)
            {
                animator.SetBool("receive item", true);
                currentState = PlayerState.interact;

                // Hiển thị hình ảnh món đồ nhận được.
                receivedItemSprite.sprite = playerInventory.currentItem.itemImage;
                //receivedItemSprite.sprite = CustomTreasureChest.Instance.customItem.GetComponent<SpriteRenderer>().sprite;

            }
            else
            {
                animator.SetBool("receive item", false);
                currentState = PlayerState.idle;

                receivedItemSprite.sprite = null;  // Xóa hình ảnh món đồ khi không còn tương tác.
                playerInventory.currentItem = null; // Đặt món đồ hiện tại của người chơi thành null.
            }
        }
    }
    void MoveCharacter()
    {
        change.Normalize(); // Chuẩn hóa vectơ
        myRigidbody.MovePosition(transform.position + change * speed * Time.deltaTime);
    }

    // Hàm `Knock` được gọi khi player bị tấn công và trừ health. Nó xử lý hành động của người chơi sau khi bị đánh.
    public void Knock(float knockTime)
    {
        StartCoroutine(knockCo(knockTime));
    }

    // xử lý hành động của người chơi sau khi bị đánh.
    private IEnumerator knockCo(float knockTime)
    {
        // Kích hoạt sự kiện tấn công và kích hoạt hiệu ứng nhấp nháy.
        //playerHit.Raise();
        if (myRigidbody != null)
        {
            StartCoroutine(FlashCo());
            yield return new WaitForSeconds(knockTime);
            myRigidbody.velocity = Vector2.zero;
            currentState = PlayerState.idle;
            myRigidbody.velocity = Vector2.zero;
        }
    }

    // tạo hiệu ứng nhấp nháy khi người chơi bị đánh.
    private IEnumerator FlashCo()
    {
        int temp = 0;
        triggerCollider.enabled = false;  // Tắt collider để ngăn người chơi bị đánh liên tiếp.
        while (temp < numberOfFlashes)
        {
            mySprite.color = flashColor;
            yield return new WaitForSeconds(flashDuration);
            mySprite.color = regularColor;
            yield return new WaitForSeconds(flashDuration);
            temp++;
        }
        triggerCollider.enabled = true; // Bật lại collider sau khi hiệu ứng nhấp nháy kết thúc.
    }
}
