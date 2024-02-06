using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class PauseManager : MonoBehaviour
{
    [Header("Pause Status")]
    private bool isPaused;

    [Header("Pause Panels")]
    public GameObject pausePanel;
    public GameObject inventoryPanel;
    public GameObject shopPanel;

    [Header("Panel Usage Flags")]
    public bool usingShopPanel;
    public bool usingPausePanel;

    [Header("Scene Management")]
    public string mainMenu;

    //private string volumeDataPath = "Assets/MusicData/volumeData.json";
    private string volumeDataPath;
    void Start()
    {
        isPaused = false;
        pausePanel.SetActive(false);
        inventoryPanel.SetActive(false);
        shopPanel.SetActive(false);
        usingShopPanel = false;
        usingPausePanel = false;
        volumeDataPath = Path.Combine(Application.dataPath, "Assets/MusicData/volumeData.json");
    }
    void Update()
    {
        if (Input.GetButtonDown("pause") || Input.GetKeyDown(KeyCode.P))
        {
            ChangePause();
        }
        if (Input.GetKeyUp(KeyCode.B))
        {
            ShopPanel();
            Time.timeScale = 1f;
        }
        if (Input.GetKeyUp(KeyCode.N))
        {
            ChangeInventoryPanel();
            Time.timeScale = 1f;
        }
    }


    public void ChangePause()
    {
        isPaused = !isPaused;
        if (isPaused)
        {
            pausePanel.SetActive(true);
            Time.timeScale = 0f;
            usingPausePanel = true;
        }
        else
        {
            inventoryPanel.SetActive(false);
            pausePanel.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void QuitToMain()
    {
        SceneManager.LoadScene(mainMenu);
        Time.timeScale = 1f;
       // DeleteVolumeDataFile();
    }
    public void SwitchPanels()
    {
        usingPausePanel = !usingPausePanel;
        if (usingPausePanel)
        {
            pausePanel.SetActive(true);
            inventoryPanel.SetActive(false); 
        }
        else
        {
            inventoryPanel.SetActive(true);
            pausePanel.SetActive(false);
        }
    }
    public void ShopPanel()
    {
        // Kiểm tra trạng thái hiện tại của shopPanel
        bool isShopPanelActive = shopPanel.activeSelf;

        // Đảo ngược trạng thái của shopPanel
        shopPanel.SetActive(!isShopPanelActive);
    }
    public void ChangeInventoryPanel()
    {
        // Kiểm tra trạng thái hiện tại của shopPanel
        bool isInventoryPanelActive = inventoryPanel.activeSelf;

        // Đảo ngược trạng thái của shopPanel
        inventoryPanel.SetActive(!isInventoryPanelActive);
    }
    public void DeleteVolumeDataFile()
    {
        if (File.Exists(volumeDataPath))
        {
            File.Delete(volumeDataPath);
            //Debug.Log("Deleted volume data file.");
        }
        else
        {
            //Debug.Log("Volume data file does not exist.");
        }
    }
}