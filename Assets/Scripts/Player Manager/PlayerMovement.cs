using System.Collections;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    public PlayerState currentState;

    [Header("Movement Settings")]
    public float speed;
    private Rigidbody2D myRigidbody;
    private Vector3 change;
    private Animator animator;

    [Header("Facing Direction")]
    private Vector2 facingDirection = Vector2.down;

    [Header("Starting Position")]
    public VectorValue startingPosition;

    [Header("Inventory and Skills")]
    public InventoryForPlayer playerInventory;
    public Skills playerSkills;

    [Header("Received Item Display")]
    public SpriteRenderer receivedItemSprite;

    [Header("Signals")]
    public SignalSender playerHit;
    public SignalSender reduceMagic;


    [Header("IFrame Stuff")]
    public Color flashColor;
    public Color regularColor;
    public float flashDuration;
    public int numberOfFlashes;
    public Collider2D triggerCollider;
    public SpriteRenderer mySprite;

    [Header("Prefab Weapon")]
    public GameObject projectile;
    public GameObject poisonPrefab;
    public GameObject trineBlastPrefab;


    private bool canThrowPoison = true;
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
                canThrowPoison = false; 

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
                    Berserker.Instance.ActivateBerserkerSkill();
                    SkillsManager.Instance.UpdateSkillImages();
                }
                else
                {
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
        animator.SetBool("attacking", true);
        currentState = PlayerState.attack;

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
        canThrowPoison = true; 
    }
    private void ThrowPoison()
    {
        if (DataManager.Instance.currentMana > 5)
        {
            if (playerSkills.CheckForWeaponItem(poison))
            {
                GameObject poisonObj = Instantiate(poisonPrefab, transform.position, Quaternion.identity);

                Vector2 throwDirection = new Vector2(animator.GetFloat("moveX"), animator.GetFloat("moveY")).normalized;

                Poison poisonScript = poisonObj.GetComponent<Poison>();

                poisonScript.ThrowPoison(throwDirection);
                DataManager.Instance.currentMana -= 5;
                MagicManager.Instance.HandleManaReduction();
                DataManager.Instance.SaveMana();
            }
        }    
        
    }

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

    public void RaiseItem()
    {
        if (playerInventory.currentItem != null)
        {
            if (currentState != PlayerState.interact)
            {
                animator.SetBool("receive item", true);
                currentState = PlayerState.interact;

                receivedItemSprite.sprite = playerInventory.currentItem.itemImage;
                //receivedItemSprite.sprite = CustomTreasureChest.Instance.customItem.GetComponent<SpriteRenderer>().sprite;

            }
            else
            {
                animator.SetBool("receive item", false);
                currentState = PlayerState.idle;

                receivedItemSprite.sprite = null; 
                playerInventory.currentItem = null;
            }
        }
    }
    void MoveCharacter()
    {
        change.Normalize();
        myRigidbody.MovePosition(transform.position + change * speed * Time.deltaTime);
    }


    public void Knock(float knockTime)
    {
        StartCoroutine(knockCo(knockTime));
    }

    private IEnumerator knockCo(float knockTime)
    {
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

    private IEnumerator FlashCo()
    {
        int temp = 0;
        triggerCollider.enabled = false; 
        while (temp < numberOfFlashes)
        {
            mySprite.color = flashColor;
            yield return new WaitForSeconds(flashDuration);
            mySprite.color = regularColor;
            yield return new WaitForSeconds(flashDuration);
            temp++;
        }
        triggerCollider.enabled = true; 
    }
}
