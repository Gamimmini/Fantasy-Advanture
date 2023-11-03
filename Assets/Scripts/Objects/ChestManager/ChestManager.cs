using System.IO;
using UnityEngine;

public class ChestManager : MonoBehaviour
{
    /*public static ChestManager Instance;
    public ChestData[] chest;
    
    private string filePath;
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        //anim = GetComponent<Animator>();
        filePath = Application.dataPath + "/chestData.json";
        //ChestManager.Instance.LoadChestData();
        //UpdateChestState();
    }
    public void SaveChestData()
    {
        ChestData chestData = new ChestData();
        chestData.RuntimeValue = TreasureChest.Instance.isOpen;

        string json = JsonUtility.ToJson(chestData, true);
        File.WriteAllText(Application.dataPath + "/chestData.json", json);
        Debug.Log("Saved JSON: " + json);
        Debug.Log("Save");

    }
    public void LoadChestData()
    {

        if (File.Exists(filePath)) // Kiểm tra xem tệp tồn tại trước khi đọc
        {
            string json = File.ReadAllText(filePath);
            ChestData chestData = JsonUtility.FromJson<ChestData>(json);
            //Debug.Log("Loaded JSON: " + json);
            TreasureChest.Instance.isOpen = chestData.RuntimeValue;
            //Debug.Log("Load");
        }
        else
        {
            //Debug.Log("Không tìm thấy tệp chestData.json");
        }
    }
    */
}
