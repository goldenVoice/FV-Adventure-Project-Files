using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour
{
	void OnTriggerEnter()
	{
		GameObject.FindGameObjectWithTag("Player").SendMessage("PowerUp");
		Destroy(gameObject);
	}
}
