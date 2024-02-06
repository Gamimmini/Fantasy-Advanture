using UnityEngine;
using UnityEngine.UI;

public class Sign : Interactable
{
    [Header("Dialog Box")]
    public GameObject dialogBox;
    public Text dialogText;

    [Header("Dialog Text")]
    public string dialog;

    public virtual void Update()
    {
        if (Input.GetButtonDown("attack") && playerInRange)
        {
            if (dialogBox.activeInHierarchy)
            {
                dialogBox.SetActive(false);
            }
            else
            {
                dialogBox.SetActive(true);
                dialogText.text = dialog;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            context.Raise();
            playerInRange = false;
            dialogBox.SetActive(false);
        }
    }
}
