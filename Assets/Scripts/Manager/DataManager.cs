using UnityEngine;
using System.IO;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    [Header("Scores")]
    public int scores = 0;

    [Header("Coin")]
    public int coins = 0;

    [Header("Health")]
    public float heartContainers;
    public float currentHealth;

    [Header("Armor")]
    public float maxArmor;
    public float currentArmor;
    

    [Header("Mana")]
    public float maxMana;
    public float currentMana;

    private void Awake()
    {
        Instance = this;
        LoadCoin();
        LoadHealth();
        LoadMana();
        LoadScores();
        LoadArmor();
    }
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.U))
        {
            LogInfo();
        }
    }
    void LogInfo()
    {
        string cpuInfo = SystemInfo.processorType;
        float cpuSpeed = SystemInfo.processorFrequency;
        Debug.Log("CPU: " + cpuInfo + " @ " + cpuSpeed + " GHz");

        int ramSize = SystemInfo.systemMemorySize;
        Debug.Log("RAM: " + ramSize + " MB");

        string gpuInfo = SystemInfo.graphicsDeviceName;
        int vramSize = SystemInfo.graphicsMemorySize;
        Debug.Log("GPU: " + gpuInfo + " - VRAM: " + vramSize + " MB");


    }

    public void SaveCoin()
    {
        CoinData coinData = new CoinData();
        coinData.coins = coins;

        string json = JsonUtility.ToJson(coinData, true);

        File.WriteAllText(Application.dataPath + "/coinData.json", json);
    }
    public void LoadCoin()
    {
        string filePath = Application.dataPath + "/coinData.json";
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);

            CoinData coinData = JsonUtility.FromJson<CoinData>(json);
            coins = coinData.coins;
        }
    }
    public void SaveHealth()
    {

        HealthData healthData = new HealthData();
        healthData.heartContainers = heartContainers;
        healthData.currentHealth = currentHealth;


        string healthJson = JsonUtility.ToJson(healthData, true);


        string healthFilePath = Application.dataPath + "/healthData.json";
        File.WriteAllText(healthFilePath, healthJson);
    }
    public void LoadHealth()
    {
        string healthFilePath = Application.dataPath + "/healthData.json";
        if (File.Exists(healthFilePath))
        {
            string healthJson = File.ReadAllText(healthFilePath);
            HealthData healthData = JsonUtility.FromJson<HealthData>(healthJson);
            heartContainers = healthData.heartContainers;
            currentHealth = healthData.currentHealth;
        }
    }

    public void SaveMana()
    {
        ManaData manaData = new ManaData();
        manaData.maxMana = maxMana;
        manaData.currentMana = currentMana;
        string manaJson = JsonUtility.ToJson(manaData, true);
        string manaFilePath = Application.dataPath + "/manaData.json";
        File.WriteAllText(manaFilePath, manaJson);
    }

    public void LoadMana()
    {
        string manaFilePath = Application.dataPath + "/manaData.json";
        if (File.Exists(manaFilePath))
        {
            string manaJson = File.ReadAllText(manaFilePath);
            ManaData manaData = JsonUtility.FromJson<ManaData>(manaJson);
            maxMana = manaData.maxMana;
            currentMana = manaData.currentMana;
        }
    }

    public void SaveScores()
    {
        ScoreData scoreData = new ScoreData();
        scoreData.scores = scores;
        string json = JsonUtility.ToJson(scoreData, true);
        File.WriteAllText(Application.dataPath + "/scoreData.json", json);
    }

    public void LoadScores()
    {
        string filePath = Application.dataPath + "/scoreData.json";
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            ScoreData scoreData = JsonUtility.FromJson<ScoreData>(json);
            scores = scoreData.scores;
        }
    }

    public void SaveArmor()
    {
        ArmorData armorData = new ArmorData();
        armorData.maxArmor = maxArmor;
        armorData.currentArmor = currentArmor;
        string armorJson = JsonUtility.ToJson(armorData, true);
        string armorFilePath = Application.dataPath + "/armorData.json";
        File.WriteAllText(armorFilePath, armorJson);
    }

    public void LoadArmor()
    {
        string armorFilePath = Application.dataPath + "/armorData.json";
        if (File.Exists(armorFilePath))
        {
            string armorJson = File.ReadAllText(armorFilePath);
            ArmorData armorData = JsonUtility.FromJson<ArmorData>(armorJson);
            maxArmor = armorData.maxArmor;
            currentArmor = armorData.currentArmor;
        }
    }


}
