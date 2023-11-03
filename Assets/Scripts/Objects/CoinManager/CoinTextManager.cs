using TMPro;
using UnityEngine;


public class CoinTextManager : MonoBehaviour
{
    public static CoinTextManager Instance;
    public TextMeshProUGUI coinText;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        coinText.text = DataManager.Instance.coins.ToString();
        //Debug.Log("Coin");
    }

    public void UpdateCoinCount(int v)
    {
        DataManager.Instance.coins += v;
        coinText.text = DataManager.Instance.coins.ToString(); // Chuyển đổi thành chuỗi
        //Debug.Log("Coin2");
    }
}
