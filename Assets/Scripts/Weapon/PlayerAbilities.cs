using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    public static PlayerAbilities Instance;

    private void Awake()
    {
        Instance = this; 
    }

    public void MakeArrow(Transform playerTransform)
    {
        if (DataManager.Instance.currentMana > 0)
        {
            // Lấy hướng di chuyển của người chơi và tạo ra một đối tượng mũi tên (Arrow).
            Vector2 temp = new Vector2(playerTransform.GetComponent<Animator>().GetFloat("moveX"), playerTransform.GetComponent<Animator>().GetFloat("moveY"));
            Arrow arrow = Instantiate(playerTransform.GetComponent<PlayerMovement>().projectile, playerTransform.position, Quaternion.identity).GetComponent<Arrow>();
            arrow.Setup(temp, ChooseArrowDirection(playerTransform)); // Thiết lập thông tin cho mũi tên.
            MagicManager.Instance.ReduceMana(arrow.manaValue); 
        }
    }

    public Vector3 ChooseArrowDirection(Transform playerTransform)
    {
        // Xác định hướng mũi tên dựa trên hướng di chuyển của người chơi.
        float temp = Mathf.Atan2(playerTransform.GetComponent<Animator>().GetFloat("moveY"), playerTransform.GetComponent<Animator>().GetFloat("moveX")) * Mathf.Rad2Deg;
        return new Vector3(0, 0, temp);
    }

    public void MakeTrineBlast(Transform playerTransform)
    {
        if (DataManager.Instance.currentMana >= 5)
        {
            // Lấy hướng di chuyển của người chơi và tạo ra một loạt đợt pháo Trine Blast.
            Vector2 playerDirection = new Vector2(playerTransform.GetComponent<Animator>().GetFloat("moveX"), playerTransform.GetComponent<Animator>().GetFloat("moveY")).normalized;

            for (int i = 0; i < 3; i++)
            {
                // Tạo ra một đợt pháo Trine Blast.
                GameObject blastObject = Instantiate(playerTransform.GetComponent<PlayerMovement>().trineBlastPrefab, playerTransform.position, Quaternion.identity);
                TrineBlast blast = blastObject.GetComponent<TrineBlast>();

                float blastAngle;

                // Xác định góc bay của từng đợt pháo trong loạt.
                if (i == 0)
                {
                    blastAngle = 0f;
                }
                else if (i == 1)
                {
                    blastAngle = 30f;
                }
                else
                {
                    blastAngle = -30f;
                }

                // Xác định hướng bay và thiết lập thông tin cho đợt pháo Trine Blast.
                Vector2 blastDirection = Quaternion.Euler(0, 0, blastAngle) * playerDirection;
                blast.Setup(blastDirection, Vector3.zero);
            }

            DataManager.Instance.currentMana -= 5; 
            MagicManager.Instance.HandleManaReduction(); 
            DataManager.Instance.SaveMana(); 
        }

    }
}
