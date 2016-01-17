using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{
	public int initialHealth = 10;
	public GameObject hitEffect;
	public GameObject deathEffect;
	public string loadLevelOnDeath = "";
	private int currentHealth;

	// Use this for initialization
	void Start ()
	{
		currentHealth = initialHealth;
	}

	void OnTriggerEnter(Collider col)
	{
		currentHealth--;
		Instantiate(hitEffect, col.transform.position, Quaternion.identity);
		if(currentHealth <= 0)
		{
			transform.parent.SendMessage("Dead",transform.position, SendMessageOptions.DontRequireReceiver);
			Instantiate(deathEffect, col.transform.position, Quaternion.identity);
			Destroy(gameObject);

			if(!System.String.IsNullOrEmpty(loadLevelOnDeath))
			{
				Application.LoadLevel(loadLevelOnDeath);
			}
		}
	}
}
