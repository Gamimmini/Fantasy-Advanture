using UnityEngine;

public class WinLevel : MonoBehaviour
{
    public GameObject changeScene;
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            changeScene.SetActive(true);
            Destroy(this.gameObject);
        }
    }
}
