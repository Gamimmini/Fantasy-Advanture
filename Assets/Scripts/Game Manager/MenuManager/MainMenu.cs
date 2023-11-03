using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class MainMenu : MonoBehaviour
{
    private string chestDataFilePath;
    private string coinDataFilePath;
    private string healthDataFilePath;
    private string manaDataFilePath;
    private string shopItemQuantityDataFilePath;
    private string roomStateDataFilePath;
    private string enemyHealthDataFilePath;
    private string treasureChestDataFilePath;
    private string boxDataFilePath;
    private string scoreDataFilePath;
    private string inventoryDataFilePath;
    private string armorDataFilePath;

    [Header("Select Level")]
    public GameObject selectLevel;
    public GameObject goBtn;
    public GameObject guidePanel;
    private void Start()
    {
        // Xác định đường dẫn đến các tệp JSON riêng biệt
        chestDataFilePath = Path.Combine(Application.dataPath, "chestData.json");
        coinDataFilePath = Path.Combine(Application.dataPath, "coinData.json");
        healthDataFilePath = Path.Combine(Application.dataPath, "healthData.json");
        manaDataFilePath = Path.Combine(Application.dataPath, "manaData.json");
        shopItemQuantityDataFilePath = Path.Combine(Application.dataPath, "ShopItemQuantity.json");
        roomStateDataFilePath = Path.Combine(Application.dataPath, "DataRoom");
        enemyHealthDataFilePath = Path.Combine(Application.dataPath, "EnemyHealthData");
        treasureChestDataFilePath = Path.Combine(Application.dataPath, "DataChest");
        boxDataFilePath = Path.Combine(Application.dataPath, "DataBox");
        scoreDataFilePath = Path.Combine(Application.dataPath, "scoreData.json");
        inventoryDataFilePath = Path.Combine(Application.dataPath, "inventoryData.json");
        armorDataFilePath = Path.Combine(Application.dataPath, "armorData.json");

        selectLevel.SetActive(false);
        goBtn.SetActive(false);
        guidePanel.SetActive(false);
    }

    public void NewGame()
    {
        SceneManager.LoadScene("OpeningCutscene");
    }

    public void QuitToDesktop()
    {
        Application.Quit();
    }
    public void SelecetLevel()
    {
        selectLevel.SetActive(true);
        goBtn.SetActive(false);  
    }
    public void ReadGuide()
    {
        guidePanel.SetActive(true);
    }
    public void DeleteDataFiles()
    {
        // Kiểm tra và xóa từng tệp JSON
        DeleteFile(chestDataFilePath);
        DeleteFile(coinDataFilePath);
        DeleteFile(scoreDataFilePath);
        DeleteFile(healthDataFilePath);
        DeleteFile(manaDataFilePath);
        DeleteFile(shopItemQuantityDataFilePath);
        DeleteFile(inventoryDataFilePath);
        DeleteFile(armorDataFilePath);
        // Xóa tệp JSON cho dữ liệu của cửa
        string doorDataDirectory = Path.Combine(Application.dataPath, "DataDoor");
        if (Directory.Exists(doorDataDirectory))
        {
            string[] doorDataFiles = Directory.GetFiles(doorDataDirectory, "Door_*.json");
            foreach (string doorDataFile in doorDataFiles)
            {
                DeleteFile(doorDataFile);
            }
        }
        if (Directory.Exists(roomStateDataFilePath))
        {
            string[] roomStateDataFiles = Directory.GetFiles(roomStateDataFilePath, "RoomState_*.json");
            foreach (string roomStateDataFile in roomStateDataFiles)
            {
                DeleteFile(roomStateDataFile);
            }
        }
        if (Directory.Exists(enemyHealthDataFilePath))
        {
            string[] enemyHealthDataFiles = Directory.GetFiles(enemyHealthDataFilePath, "EnemyHealth_*.json");
            foreach (string enemyHealthDataFile in enemyHealthDataFiles) 
            {
                DeleteFile(enemyHealthDataFile);
            }
        }
        if (Directory.Exists(treasureChestDataFilePath))
        {
            string[] treasureChestDataFiles = Directory.GetFiles(treasureChestDataFilePath, "ChestData_*.json");
            foreach (string treasureChestDataFile in treasureChestDataFiles)
            {
                DeleteFile(treasureChestDataFile);
            }
        }
        if (Directory.Exists(boxDataFilePath))
        {
            string[] boxDataFiles = Directory.GetFiles(boxDataFilePath, "BoxData_*.json");
            foreach (string boxDataFile in boxDataFiles)
            {
                DeleteFile(boxDataFile);
            }
        }
    }

    private void DeleteFile(string filePath)
    {
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
            //Debug.Log("Deleted " + Path.GetFileName(filePath));
        }
    }

}
