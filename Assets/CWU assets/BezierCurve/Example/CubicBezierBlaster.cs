﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CubicBezierBlaster : MonoBehaviour
{
	public GameObject particlePrefab;
	private GameObject[] particles;

	public float maximumJumpDistance = 4.0f;
	public int maximumJumpCount = 4;

	private CubicBezierChain bezierChain;
	
	// variables for moving from one enemy to another over time instead of instantly
	private List<CubicBezierPoints> previousPoints;
	private float maxMovementPerSecond = 30.0f;

	// Use this for initialization
	void Start()
	{
		bezierChain = GetComponent<CubicBezierChain>();

		particles = new GameObject[maximumJumpCount];
		for( int i = 0; i < maximumJumpCount; i++ )
			particles[i] = GameObject.Instantiate(particlePrefab) as GameObject;
		
		previousPoints = new List<CubicBezierPoints>();
	}
	
	// Update is called once per frame
	void Update()
	{
		SelectEnemies();
	}

	private void SelectEnemies()
	{
		List<CubicBezierPoints> chain = new List<CubicBezierPoints>();

		List<GameObject> enemies = new List<GameObject>();
		enemies.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));

		// position variables
		GameObject closest;
		float distance;
		float tempDistance;

		// pull position variables
		float difference;
		Vector3 startPull;
		Vector3 endPull;

		int iterations = 0;
		while( iterations++ < maximumJumpCount )
		{
			// init variables for this jump
			closest = null;
			distance = maximumJumpDistance;

			foreach( GameObject enemy in enemies )
			{
				if( chain.Count == 0 && enemy.transform.position.y <= transform.parent.position.y )
				{
					// do nothing, enemy is below the player
				}
				else if( chain.Count > 0 && enemy.transform.position.y <= chain[chain.Count-1].p3.y )
				{
					// do nothing, enemy is below the previous connected enemy
				}
				else
				{
					if( chain.Count == 0 )
						tempDistance = Vector3.Distance(transform.parent.position, enemy.transform.position);
					else
						tempDistance = Vector3.Distance(chain[chain.Count-1].p3, enemy.transform.position);

					if( tempDistance < distance )
					{
						closest = enemy;
						distance = tempDistance;
					}
				}
			}

			if( distance >= maximumJumpDistance || closest == null )
			{
				// cannot connect to another enemy, end the blaster beam
				break;
			}
			else
			{
				// add a new bezier curve to the chain
				if( chain.Count == 0 )
				{
					// start the laser moving vertical
					difference = closest.transform.position.y - transform.parent.position.y;
					startPull = transform.parent.position + Vector3.up * difference * (difference / distance);
					endPull = startPull;

					chain.Add(new CubicBezierPoints(transform.parent.position, startPull, endPull, closest.transform.position));
				}
				else
				{
					// laser direction is effected by the vector of the previous pull position and end position
					startPull = chain[chain.Count-1].p3 + (chain[chain.Count-1].p3 - chain[chain.Count-1].p2) * 0.5f;
					// continue pull up from startPull
					endPull = startPull + (closest.transform.position.y - chain[chain.Count-1].p3.y) * Vector3.up;

					chain.Add(new CubicBezierPoints(chain[chain.Count-1].p3, startPull, endPull, closest.transform.position));
				}

				// remove this enemy from the list of enemies to check distance for in following iterations
				enemies.Remove(closest);
			}
		}
		// list will determine if the enemy hit particles should display
		List<bool> targetHit = new List<bool>();

		/*
		 * 	This section checks the distance a point is moving from the previous frame and causes the beam to move instead of snap
		 */
		float maxMovementThisFrame = maxMovementPerSecond * Time.deltaTime;
		int i;
		float percentageMoved;
		for( i = 0; i < chain.Count; i++ )
		{
			// if there is a previous position, limit distance moved
			if( i < previousPoints.Count )
			{
				distance = Vector3.Distance(chain[i].p2, previousPoints[i].p2);
				// if the distance is too great, move the new position partway between the previous and target positions
				if( distance > maxMovementThisFrame )
				{
					targetHit.Add(false);

					// this is used to move the pull positions based on the percentage distance the final point is moving
					percentageMoved = maxMovementThisFrame / distance;

					// move pull one
					chain[i].p1 = previousPoints[i].p1 + (chain[i].p1 - previousPoints[i].p1).normalized * maxMovementThisFrame;
					// move pull two
					chain[i].p2 = previousPoints[i].p2 + (chain[i].p2 - previousPoints[i].p2) * percentageMoved;
					// move final point
					chain[i].p3 = previousPoints[i].p3 + (chain[i].p3 - previousPoints[i].p3) * percentageMoved;

					if( i+1 < chain.Count )
					{
						chain[i+1].p0 = chain[i].p3;
					}
					
					// only move one new point to desired position at a time
					if( i >= previousPoints.Count-1 )
						break;
				}
				else
				{
					targetHit.Add(true);
				}
			}
			else	// no previous position, only one new jump
			{
				distance = Vector3.Distance(chain[i].p2, chain[i].p0);
				// if the distance is too great, move the new position partway between the previous and target positions
				if( distance > maxMovementThisFrame )
				{
					targetHit.Add(false);
					
					// this is used to move the pull positions based on the percentage distance the final point is moving
					percentageMoved = maxMovementThisFrame / distance;

					// move pull one
					chain[i].p1 = chain[i].p0 + (chain[i].p1 - chain[i].p0).normalized * maxMovementThisFrame;
					// move pull two
					chain[i].p2 = chain[i].p0 + (chain[i].p2 - chain[i].p0) * percentageMoved;
					// move final point
					chain[i].p3 = chain[i].p0 + (chain[i].p3 - chain[i].p0) * percentageMoved;

					// end the looping
					break;
				}
				else
				{
					targetHit.Add(true);
				}
			}
		}
		
		// remove extra new jumps after loop
		for( int j = chain.Count-1; j > i; j-- )
			chain.Remove(chain[j]);

		// update the bezier curve
		if( chain.Count > 0 || bezierChain.GetSubdivisionPointLength() > 0 )
			bezierChain.SetBezierChain(chain);

		// place particles on enemies hit by the beam
		for( i = 0; i < chain.Count; i++ )
		{
			particles[i].SetActive(targetHit[i]);
			particles[i].transform.position = chain[i].p3;
		}
		for( i = chain.Count; i < particles.Length; i++ )
		{
			particles[i].SetActive(false);
		}

		previousPoints = chain;
	}
}
