using UnityEngine;

public class Example : MonoBehaviour
{
    public HealthReaction healthReaction;

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.M))
        {
            float amountToIncrease = 10f; 
            healthReaction.Use(amountToIncrease);
        }
    }
}
