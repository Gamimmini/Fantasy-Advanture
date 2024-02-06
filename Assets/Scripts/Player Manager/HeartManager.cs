
using UnityEngine;
using UnityEngine.UI;

public class HeartManager : MonoBehaviour
{
    public static HeartManager Instance;

    [Header("Heart Manager Settings")]
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite halfFullHeart;
    public Sprite emptyHeart;
    //public FloatValue heartContainers;
    //public FloatValue playerCurrentHealth;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        InitHearts();
        UpdateHearts();
    }

    public void InitHearts()
    {
        for (int i = 0; i < DataManager.Instance.heartContainers; i++)
        {
            if(i < hearts.Length)
            {
                hearts[i].gameObject.SetActive(true);
                hearts[i].sprite = fullHeart;
            }    
           
        }
    }

    public void UpdateHearts()
    {
        //DataManager.Instance.LoadHealth();
        InitHearts();
        
        float tempHealth = DataManager.Instance.currentHealth / 2;
        for (int i = 0; i < DataManager.Instance.heartContainers; i++)
        {
            if (i <= tempHealth - 1)
            {
                //Full Heart
                hearts[i].sprite = fullHeart;
            }
            else if (i >= tempHealth)
            {
                //empty heart
                hearts[i].sprite = emptyHeart;
            }
            else
            {
                //half full heart
                hearts[i].sprite = halfFullHeart;
            }
        }
        DataManager.Instance.SaveHealth();   
    }

}