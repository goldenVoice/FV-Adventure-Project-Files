using UnityEngine;
using System.Collections;

public class EnableAllScriptsOnTrigger : MonoBehaviour
{
	void OnTriggerEnter()
	{
		Debug.Log("Triggered");
		foreach(MonoBehaviour mono in gameObject.GetComponentsInChildren<MonoBehaviour>())
		{
			mono.enabled = true;
		}
	}
}
