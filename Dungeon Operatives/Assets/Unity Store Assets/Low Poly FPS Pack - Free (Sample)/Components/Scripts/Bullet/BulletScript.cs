using UnityEngine;
using System.Collections;

// ----- Low Poly FPS Pack Free Version -----
public class BulletScript : MonoBehaviour
{

	[Range(5, 100)]
	[Tooltip("After how long time should the bullet prefab be destroyed?")]
	public float destroyAfter;
	[Tooltip("If enabled the bullet destroys on impact")]
	public bool destroyOnImpact = false;
	[Tooltip("Minimum time after impact that the bullet is destroyed")]
	public float minDestroyTime;
	[Tooltip("Maximum time after impact that the bullet is destroyed")]
	public float maxDestroyTime;

	[Header("Impact Effect Prefabs")]
	public Transform[] metalImpactPrefabs;

	//Damage enemy variables
	public float bodyDamage = 25;
	public float headDamage = 50;
    public GameObject healthDrop;
    //public float cbodyDamage = 25;
    //public float cheadDamage = 50;

    private void Start()
	{        
        //Start destroy timer
        StartCoroutine(DestroyAfter());        
    }

    //If the bullet collides with anything
    private void OnCollisionEnter(Collision collision)
    {
        var rangeEnemy = collision.transform.gameObject;
        var chaseEnemy = collision.transform.gameObject;
        //If destroy on impact is false, start 
        //coroutine with random destroy timer
        if (!destroyOnImpact)
        {
            StartCoroutine(DestroyTimer());
        }
        //Otherwise, destroy bullet on impact
        else
        {
            Destroy(gameObject);
        }

        //If bullet collides with "Metal" tag
        if (collision.transform.tag == "Metal")
        {
            //Instantiate random impact prefab from array
            Instantiate(metalImpactPrefabs[Random.Range
                (0, metalImpactPrefabs.Length)], transform.position,
                Quaternion.LookRotation(collision.contacts[0].normal));
            //Destroy bullet object
            Destroy(gameObject);
        }

        //If bullet collides with "Target" tag
        if (collision.transform.tag == "Target")
        {
            //Toggle "isHit" on target object
            collision.transform.gameObject.GetComponent<TargetScript>().isHit = true;
            //Destroy bullet object
            Destroy(gameObject);
        }

        //If bullet collides with "ExplosiveBarrel" tag
        if (collision.transform.tag == "ExplosiveBarrel")
        {
            //Toggle "explode" on explosive barrel object
            collision.transform.gameObject.GetComponent<ExplosiveBarrelScript>().explode = true;
            //Destroy bullet object
            Destroy(gameObject);
        }

        //If bullet collides with "RangeEnemy" tag and "RangeEnemHead" tag
        if (collision.transform.tag == "RangeEnemy" && collision.gameObject.GetComponent<RangeEnemyController>().rhealth > 1)
        {
            collision.transform.gameObject.GetComponent<RangeEnemyController>().rhealth -= bodyDamage;
            Destroy(gameObject);
        }
        if (collision.transform.tag == "RangeEnemHead" && collision.gameObject.GetComponentInParent<RangeEnemyController>().rhealth > 1)
        {
            collision.transform.gameObject.GetComponentInParent<RangeEnemyController>().rhealth -= headDamage;
            Destroy(gameObject);
        }

        if (collision.transform.gameObject.GetComponentInParent<RangeEnemyController>().rhealth <= 0 && (collision.transform.tag == "RangeEnemy" || collision.transform.tag == "RangeEnemHead"))
        {
            Instantiate(healthDrop, transform.localPosition, Quaternion.Euler(-90,0,0));
            Destroy(gameObject);
            Destroy(rangeEnemy.GetComponentInParent<RangeEnemyController>().gameObject);
            //Destroy(rangeEnemy);
        }      

            //If bullet collides with "ChaseEnemy" tag and "ChaseEnemHead" tag
            //if (collision.transform.tag == "ChaseEnemy" && collision.gameObject.GetComponent<ChaseEnemyController>().health > 1)
            //{
            //    collision.transform.gameObject.GetComponent<ChaseEnemyController>().health -= bodyDamage;
            //    Destroy(gameObject);
            //}
            //if (collision.transform.tag == "ChaseEnemHead" && collision.gameObject.GetComponentInParent<ChaseEnemyController>().health > 1)
            //{
            //    collision.transform.gameObject.GetComponentInParent<ChaseEnemyController>().health -= headDamage;
            //    Destroy(gameObject);
            //}
            //if (collision.transform.gameObject.GetComponent<ChaseEnemyController>().health <= 0)
            //{
            //    Destroy(gameObject);
            //    Destroy(chaseEnemy.GetComponent<ChaseEnemyController>().gameObject);
            //    Destroy(chaseEnemy);
            //}
        }

 //   private void OnControllerColliderHit(ControllerColliderHit hit)
 //   {
	//	if (hit.transform.tag == "ChaseEnemy" && hit.gameObject.GetComponent<ChaseEnemyController>().health > 1)
	//	{
	//		hit.transform.gameObject.GetComponent<ChaseEnemyController>().health -= bodyDamage;
	//	}
	//}

    private IEnumerator DestroyTimer () 
	{
		//Wait random time based on min and max values
		yield return new WaitForSeconds
			(Random.Range(minDestroyTime, maxDestroyTime));
		//Destroy bullet object
		Destroy(gameObject);
	}

	private IEnumerator DestroyAfter () 
	{
		//Wait for set amount of time
		yield return new WaitForSeconds (destroyAfter);
		//Destroy bullet object
		Destroy (gameObject);
	}
}
// ----- Low Poly FPS Pack Free Version -----