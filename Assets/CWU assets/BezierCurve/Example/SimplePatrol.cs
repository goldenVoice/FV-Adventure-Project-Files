using UnityEngine;
using System.Collections;

public class SimplePatrol : MonoBehaviour
{
	public Vector3 positionOne;
	public Vector3 positionTwo;

	public float lerpTime;

	private float currentLerpTime;
	private bool moveTowardOne;

	// Use this for initialization
	void Start()
	{
		transform.position = positionOne;
		moveTowardOne = false;

		currentLerpTime = 0.0f;
	}
	
	// Update is called once per frame
	void Update()
	{
		if( moveTowardOne )
		   transform.position = Vector3.Lerp(positionTwo, positionOne, currentLerpTime / lerpTime);
		else
			transform.position = Vector3.Lerp(positionOne, positionTwo, currentLerpTime / lerpTime);

		if( currentLerpTime >= lerpTime )
		{
			currentLerpTime = 0.0f;
			moveTowardOne = !moveTowardOne;
		}

		currentLerpTime += Time.deltaTime;
	}
}
