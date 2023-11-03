using System.Collections.Generic;
using UnityEngine;

public class OverworldRoom : Room
{
    public List<GameObject> objects;
    public static OverworldRoom Instance;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        CheckEnemies();
        if (EnemiesActive() > 0)
        {
            //Door.Instance.SaveDoorState();
            if (objects != null)
            {
                foreach (var obj in objects)
                {
                    obj.SetActive(false);
                }
            }
        }
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
            //Door.Instance.SaveDoorState();
            if (objects != null)
            {

                foreach (var obj in objects)
                {
                    CustomBox customBox = obj.GetComponent<CustomBox>();
                    if (customBox == null)
                    {
                        obj.SetActive(true);
                    }
                    else 
                    {
                        if (customBox.isOpen)
                        {
                            obj.SetActive(false);
                        }
                        else
                        {
                            obj.SetActive(true);
                        }
                    }
                    
                }

            }
        }
    }
}
