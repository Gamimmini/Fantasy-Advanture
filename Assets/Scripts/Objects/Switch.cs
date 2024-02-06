using UnityEngine;

public class Switch : MonoBehaviour
{
    [Header("Switch Settings")]
    public bool active;

    [Header("Sprites")]
    public Sprite activeSprite;

    [Header("References")]
    public Door thisDoor;

    private SpriteRenderer mySprite;
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
