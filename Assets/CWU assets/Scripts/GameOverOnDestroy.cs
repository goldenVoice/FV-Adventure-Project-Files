using UnityEngine;
using System.Collections;

public class GameOverOnDestroy : MonoBehaviour
{
	void OnDestroy()
	{
		GameObject gameManagerObject = GameObject.FindGameObjectWithTag("GameManager");

		if(gameManagerObject != null)
		{
			GameManager gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
			gameManager.gameOver = true;
		}
	}
}
