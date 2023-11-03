using UnityEngine;

public class TrineBlast : Arrow
{
    public override void Setup(Vector2 velocity, Vector3 direction)
    {
        // Sử dụng góc tam giác để xác định hướng viên TrineBlast.
        float blastAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        myRigidbody.velocity = velocity.normalized * speed;
        transform.rotation = Quaternion.Euler(0, 0, blastAngle);
    }
}
