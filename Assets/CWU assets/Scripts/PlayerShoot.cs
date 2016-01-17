using UnityEngine;
using System.Collections;

public class PlayerShoot : MonoBehaviour
{
	public GameObject bullet;
	public float shotDelay = 0.2f;
	private bool readyToShoot = true;

	// Update is called once per frame
	void Update ()
	{
		if(Input.GetButton("Fire1") && readyToShoot)
		{
			Instantiate(bullet,transform.position,transform.rotation);
			readyToShoot = false;
			Invoke("ResetReadyToShoot", shotDelay);
		}
	}

	void ResetReadyToShoot()
	{
		readyToShoot = true;
	}
}
