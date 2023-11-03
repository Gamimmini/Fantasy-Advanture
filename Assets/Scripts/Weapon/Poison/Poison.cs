using UnityEngine;

public class Poison : MonoBehaviour
{
    public GameObject poisonPool; // Kéo prefab vũng độc vào đây
    public float throwSpeed = 4f; // Tốc độ ném

    public void ThrowPoison(Vector2 throwDirection)
    {
        // Thực hiện ném bình độc
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = throwDirection * throwSpeed;

        // Khi bình độc bị phá hủy, tạo vũng độc
        Destroy(gameObject, 1f); // Đặt thời gian phá hủy bình độc (tuỳ chọn)
    }

    private void OnDestroy()
    {
        // Tạo vũng độc tại vị trí bình độc bị phá hủy
        Instantiate(poisonPool, transform.position, Quaternion.identity);
    }

}
