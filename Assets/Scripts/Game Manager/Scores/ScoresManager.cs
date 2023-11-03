using UnityEngine;

public class ScoresManager : MonoBehaviour
{
    public static ScoresManager Instance;

    private void Awake()
    {
        Instance = this;
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            DataManager.Instance.scores += 1;
            DataManager.Instance.SaveScores();
            Destroy(this.gameObject);
        }
    }
}
