using UnityEngine;
using System.Collections;

public class SetParentEvent : MonoBehaviour
{
	public Transform target;
	public string newParentTag;

	void SetParent()
	{
		Debug.Log ("Totally setting parent");

		Transform newParent = GameObject.FindGameObjectWithTag(newParentTag).transform;

		if(target == null)
		{
			transform.parent = newParent;
		}
		else
		{
			target.parent = newParent;
		}
	}
}
