using UnityEngine;

public class Example : MonoBehaviour
{
    public HealthReaction healthReaction; // Thay thế 'HealthReaction' bằng tên thích hợp

    void Update()
    {
        // Ví dụ: Gọi Use() khi nhấn phím Space
        if (Input.GetKeyDown(KeyCode.M))
        {
            // Gọi hàm Use() và truyền giá trị amountToIncrease
            float amountToIncrease = 10f; // Thay thế giá trị này bằng giá trị mong muốn
            healthReaction.Use(amountToIncrease);
        }
    }
}
