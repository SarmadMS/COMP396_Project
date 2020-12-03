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

	//public Transform rangeEnemy;
	public float bodyDamage = 50;
	public float headDamage = 75;

	//public RangeEnemyController rhealth;
	private void Start()
	{
		//Start destroy timer
		StartCoroutine(DestroyAfter());
		//rangeHead = GameObject.FindGameObjectWithTag("RangeEnemy");
		//rangeEnemy = GameObject.FindGameObjectWithTag("RangeEnemy").transform;
	}

	//If the bullet collides with anything
	private void OnCollisionEnter(Collision collision)
	{
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
			collision.transform.gameObject.GetComponent
				<TargetScript>().isHit = true;
			//Destroy bullet object
			Destroy(gameObject);
		}

		//If bullet collides with "ExplosiveBarrel" tag
		if (collision.transform.tag == "ExplosiveBarrel")
		{
			//Toggle "explode" on explosive barrel object
			collision.transform.gameObject.GetComponent
				<ExplosiveBarrelScript>().explode = true;
			//Destroy bullet object
			Destroy(gameObject);
		}

		//If bullet collides with "RangedEnemy" and "RangedEnemyHead"
		if (collision.transform.tag == "RangeEnemy")
		{
			collision.transform.gameObject.GetComponent<RangeEnemyController>().health -= bodyDamage;
		}
		else if (collision.transform.tag == "RangeEnemHead")
		{
			collision.transform.gameObject.GetComponentInParent<RangeEnemyController>().health -= headDamage;
		}
		if (collision.transform.gameObject.GetComponent<RangeEnemyController>().health <= 0)
		{
			GameObject.Destroy(collision.gameObject);
        }
		else if (collision.transform.gameObject.GetComponentInParent<RangeEnemyController>().health <= 0)
		{
			GameObject.Destroy(collision.gameObject);
		}
		//else if (collision.transform.tag == "RangeEnemHead" && collision.transform.gameObject.GetComponent<RangeEnemyController>().health <= 0)
		//{
		//    Destroy(collision.gameObject);
		//}
		//if (collision.transform.gameObject.GetComponentInChildren<RangeEnemyHeadController>().health.health <= 0)
		//{
		//    Destroy(rangeEnemy);
		//}

		//If bullet collides with "ChasingEnemy" and "ChaseEnemyHead"
		if (collision.transform.tag == "ChasingEnemy")
		{
			collision.transform.gameObject.GetComponent<RangeEnemyController>().health -= bodyDamage;
		}
		else if (collision.transform.tag == "ChaseEnemHead")
		{
			collision.transform.gameObject.GetComponentInParent<RangeEnemyController>().health -= headDamage;
		}
		if (collision.transform.gameObject.GetComponent<RangeEnemyController>().health <= 0)
		{
			GameObject.Destroy(collision.gameObject);
		}
		else if (collision.transform.gameObject.GetComponentInParent<RangeEnemyController>().health <= 0)
		{
			GameObject.Destroy(collision.gameObject);
		}
	}

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