using UnityEngine;

public class DungeonEnemyRoom : DungeonRoom
{
    [Header("Door Management")]
    public Door[] doors;

    [Header("Trigger State")]
    private bool hasBeenTriggered = false;

    [Header("Room ID")]
    public int id;
    private void Start()
    {
        LoadRoomState();
        OpenDoors();
    }
    private void Update()
    {
        CheckEnemies();
    }
    public int EnemiesActive()
    {
        int activeEnemies = 0;
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i].gameObject.activeInHierarchy)
            {
                activeEnemies++;
            }
        }
        return activeEnemies;
    }

    public void CheckEnemies()
    {
        if (EnemiesActive() == 0)
        {
            OpenDoors();
            //Door.Instance.SaveDoorState();
        }
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger )
        {
            if(!hasBeenTriggered)
            {
                for (int i = 0; i < enemies.Length; i++)
                {
                    ChangeActivation(enemies[i], true);
                }
                for (int i = 0; i < pots.Length; i++)
                {
                    ChangeActivation(pots[i], true);
                }
                CloseDoors();
            }    
            
            virtualCamera.SetActive(true);
            hasBeenTriggered = true;
            SaveRoomState();
        }

    }

    public override void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                ChangeActivation(enemies[i], false);
            }
            for (int i = 0; i < pots.Length; i++)
            {
                ChangeActivation(pots[i], false);
            }
            virtualCamera.SetActive(false);
            //hasBeenTriggered = false;
        }
    }

    public void CloseDoors()
    {
        for (int i = 0; i < doors.Length; i++)
        {
            doors[i].Close();
        }
        //Debug.Log("Close Doors");
    }

    public void OpenDoors()
    {
        for (int i = 0; i < doors.Length; i++)
        {
            doors[i].Open();
            Door.Instance.SaveDoorState();
        }
        //Debug.Log("Open Doors");
    }
    public void SaveRoomState()
    {
        RoomState roomState = new RoomState
        {
            id = this.id,
            hasBeenTriggered = this.hasBeenTriggered
        };

        string json = JsonUtility.ToJson(roomState);
        string filePath = "Assets/DataRoom/RoomState_" + id + ".json";
        System.IO.File.WriteAllText(filePath, json);
    }

    public void LoadRoomState()
    {
        string filePath = "Assets/DataRoom/RoomState_" + id + ".json";
        if (System.IO.File.Exists(filePath))
        {
            string json = System.IO.File.ReadAllText(filePath);
            RoomState roomState = JsonUtility.FromJson<RoomState>(json);
            this.hasBeenTriggered = roomState.hasBeenTriggered;
        }
    }


}