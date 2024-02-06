using UnityEngine;

public class MummyIdle : Enemy
{
    [Header("Define Target")]
    public Transform target;
    public Collider2D boundary;

    [Header("Summoning Settings")]
    public GameObject mummyPrefab;
    public Transform summonMummy;
    private GameObject mummyInstance;
    private float cooldownTimer = 0f;

    void Update()
    {

        if (cooldownTimer <= 0f)
        {
            if (boundary.bounds.Contains(target.transform.position))
            {
                //Debug.Log("Summoning Mummy...");
                SummonMummy();
                cooldownTimer = 5f; 
            }
        }
        else
        {
            cooldownTimer -= Time.deltaTime; 
        }
    }

    public void SummonMummy()
    {
        int randomId = UnityEngine.Random.Range(1000, 10000);

        mummyPrefab.GetComponent<Mummy>().id = randomId;
        GameObject mummyObject = Instantiate(mummyPrefab, summonMummy.position, Quaternion.identity);
        Mummy mummyComponent = mummyObject.GetComponent<Mummy>();
        if (mummyComponent != null)
        {
            mummyComponent.CopyBoundaryFromMummyIdle(this); 
        }
    }    
}
