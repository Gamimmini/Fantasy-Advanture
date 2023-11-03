using UnityEngine;

public class Switch : MonoBehaviour
{
    public bool active;
    //public BoolValue storedValue;
    public Sprite activeSprite;
    private SpriteRenderer mySprite;
    public Door thisDoor;
    void Start()
    {
        mySprite = GetComponent<SpriteRenderer>();
        //active = storedValue.RuntimeValue;
        if (active)
        {
            ActivateSwitch();
        }
    }
    public void ActivateSwitch()
    {
        active = true;
        //storedValue.RuntimeValue = active; // change value
        thisDoor.Open();
        thisDoor.SaveDoorState();
        mySprite.sprite = activeSprite;
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        // Is it the player?
        if (other.CompareTag("Player"))
        {
            ActivateSwitch();
        }
    }
}
