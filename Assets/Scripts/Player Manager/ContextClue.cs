using UnityEngine;

public class ContextClue : MonoBehaviour
{
    [Header("Context Clue Settings")]
    public GameObject contextClue;
    public bool contextActive = false;

   public void ChangeContext()
    {
        contextActive = !contextActive;
        if(contextActive)
        {
            contextClue.SetActive(true);
        }    
        else
        {
            contextClue.SetActive(false);
        }    
    }    
}
