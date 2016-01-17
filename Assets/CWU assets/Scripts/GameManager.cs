using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public bool gameOver = false;

	void OnGUI()
	{
		if(gameOver)
		{
			GUILayout.Label("GAME OVER: Press Return to reset!");
		}
	}

	void Update()
	{
		if(gameOver)
		{
			if(Input.GetKeyDown(KeyCode.Return))
			{
				Application.LoadLevel(Application.loadedLevel);
				gameOver = false;
			}
		}
	}
}
