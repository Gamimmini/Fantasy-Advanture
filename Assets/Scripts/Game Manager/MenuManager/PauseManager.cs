using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{

    private bool isPaused;
    public GameObject pausePanel;
    public GameObject inventoryPanel;
    public GameObject shopPanel;
    public bool usingShopPanel;
    public bool usingPausePanel;
    public string mainMenu;

    void Start()
    {
        isPaused = false;
        pausePanel.SetActive(false);
        inventoryPanel.SetActive(false);
        shopPanel.SetActive(false);
        usingShopPanel = false;
        usingPausePanel = false;
    }
    void Update()
    {
        if (Input.GetButtonDown("pause"))
        {
            ChangePause();
        }
        if (Input.GetKeyUp(KeyCode.B))
        {
            ShopPanel();
        }
        if (Input.GetKeyUp(KeyCode.N))
        {
            ChangeInventoryPanel();
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

}