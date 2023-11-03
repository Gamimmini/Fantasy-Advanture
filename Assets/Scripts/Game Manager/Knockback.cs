using UnityEngine;
using DG.Tweening;

public class Knockback : MonoBehaviour
{
    [SerializeField] private float thrust; // Lực đẩy khi va chạm xảy ra
    [SerializeField] private float knockTime; // khoảng thời gian bị đẩy lùi
    [SerializeField] private string otherTag; // chứa tag của đối tượng mà ta muốn tương tác
    //public float damage;
    private void OnTriggerEnter2D(Collider2D other)
    {
        /*
        if (other.gameObject.CompareTag("breakable") && this.gameObject.CompareTag("Player"))
        {
            other.GetComponent<pot>().Smash();
        }
        */
        if (other.gameObject.CompareTag(otherTag) && other.isTrigger)
        {
            Rigidbody2D hit = other.GetComponentInParent<Rigidbody2D>();

            if(hit != null)
            {
                // Tính toán giữa vị trí hiện tại và vị trí của đối tượng bị knockback.
                Vector3 difference = hit.transform.position - transform.position;
                // Chuẩn hóa vector theo lực đẩy
                difference = difference.normalized * thrust;
                // Sử dụng DOTween để đẩy đối tượng bị va chạm (hit) trong khoảng thời gian (knockTime)
                hit.DOMove(hit.transform.position + difference, knockTime);

                if (other.gameObject.CompareTag("enemy") && other.isTrigger)
                {
                    //  gây ra trạng thái stagger cho kẻ thù và gọi hàm Knock để gây sát thương.
                    hit.GetComponent<Enemy>().currentState = EnemyState.stagger;
                    other.GetComponent<Enemy>().Knock(hit, knockTime);
                }
                PlayerMovement playerMovement = other.GetComponentInParent<PlayerMovement>();
                if (playerMovement != null)
                {
                    if (playerMovement.currentState != PlayerState.stagger)
                    {
                        playerMovement.currentState = PlayerState.stagger;
                        playerMovement.Knock(knockTime);
                    }
                }


            }    
        }    
    }  
}
