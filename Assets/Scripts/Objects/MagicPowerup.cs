using UnityEngine;

public class MagicPowerup : MonoBehaviour
{
    //public Inventory playerInventory;
    public float manaValue;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //DataManager.Instance.currentMana += magicValue;
            MagicManager.Instance.IncreaseMana(manaValue);
            //powerupSignal.Raise();
            Destroy(this.gameObject);
        }
    }
}