﻿using UnityEngine;
using System.Collections;

public class LookAtNearestEnemy : MonoBehaviour
{
	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		float nearestDistance = Mathf.Infinity;
		GameObject nearestEnemy = null;
		foreach(GameObject obj in GameObject.FindGameObjectsWithTag("Enemies"))
		{
			float distance = (transform.position - obj.transform.position).sqrMagnitude;
			if(distance < nearestDistance)
			{
				nearestDistance = distance;
				nearestEnemy = obj;
			}
		}

		if(nearestEnemy != null)
		{
			transform.rotation = Quaternion.LookRotation(nearestEnemy.transform.position - transform.position,Vector3.back);
		}
	}
}
