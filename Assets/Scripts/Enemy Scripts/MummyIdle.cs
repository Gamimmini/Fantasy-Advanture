using UnityEngine;

public class MummyIdle : Enemy
{
    public Transform target;
    public Collider2D boundary;
    public GameObject mummyPrefab; // Kéo và thả Prefab Mummy vào trường này trong trình chỉnh sửa Unity.
    public Transform summonMummy;
    private GameObject mummyInstance; // Lưu trữ thể hiện của Mummy Prefab.
    private float cooldownTimer = 0f;

    void Update()
    {
        // Kiểm tra xem cooldown đã hoàn thành chưa
        if (cooldownTimer <= 0f)
        {
            if (boundary.bounds.Contains(target.transform.position))
            {
                //Debug.Log("Summoning Mummy...");
                SummonMummy();
                cooldownTimer = 5f; // Đặt lại cooldown thành 5 giây.
            }
        }
        else
        {
            cooldownTimer -= Time.deltaTime; // Giảm thời gian cooldown mỗi frame.
        }
    }

    public void SummonMummy()
    {
        // Tạo một id ngẫu nhiên
        int randomId = UnityEngine.Random.Range(1000, 10000);

        // Gán id cho prefab Mummy
        mummyPrefab.GetComponent<Mummy>().id = randomId;
        GameObject mummyObject = Instantiate(mummyPrefab, summonMummy.position, Quaternion.identity);
        Mummy mummyComponent = mummyObject.GetComponent<Mummy>();
        if (mummyComponent != null)
        {
            mummyComponent.CopyBoundaryFromMummyIdle(this); // Copy boundary from MummyIdle
        }
    }    
}
