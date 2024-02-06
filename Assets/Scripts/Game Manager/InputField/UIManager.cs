using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("Select Level Settings")]
    public TMP_InputField inputField;
    public GameObject goBtn;

    public MainMenu mainMenu;

    private void Start()
    {
        inputField.onValueChanged.AddListener(OnInputFieldValueChanged);
    }

    private void OnInputFieldValueChanged(string value)
    {
        //Debug.Log("Giá trị trong InputField: " + value);
        if (value == "0411" || value == "1010")
        {
            //Debug.Log("Người chơi đã nhập đúng mã số bí mật!");
            goBtn.SetActive(true);
        }
        else
        {
            goBtn.SetActive(false);
        }
    }
    public void SelectScene()
    {
        if (inputField.text == "0411")
        {
            SceneManager.LoadScene("SampleScene");
            //Debug.Log("SampleScene");
        }
        else if (inputField.text == "1010")
        {
            SceneManager.LoadScene("Scene2");
            //Debug.Log("Scene2");
        }
        mainMenu.DeleteDataFiles();
    }    
    
}
