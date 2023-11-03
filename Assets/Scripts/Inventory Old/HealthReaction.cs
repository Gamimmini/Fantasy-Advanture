using UnityEngine;

public class HealthReaction : MonoBehaviour
{
    //public FloatValue playerHealth;
    //public SignalSender healthSignal;

    public void Use(float amountToIncrease)
    {
        DataManager.Instance.currentHealth += amountToIncrease;
        if (DataManager.Instance.currentHealth > DataManager.Instance.heartContainers * 2f)
        {
            DataManager.Instance.currentHealth = DataManager.Instance.heartContainers * 2f;
        }
        HeartManager.Instance.UpdateHearts();
        //healthSignal.Raise();
    }
}