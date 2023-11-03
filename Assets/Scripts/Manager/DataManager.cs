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

    public void SaveCoin()
    {
        // Tạo đối tượng chứa thông tin coin để lưu vào JSON
        CoinData coinData = new CoinData();
        coinData.coins = coins;

        // Chuyển đổi đối tượng coinData thành chuỗi JSON
        string json = JsonUtility.ToJson(coinData, true);

        // Lưu chuỗi JSON vào file
        File.WriteAllText(Application.dataPath + "/coinData.json", json);
    }
    public void LoadCoin()
    {
        // Đọc nội dung tệp JSON
        string filePath = Application.dataPath + "/coinData.json";
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);

            // Chuyển đổi chuỗi JSON thành đối tượng coinData
            CoinData coinData = JsonUtility.FromJson<CoinData>(json);

            // Cập nhật giá trị coins
            coins = coinData.coins;
        }
    }
    public void SaveHealth()
    {
        // Tạo đối tượng chứa thông tin sức khỏe để lưu vào JSON
        HealthData healthData = new HealthData();
        healthData.heartContainers = heartContainers;
        healthData.currentHealth = currentHealth;

        // Chuyển đổi đối tượng healthData thành chuỗi JSON
        string healthJson = JsonUtility.ToJson(healthData, true);

        // Lưu chuỗi JSON vào file
        string healthFilePath = Application.dataPath + "/healthData.json";
        File.WriteAllText(healthFilePath, healthJson);
    }
    public void LoadHealth()
    {
        // Đọc nội dung tệp JSON cho sức khỏe
        string healthFilePath = Application.dataPath + "/healthData.json";
        if (File.Exists(healthFilePath))
        {
            string healthJson = File.ReadAllText(healthFilePath);

            // Chuyển đổi chuỗi JSON thành đối tượng healthData
            HealthData healthData = JsonUtility.FromJson<HealthData>(healthJson);

            // Cập nhật giá trị heartContainers và currentHealth
            heartContainers = healthData.heartContainers;
            currentHealth = healthData.currentHealth;
        }
    }

    public void SaveMana()
    {
        // Tạo đối tượng chứa thông tin mana để lưu vào JSON
        ManaData manaData = new ManaData();
        manaData.maxMana = maxMana;
        manaData.currentMana = currentMana;

        // Chuyển đổi đối tượng manaData thành chuỗi JSON
        string manaJson = JsonUtility.ToJson(manaData, true);

        // Lưu chuỗi JSON vào file
        string manaFilePath = Application.dataPath + "/manaData.json";
        File.WriteAllText(manaFilePath, manaJson);
    }

    public void LoadMana()
    {
        // Đọc nội dung tệp JSON cho mana
        string manaFilePath = Application.dataPath + "/manaData.json";
        if (File.Exists(manaFilePath))
        {
            string manaJson = File.ReadAllText(manaFilePath);

            // Chuyển đổi chuỗi JSON thành đối tượng manaData
            ManaData manaData = JsonUtility.FromJson<ManaData>(manaJson);

            // Cập nhật giá trị maxMana và currentMana
            maxMana = manaData.maxMana;
            currentMana = manaData.currentMana;
        }
    }
    public void SaveScores()
    {
        // Tạo đối tượng chứa thông tin điểm để lưu vào JSON
        ScoreData scoreData = new ScoreData();
        scoreData.scores = scores;

        // Chuyển đổi đối tượng scoreData thành chuỗi JSON
        string json = JsonUtility.ToJson(scoreData, true);

        // Lưu chuỗi JSON vào tệp
        File.WriteAllText(Application.dataPath + "/scoreData.json", json);
    }

    public void LoadScores()
    {
        // Đọc nội dung tệp JSON cho điểm
        string filePath = Application.dataPath + "/scoreData.json";
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);

            // Chuyển đổi chuỗi JSON thành đối tượng scoreData
            ScoreData scoreData = JsonUtility.FromJson<ScoreData>(json);

            // Cập nhật giá trị điểm (scores)
            scores = scoreData.scores;
        }
    }
    public void SaveArmor()
    {
        // Tạo đối tượng chứa thông tin giáp để lưu vào JSON
        ArmorData armorData = new ArmorData();
        armorData.maxArmor = maxArmor;
        armorData.currentArmor = currentArmor;

        // Chuyển đổi đối tượng armorData thành chuỗi JSON
        string armorJson = JsonUtility.ToJson(armorData, true);

        // Lưu chuỗi JSON vào file
        string armorFilePath = Application.dataPath + "/armorData.json";
        File.WriteAllText(armorFilePath, armorJson);
    }

    public void LoadArmor()
    {
        // Đọc nội dung tệp JSON cho giáp
        string armorFilePath = Application.dataPath + "/armorData.json";
        if (File.Exists(armorFilePath))
        {
            string armorJson = File.ReadAllText(armorFilePath);

            // Chuyển đổi chuỗi JSON thành đối tượng armorData
            ArmorData armorData = JsonUtility.FromJson<ArmorData>(armorJson);

            // Cập nhật giá trị maxArmor và currentArmor
            maxArmor = armorData.maxArmor;
            currentArmor = armorData.currentArmor;
        }
    }

}
