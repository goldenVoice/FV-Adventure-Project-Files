using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BezierLaser : MonoBehaviour
{
	public List<Transform> beamTransforms = new List<Transform>();
	private QuadraticBezierChain beam;

	private class EnemyInfo
	{
		GameObject enemy;
		float sqrDistaceFromPlayer;
	}

	// Use this for initialization
	void Start ()
	{
		beam = GetComponent<QuadraticBezierChain>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		List<GameObject> allEnemies = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemies"));

		beamTransforms.Clear();

		beamTransforms.Add(transform);

		while(allEnemies.Count > 0)
		{
			float nearestSqrDistance = Mathf.Infinity;
			GameObject nearestEnemy = null;

			foreach(GameObject enemy in allEnemies)
			{
				float sqrDistance = (enemy.transform.position - beamTransforms[beamTransforms.Count-1].position).sqrMagnitude;
				if(sqrDistance < nearestSqrDistance)
				{
					nearestSqrDistance = sqrDistance;
					nearestEnemy = enemy;
				}
			}

			beamTransforms.Add(nearestEnemy.transform);
			allEnemies.Remove(nearestEnemy);
		}


		List<QuadraticBezierPoints> beamChain = new List<QuadraticBezierPoints>();

		for(int i = 0; i < beamTransforms.Count - 1; i++)
		{
			QuadraticBezierPoints bezierPoints = new QuadraticBezierPoints(beamTransforms[i].position,Vector3.Lerp(beamTransforms[i].position,beamTransforms[i+1].position,0.5f),beamTransforms[i+1].position);
			beamChain.Add(bezierPoints);

			beam.SetBezierChain(beamChain);
		}
	}
}
