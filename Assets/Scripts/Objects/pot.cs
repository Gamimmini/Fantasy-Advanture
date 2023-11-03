using System.Collections;
using UnityEngine;

public class pot : MonoBehaviour
{
    private Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void Smash()
    {
        anim.SetBool("smash", true);
        StartCoroutine(brealCo());
    }    
    IEnumerator brealCo()
    {
        yield return new WaitForSeconds(0.4f);
        this.gameObject.SetActive(false);
    }    
}
