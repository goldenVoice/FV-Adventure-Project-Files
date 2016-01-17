using UnityEngine;
using System.Collections;

public class DestroyParticleWhenFinished : MonoBehaviour
{	
	// Update is called once per frame
	void Update ()
	{
		if(!particleSystem.isPlaying)
		{
			Destroy(gameObject);
		}
	}
}
