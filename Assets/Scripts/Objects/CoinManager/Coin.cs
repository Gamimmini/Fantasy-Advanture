using UnityEngine;

public class Coin : Powerup
{
    public int value;
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            CoinTextManager.Instance.UpdateCoinCount(value);
            DataManager.Instance.SaveCoin();
            Destroy(this.gameObject);
        }
    }
}
