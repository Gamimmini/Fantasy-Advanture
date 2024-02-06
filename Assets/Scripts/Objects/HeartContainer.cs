using UnityEngine;

public class HeartContainer : Powerup
{
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !other.isTrigger)
        {
            DataManager.Instance.heartContainers += 1;
            DataManager.Instance.currentHealth = DataManager.Instance.heartContainers * 2;
            powerupSignal.Raise();
            PlayerHealth.Instance.currentHealth = DataManager.Instance.currentHealth;
            HeartManager.Instance.UpdateHearts();
            Destroy(this.gameObject);
        }
    }

}