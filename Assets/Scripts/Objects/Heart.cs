using UnityEngine;

public class Heart : Powerup
{
    public float amountToIncrease;
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            DataManager.Instance.currentHealth += amountToIncrease;
            
            if (DataManager.Instance.currentHealth > DataManager.Instance.heartContainers * 2f)
            {
                DataManager.Instance.currentHealth = DataManager.Instance.heartContainers * 2f;
                PlayerHealth.Instance.currentHealth = DataManager.Instance.currentHealth;
            }
            powerupSignal.Raise();
            PlayerHealth.Instance.currentHealth = DataManager.Instance.currentHealth;
            HeartManager.Instance.UpdateHearts();
            Destroy(this.gameObject);
        }
    }
}
