using UnityEngine;

public class TrineBlast : Arrow
{
    public override void Setup(Vector2 velocity, Vector3 direction)
    {
        float blastAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        myRigidbody.velocity = velocity.normalized * speed;
        transform.rotation = Quaternion.Euler(0, 0, blastAngle);
    }
}
