using UnityEngine;

public class CustomBox : Interactable
{
    public static CustomBox Instance;

    [Header("Custom Item Settings")]
    public GameObject customItem;

    [Header("Box ID")]
    public int id;

    [Header("Signals and dialogs")]
    public SignalSender raiseItem;
    public bool isOpen;
    private void Awake()
    {
        Instance = this;
        LoadBoxData();
    }
    void Update()
    {
        if (Input.GetButtonDown("attack") && playerInRange)
        {
            if (!isOpen)
            {
                OpenBox();
            }
            else
            {
                gameObject.SetActive(false);
            }    

        }
    }
    public void OpenBox()
    {
        Instantiate(customItem, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
        //int i = 0; // Assign a valid index here.
        //OverworldRoom.Instance.objects[i].SetActive(false);
        isOpen = true;
        SaveBoxData();

    }

    public virtual void SaveBoxData()
    {
        BoxData boxdata = new BoxData
        {
            id = this.id,
            isOpen = this.isOpen
        };

        string json = JsonUtility.ToJson(boxdata);
        string filePath = "Assets/DataBox/BoxData_" + id + ".json";
        System.IO.File.WriteAllText(filePath, json);

    }
    public virtual void LoadBoxData()
    {
        string filePath = "Assets/DataBox/BoxData_" + id + ".json";
        if (System.IO.File.Exists(filePath))
        {
            string json = System.IO.File.ReadAllText(filePath);
            BoxData boxdata = JsonUtility.FromJson<BoxData>(json);
            this.isOpen = boxdata.isOpen;
            if(isOpen)
            {
                gameObject.SetActive(false);
            }    
        }
    }
}
