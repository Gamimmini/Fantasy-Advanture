using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;
public class EndGame : MonoBehaviour
{
    public GameObject winPanel;
    public GameObject LosePanel;
    public GameObject finishPanel;
    public string sceneToLoad;
    public DataManager dataManager;
    public TextMeshProUGUI winText;
    public string customString;
    private string scoreDataFilePath;
    void Start()
    {
        winPanel.SetActive(false);
        LosePanel.SetActive(false);
        finishPanel.SetActive(false);
        

        scoreDataFilePath = Path.Combine(Application.dataPath, "scoreData.json");
    }

    public void RestartGame()
    {
        // Đây là hàm để load lại scene hiện tại
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
    private void Update()
    {
        ChangeText();
    }

    public void GoToNextScene()
    {
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
    public void DeleteDataFiles()
    {
        DeleteFile(scoreDataFilePath);
    }    

    public void ChangeText()
    {
        // Kiểm tra điều kiện và hiển thị winText nếu thỏa mãn
        if (dataManager.scores >= 4 && winText != null)
        {
            winText.text = "Your code: " + customString;
        }
        else
        {
            winText.text = "You didn't collect enough code!";
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
