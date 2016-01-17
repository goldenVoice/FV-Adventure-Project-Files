using UnityEngine;
using System.Collections;

public class PlayerControls : MonoBehaviour
{
	private float unitsPerSecond = 4.0f;

	// Use this for initialization
	void Start()
	{
	
	}
	
	// Update is called once per frame
	void Update()
	{
		if( Input.GetKey(KeyCode.A) )
		{
			transform.position -= transform.right * Time.deltaTime * unitsPerSecond;
		}
		if( Input.GetKey(KeyCode.D) )
		{
			transform.position += transform.right * Time.deltaTime * unitsPerSecond;
		}
		if( Input.GetKey(KeyCode.W) )
		{
			transform.position += transform.up * Time.deltaTime * unitsPerSecond;
		}
		if( Input.GetKey(KeyCode.S) )
		{
			transform.position -= transform.up * Time.deltaTime * unitsPerSecond;
		}
	}
}
